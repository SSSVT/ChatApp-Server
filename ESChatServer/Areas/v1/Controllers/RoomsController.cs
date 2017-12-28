using ESChatServer.Areas.v1.Models.Application.Objects;
using ESChatServer.Areas.v1.Models.Database;
using ESChatServer.Areas.v1.Models.Database.Entities;
using ESChatServer.Areas.v1.Models.Database.Interfaces;
using ESChatServer.Areas.v1.Models.Database.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace ESChatServer.Areas.v1.Controllers
{
    [Produces("application/json")]
    [Area("v1")]
    public class RoomsController : Controller
    {
        #region Fields
        protected readonly IRoomsRepository _roomsRepository;
        protected readonly IUsersRepository _usersRepository;
        #endregion

        public RoomsController(DatabaseContext context)
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

                return Ok(this._roomsRepository.FindAll());
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

                return Ok(await this._roomsRepository.FindAllAsync());
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
                User user = this._usersRepository.FindByUsername(UserObtainer.GetCurrentUserUsername(User.Claims));

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
                User user = this._usersRepository.FindByUsername(UserObtainer.GetCurrentUserUsername(User.Claims));

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
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                //Check authenticated user ID
                User user = this._usersRepository.FindByUsername(UserObtainer.GetCurrentUserUsername(User.Claims));

                if (item.IDOwner == user.ID)
                {
                    this._roomsRepository.Update(item, true);
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
        [HttpPut]
        public async Task<IActionResult> UpdateAsync([FromBody]Room item)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                //Check authenticated user ID
                User user = this._usersRepository.FindByUsername(UserObtainer.GetCurrentUserUsername(User.Claims));

                if (item.IDOwner == user.ID)
                {
                    await this._roomsRepository.UpdateAsync(item, true);
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

        #region HttpDelete (Delete)
        [HttpDelete]
        public IActionResult Delete(int id)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                //Check authenticated user ID
                User user = this._usersRepository.FindByUsername(UserObtainer.GetCurrentUserUsername(User.Claims));
                Room item = this._roomsRepository.Find(id);

                if (item.IDOwner == user.ID)
                {
                    this._roomsRepository.Remove(item, true);
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
        [HttpDelete]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                //Check authenticated user ID
                User user = this._usersRepository.FindByUsername(UserObtainer.GetCurrentUserUsername(User.Claims));
                Room item = await this._roomsRepository.FindAsync(id);

                if (item.IDOwner == user.ID)
                {
                    await this._roomsRepository.RemoveAsync(item, true);
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
    }
}