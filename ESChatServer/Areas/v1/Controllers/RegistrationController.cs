using ESChatServer.Areas.v1.Models.Application.Entities;
using ESChatServer.Areas.v1.Models.Application.Objects;
using ESChatServer.Areas.v1.Models.Database;
using ESChatServer.Areas.v1.Models.Database.Interfaces;
using ESChatServer.Areas.v1.Models.Database.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using Entities = ESChatServer.Areas.v1.Models.Database.Entities;

namespace ESChatServer.Areas.v1.Controllers
{
    [Produces("application/json")]
    [Area("v1")]
    public class RegistrationController : Controller
    {
        #region Fields
        protected readonly IUsersRepository _usersRepository;
        protected readonly DatabaseContext _databaseContext;
        #endregion

        public RegistrationController(DatabaseContext context)
        {
            this._databaseContext = context;
            this._usersRepository = new UsersRepository(context);
        }

        [HttpPost]
        public IActionResult Register([FromBody]Registration registration)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                string salt = PasswordFactory.GenerateSalt(128);
                Entities.User user = new Entities.User()
                {
                    FirstName = registration.FirstName,
                    MiddleName = registration.MiddleName,
                    LastName = registration.LastName,
                    Birthday = registration.Birthday,
                    Gender = registration.Gender,
                    Username = registration.Username,
                    PasswordHash = PasswordFactory.Hash(registration.Password, salt),
                    PasswordSalt = salt,
                    UTCRegistrationDate = DateTime.UtcNow
                };
                this._usersRepository.Add(user, true);

                return Ok();
            }
            catch (Exception ex)
            {
                //TODO: SaveException
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
        [HttpPost]
        public async Task<IActionResult> RegisterAsync([FromBody]Registration registration)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                string salt = PasswordFactory.GenerateSalt(128);
                Entities.User user = new Entities.User()
                {
                    FirstName = registration.FirstName,
                    MiddleName = registration.MiddleName,
                    LastName = registration.LastName,
                    Birthday = registration.Birthday,
                    Gender = registration.Gender,
                    Username = registration.Username,
                    PasswordHash = PasswordFactory.Hash(registration.Password, salt),
                    PasswordSalt = salt,
                    UTCRegistrationDate = DateTime.UtcNow
                };
                await this._usersRepository.AddAsync(user, true);

                return Ok();
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

                return Ok(this._usersRepository.FindByUsername(id) == null);
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

                return Ok(await this._usersRepository.FindByUsernameAsync(id) == null);
            }
            catch (Exception ex)
            {
                //TODO: SaveException
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}