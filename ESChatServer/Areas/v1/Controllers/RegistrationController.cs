using ESChatServer.Areas.v1.Models.Application.Entities;
using ESChatServer.Areas.v1.Models.Application.Objects;
using ESChatServer.Areas.v1.Models.Database;
using ESChatServer.Areas.v1.Models.Database.Interfaces;
using ESChatServer.Areas.v1.Models.Database.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using Entities = ESChatServer.Areas.v1.Models.Database.Entities;

namespace ESChatServer.Areas.v1.Controllers
{
    [Produces("application/json")]
    [Area("v1")]
    [AllowAnonymous]
    public partial class RegistrationController : Controller
    {
        #region Fields
        protected readonly IUsersRepository _usersRepository;
        #endregion

        public RegistrationController(DatabaseContext context)
        {
            this._usersRepository = new UsersRepository(context);
        }

        [HttpPost]
        public IActionResult Register([FromBody]RegistrationModel id)
        {
            try
            {
                if (!ModelState.IsValid || !this._IsUsernameAvailable(id.Username))
                {
                    return BadRequest(ModelState);
                }

                string salt = PasswordFactory.GenerateSalt();
                Entities.User user = new Entities.User(id)
                {
                    PasswordHash = PasswordFactory.Hash(id.Password, salt),
                    PasswordSalt = salt,
                    UTCRegistrationDate = DateTime.UtcNow,
                    Status = "A"
                };
                this._usersRepository.Add(user, true);

                return Ok(); //TODO: Return token
            }
            catch (Exception ex)
            {
                //TODO: SaveException
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
        [HttpPost]
        public async Task<IActionResult> RegisterAsync([FromBody]RegistrationModel id)
        {
            try
            {
                if (!ModelState.IsValid || !await this._IsUsernameAvailableAsync(id.Username))
                {
                    return BadRequest(ModelState);
                }

                string salt = PasswordFactory.GenerateSalt();
                Entities.User user = new Entities.User(id)
                {
                    PasswordHash = PasswordFactory.Hash(id.Password, salt),
                    PasswordSalt = salt,
                    UTCRegistrationDate = DateTime.UtcNow,
                    Status = "A"
                };
                await this._usersRepository.AddAsync(user, true);

                return Ok(); //TODO: Return token
            }
            catch (Exception ex)
            {
                //TODO: SaveException
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpGet]
        public IActionResult IsUsernameAvailable(string id)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                return Ok(this._IsUsernameAvailable(id));
            }
            catch (Exception ex)
            {
                //TODO: SaveException
                return StatusCode(StatusCodes.Status500InternalServerError);
            }            
        }
        [HttpGet]
        public async Task<IActionResult> IsUsernameAvailableAsync(string id)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                return Ok(await this._IsUsernameAvailableAsync(id));
            }
            catch (Exception ex)
            {
                //TODO: SaveException
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }

    public partial class RegistrationController
    {
        protected bool _IsUsernameAvailable(string username)
        {
            return this._usersRepository.FindByUsername(username) == null;
        }
        protected async Task<bool> _IsUsernameAvailableAsync(string username)
        {
            return await this._usersRepository.FindByUsernameAsync(username) == null;
        }
    }
}