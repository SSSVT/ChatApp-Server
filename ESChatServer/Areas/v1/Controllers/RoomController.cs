using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ESChatServer.Areas.v1.Models.Database;
using ESChatServer.Areas.v1.Models.Database.Entities;
using ESChatServer.Areas.v1.Models.Database.Interfaces;
using ESChatServer.Areas.v1.Models.Database.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ESChatServer.Areas.v1.Controllers
{
    [Produces("application/json")]
    [Area("v1")]
    public class RoomController : Controller
    {
        #region Fields
        protected readonly IRoomsRepository _roomsRepository;
        protected readonly IUsersRepository _usersRepository;
        #endregion

        public RoomController(DatabaseContext context)
        {
            this._roomsRepository = new RoomsRepository(context);
            this._usersRepository = new UsersRepository(context);
        }

        #region HttpGet (Select)
        [HttpGet]
        public IActionResult Find(long id)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                Room result = this._roomsRepository.Find(id);
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
        public async Task<IActionResult> FindAsync(long id)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                Room result = await this._roomsRepository.FindAsync(id);
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

        #region HttpPost (Create)
        [HttpPost]
        public IActionResult Create([FromBody]Room item)
        {
            try
            {
                ModelState.Remove("ID"); //Remove property from model validation
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                //Check authenticated user ID
                string username = User.Claims.Where(c => c.Type == "sub").Single().Value;
                User user = this._usersRepository.FindByUsername(username);

                if (item.IDOwner == user.ID)
                {
                    this._roomsRepository.Add(item, true);
                    return Ok();
                }
                else
                {
                    return Unauthorized();
                }                
            }
            catch (Exception ex)
            {
                //TODO: SaveException
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
            
        }
        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromBody]Room item)
        {
            try
            {
                ModelState.Remove("ID"); //Remove property from model validation
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                //Check authenticated user ID
                string username = User.Claims.Where(c => c.Type == "sub").Single().Value;
                User user = await this._usersRepository.FindByUsernameAsync(username);

                if (item.IDOwner == user.ID)
                {
                    await this._roomsRepository.AddAsync(item, true);
                    return Ok();
                }
                else
                {
                    return Unauthorized();
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
        public IActionResult Update([FromBody]Room item)
        {
            return StatusCode(StatusCodes.Status501NotImplemented);
        }
        [HttpPut]
        public async Task<IActionResult> UpdateAsync([FromBody]Room item)
        {
            return StatusCode(StatusCodes.Status501NotImplemented);
        }
        #endregion

        #region HttpDelete (Delete)
        [HttpDelete]
        public IActionResult Delete(int id)
        {
            return StatusCode(StatusCodes.Status501NotImplemented);
        }
        [HttpDelete]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            return StatusCode(StatusCodes.Status501NotImplemented);
        }
        #endregion
    }
}