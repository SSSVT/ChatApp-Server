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
    public class UsersController : Controller
    {
        #region Fields
        protected readonly IUsersRepository _usersRepository;
        #endregion

        public UsersController(DatabaseContext context)
        {
            this._usersRepository = new UsersRepository(context);
        }

        #region HttpGet (Select)
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
                    return NotFound(ModelState);
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
                    return NotFound(ModelState);
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
                    return NotFound(ModelState);
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
                    return NotFound(ModelState);
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
                    return NotFound(ModelState);
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
                    return NotFound(ModelState);
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
        public IActionResult Update(long id, [FromBody]User user)
        {
            try
            {
                //TODO: Add update check (is current user || admin)
                if (!ModelState.IsValid || user.ID != id)
                {
                    return BadRequest(ModelState);
                }

                if (this._usersRepository.Find(user.ID) == null)
                {
                    return NotFound(ModelState);
                }

                this._usersRepository.Update(user, true);
                return Ok();
            }
            catch (Exception ex)
            {
                //TODO: SaveException
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
        [HttpPut]
        public async Task<IActionResult> UpdateAsync(long id, [FromBody]User user)
        {
            try
            {
                //TODO: Add update check (is current user || admin)
                if (!ModelState.IsValid || user.ID != id)
                {
                    return BadRequest(ModelState);
                }

                if (await this._usersRepository.FindAsync(user.ID) == null)
                {
                    return NotFound(ModelState);
                }

                await this._usersRepository.UpdateAsync(user, true);
                return Ok();
            }
            catch (Exception ex)
            {
                //TODO: SaveException
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
        #endregion

        #region HttpDelete (Delete)
        [HttpDelete]
        public IActionResult Delete(long id)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                User user = this._usersRepository.Find(id);
                if (user == null)
                {
                    return NotFound(ModelState);
                }
                else
                {
                    this._usersRepository.Remove(user, true);
                    return Ok();
                }                
            }
            catch (Exception ex)
            {
                //TODO: SaveException
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
        [HttpDelete]
        public async Task<IActionResult> DeleteAsync(long id)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                User user = await this._usersRepository.FindAsync(id);
                if (user == null)
                {
                    return NotFound(ModelState);
                }
                else
                {
                    await this._usersRepository.RemoveAsync(user, true);
                    return Ok();
                }
            }
            catch (Exception ex)
            {
                //TODO: SaveException
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
        #endregion
    }
}