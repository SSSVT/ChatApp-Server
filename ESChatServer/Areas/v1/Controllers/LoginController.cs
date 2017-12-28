using ESChatServer.Areas.v1.Models.Application.Entities;
using ESChatServer.Areas.v1.Models.Application.Objects;
using ESChatServer.Areas.v1.Models.Database;
using ESChatServer.Areas.v1.Models.Database.Entities;
using ESChatServer.Areas.v1.Models.Database.Interfaces;
using ESChatServer.Areas.v1.Models.Database.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace ESChatServer.Areas.v1.Controllers
{
    [Produces("application/json")]
    [Area("v1")]
    public class LoginController : Controller
    {
        #region Fields
        private IConfiguration _config;
        protected readonly IUsersRepository _usersRepository;
        #endregion

        public LoginController(IConfiguration config, DatabaseContext context)
        {
            this._config = config;
            this._usersRepository = new UsersRepository(context);
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> LoginAsync([FromBody]LoginModel login)
        {
            IActionResult response = Unauthorized();
            User user = await this.AuthenticateAsync(login);

            if (user != null)
            {
                JwtSecurityToken jwtSecurityToken = this.BuildToken(user);
                string tokenString = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
                string expiration = jwtSecurityToken.Claims.Where(c => c.Type == "exp").Single().Value;

                response = Ok(new{
                    token = tokenString,
                    exp = expiration,
                    type = "Bearer"
                });
            }

            return response;
        }

        protected JwtSecurityToken BuildToken(User user)
        {
            Claim[] claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Username),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim(JwtRegisteredClaimNames.Birthdate, user.Birthdate.ToString()),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            SymmetricSecurityKey key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            SigningCredentials creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            JwtSecurityToken token = new JwtSecurityToken(
                _config["Jwt:Issuer"],
                _config["Jwt:Issuer"],
                claims,
                expires: DateTime.Now.AddMinutes(30),
                signingCredentials: creds);

            return token;
        }

        protected async Task<User> AuthenticateAsync(LoginModel login)
        {
            User user = await this._usersRepository.FindByUsernameAsync(login.Username);

            if (user != null &&
                user.Username == login.Username &&
                PasswordFactory.Hash(login.Password, user.PasswordSalt) == user.PasswordHash)
            {
                return user;
            }
            else
            {
                return null;
            }
        }
    }
}