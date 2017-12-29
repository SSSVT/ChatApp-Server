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
    public sealed partial class RegistrationController : Controller
    {
        #region Fields
        private readonly IUsersRepository _usersRepository;
        #endregion

        public RegistrationController(DatabaseContext context)
        {
            this._usersRepository = new UsersRepository(context);
        }

        #region HttpGet (Select)
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
        #endregion

        #region HttpPost (Create)
        [HttpPost]
        public IActionResult Register([FromBody] RegistrationModel id)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                if (!this._IsUsernameAvailable(id.Username))
                {
                    return BadRequest();
                }

                string salt = PasswordFactory.GenerateSalt();
                Entities.User item = new Entities.User(id)
                {
                    PasswordHash = PasswordFactory.Hash(id.Password, salt),
                    PasswordSalt = salt,
                    UTCRegistrationDate = DateTime.UtcNow,
                    Status = "A"
                };
                this._usersRepository.Add(item, true);

                return CreatedAtAction("LoginAsync", "Token", new { id = item.ID }, item);
            }
            catch (Exception ex)
            {
                //TODO: SaveException
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
        [HttpPost]
        public async Task<IActionResult> RegisterAsync([FromBody] RegistrationModel id)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                if (!await this._IsUsernameAvailableAsync(id.Username))
                {
                    return BadRequest();
                }

                string salt = PasswordFactory.GenerateSalt();
                Entities.User item = new Entities.User(id)
                {
                    PasswordHash = PasswordFactory.Hash(id.Password, salt),
                    PasswordSalt = salt,
                    UTCRegistrationDate = DateTime.UtcNow,
                    Status = "A"
                };
                await this._usersRepository.AddAsync(item, true);

                return CreatedAtAction("LoginAsync", "Token", new { id = item.ID }, item);
            }
            catch (Exception ex)
            {
                //TODO: SaveException
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
        #endregion
    }

    public partial class RegistrationController
    {
        private bool _IsUsernameAvailable(string username)
        {
            return this._usersRepository.FindByUsername(username) == null;
        }
        private async Task<bool> _IsUsernameAvailableAsync(string username)
        {
            return await this._usersRepository.FindByUsernameAsync(username) == null;
        }
    }
}