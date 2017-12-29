using ESChatServer.Areas.v1.Models.Database;
using ESChatServer.Areas.v1.Models.Database.Entities;
using ESChatServer.Areas.v1.Models.Database.Interfaces;
using ESChatServer.Areas.v1.Models.Database.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace ESChatServer.Areas.v1.Controllers
{
    [Produces("application/json")]
    [Area("v1")]
    public sealed partial class RoomsController : Controller
    {
        #region Fields
        private readonly IRoomsRepository _roomsRepository;
        private readonly IUsersRepository _usersRepository;
        #endregion

        public RoomsController(DatabaseContext context)
        {
            this._roomsRepository = new RoomsRepository(context);
            this._usersRepository = new UsersRepository(context);
        }

        #region HttpGet (Select)
        [HttpGet]
        public IActionResult Find([FromRoute] long id)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                Room result = this._roomsRepository.Find(id);
                if (result == null)
                {
                    return NotFound(id);
                }
                return Ok(result);
            }
            catch (Exception ex)
            {
                //TODO: SaveException
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
        [HttpGet]
        public async Task<IActionResult> FindAsync([FromRoute] long id)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                Room result = await this._roomsRepository.FindAsync(id);
                if (result == null)
                {
                    return NotFound(id);
                }
                return Ok(result);
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

        [HttpGet]
        public IActionResult FindByUserID(long id)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                return Ok(this._roomsRepository.FindByUserID(id));
            }
            catch (Exception ex)
            {
                //TODO: SaveException
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
        [HttpGet]
        public async Task<IActionResult> FindByUserIDAsync(long id)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                return Ok(await this._roomsRepository.FindByUserIDAsync(id));
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
        public IActionResult Create([FromBody] Room item)
        {
            try
            {
                ModelState.Remove("ID"); //Remove property from model validation
                ModelState.Remove("UTCCreationDate");
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                item.UTCCreationDate = DateTime.UtcNow;

                this._roomsRepository.Add(item, true);

                return CreatedAtAction("Find", new { id = item.ID }, item);
            }
            catch (Exception ex)
            {
                //TODO: SaveException
                return StatusCode(StatusCodes.Status500InternalServerError);
            }            
        }
        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromBody] Room item)
        {
            try
            {
                ModelState.Remove("ID"); //Remove property from model validation
                ModelState.Remove("UTCCreationDate");
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                item.UTCCreationDate = DateTime.UtcNow;

                await this._roomsRepository.AddAsync(item, true);

                return CreatedAtAction("FindAsync", new { id = item.ID }, item);
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
        public IActionResult Update([FromRoute] long id, [FromBody] Room item)
        {
            try
            {
                ModelState.Remove("UTCCreationDate");
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                if (id != item.ID)
                {
                    return BadRequest();
                }

                if (this.RoomExists(id))
                {
                    this._roomsRepository.Update(item, true);
                }
                else
                {
                    return BadRequest(new { id, item });
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
        public async Task<IActionResult> UpdateAsync([FromRoute] long id, [FromBody] Room item)
        {
            try
            {
                ModelState.Remove("UTCCreationDate");
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                if (id != item.ID)
                {
                    return BadRequest();
                }

                if (await this.RoomExistsAsync(id))
                {
                    await this._roomsRepository.UpdateAsync(item, true);
                }
                else
                {
                    return BadRequest(new { id, item });
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

        #region HttpDelete (Delete)
        [HttpDelete]
        public IActionResult Delete([FromRoute] long id)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                Room item = this._roomsRepository.Find(id);
                if (item == null)
                {
                    return BadRequest(id);
                }

                this._roomsRepository.Remove(item, true);

                return Ok(item);
            }
            catch (Exception ex)
            {
                //TODO: SaveException
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
        [HttpDelete]
        public async Task<IActionResult> DeleteAsync([FromRoute] long id)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                Room item = await this._roomsRepository.FindAsync(id);
                if (item == null)
                {
                    return BadRequest(id);
                }

                await this._roomsRepository.RemoveAsync(item, true);

                return Ok(item);
            }
            catch (Exception ex)
            {
                //TODO: SaveException
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
        #endregion
    }

    public partial class RoomsController
    {
        private bool RoomExists(long id)
        {
            return this._roomsRepository.Exists(id);
        }
        private async Task<bool> RoomExistsAsync(long id)
        {
            return await this._roomsRepository.ExistsAsync(id);
        }
    }
}