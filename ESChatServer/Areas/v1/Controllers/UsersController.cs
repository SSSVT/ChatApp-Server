using ESChatServer.Areas.v1.Models.Application.Objects;
using ESChatServer.Areas.v1.Models.Database;
using ESChatServer.Areas.v1.Models.Database.Entities;
using ESChatServer.Areas.v1.Models.Database.Interfaces;
using ESChatServer.Areas.v1.Models.Database.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ESChatServer.Areas.v1.Controllers
{
    [Produces("application/json")]
    [Area("v1")]
    public sealed partial class UsersController : Controller
    {
        #region Fields
        private readonly IUsersRepository _usersRepository;
        #endregion

        public UsersController(DatabaseContext context)
        {
            this._usersRepository = new UsersRepository(context);
        }

        #region HttpGet (Select)
        [HttpGet]
        public IActionResult GetCurrentUser()
        {
            try
            {
                User user = this._usersRepository.FindByUsername(UserObtainer.GetCurrentUserUsername(User.Claims));

                if (user != null)
                {
                    return Ok(user);
                }
                else
                {
                    return NotFound();
                }
            }
            catch (Exception ex)
            {
                //TODO: SaveException
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
        [HttpGet]
        public async Task<IActionResult> GetCurrentUserAsync()
        {
            try
            {
                User user = await this._usersRepository.FindByUsernameAsync(UserObtainer.GetCurrentUserUsername(User.Claims));

                if (user != null)
                {
                    return Ok(user);
                }
                else
                {
                    return NotFound();
                }
            }
            catch (Exception ex)
            {
                //TODO: SaveException
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpGet]
        public IActionResult FindAll()
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                ICollection<User> result = this._usersRepository.FindAll();
                if (result != null)
                {
                    return Ok(result);
                }
                else
                {
                    return NotFound();
                }
            }
            catch (Exception ex)
            {
                //TODO: SaveException
                return StatusCode(StatusCodes.Status500InternalServerError);
            }            
        }
        [HttpGet]
        public async Task<IActionResult> FindAllAsync()
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                ICollection<User> result = await this._usersRepository.FindAllAsync();
                if (result != null)
                {
                    return Ok(result);
                }
                else
                {
                    return NotFound();
                }
            }
            catch (Exception ex)
            {
                //TODO: SaveException
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpGet]
        public IActionResult FindByUsername(string id)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                User result = this._usersRepository.FindByUsername(id);
                if (result != null)
                {
                    return Ok(result);
                }
                else
                {
                    return BadRequest(id);
                }
            }
            catch (Exception ex)
            {
                //TODO: SaveException
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
        [HttpGet]
        public async Task<IActionResult> FindByUsernameAsync(string id)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                User result = await this._usersRepository.FindByUsernameAsync(id);
                if (result != null)
                {
                    return Ok(result);
                }
                else
                {
                    return BadRequest(id);
                }
            }
            catch (Exception ex)
            {
                //TODO: SaveException
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpGet]
        public IActionResult Detail(long id)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                User result = this._usersRepository.Find(id);
                if (result != null)
                {
                    return Ok(result);
                }
                else
                {
                    return NotFound();
                }
            }
            catch (Exception ex)
            {
                //TODO: SaveException
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
        [HttpGet]
        public async Task<IActionResult> DetailAsync(long id)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                User result = await this._usersRepository.FindAsync(id);
                if (result != null)
                {
                    return Ok(result);
                }
                else
                {
                    return NotFound();
                }
            }
            catch (Exception ex)
            {
                //TODO: SaveException
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
        #endregion

        #region HttpPut (Update)
        [HttpPut]
        public IActionResult Update([FromRoute] long id, [FromBody] User item)
        {
            try
            {
                ModelState.Remove("Password");
                ModelState.Remove("PasswordHash");
                ModelState.Remove("PasswordSalt");
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                if (id != item.ID)
                {
                    return BadRequest();
                }

                if (this.UserExists(id))
                {
                    if (item.Password != null)
                    {
                        item.PasswordSalt = PasswordFactory.GenerateSalt();
                        item.PasswordHash = PasswordFactory.Hash(item.Password, item.PasswordSalt);
                    }

                    this._usersRepository.Update(item, true);
                }
                else
                {
                    return NotFound();
                }

                return NoContent();
            }
            catch (Exception ex)
            {
                //TODO: SaveException
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
        [HttpPut]
        public async Task<IActionResult> UpdateAsync(long id, [FromBody]User item)
        {
            try
            {
                ModelState.Remove("Password");
                ModelState.Remove("PasswordHash");
                ModelState.Remove("PasswordSalt");
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                if (id != item.ID)
                {
                    return BadRequest();
                }

                if (this.UserExists(id))
                {
                    if (item.Password != null)
                    {
                        item.PasswordSalt = PasswordFactory.GenerateSalt();
                        item.PasswordHash = PasswordFactory.Hash(item.Password, item.PasswordSalt);
                    }

                    await this._usersRepository.UpdateAsync(item, true);
                }
                else
                {
                    return NotFound();
                }

                return NoContent();
            }
            catch (Exception ex)
            {
                //TODO: SaveException
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
        #endregion
    }

    public partial class UsersController
    {
        private bool UserExists(long id)
        {
            return this._usersRepository.Exists(id);
        }
        private async Task<bool> UserExistsAsync(long id)
        {
            return await this._usersRepository.ExistsAsync(id);
        }
    }
}