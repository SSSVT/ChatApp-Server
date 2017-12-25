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
        #endregion

        public RoomController(DatabaseContext context)
        {
            this._roomsRepository = new RoomsRepository(context);
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
            return StatusCode(StatusCodes.Status501NotImplemented);
        }
        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromBody]Room item)
        {
            return StatusCode(StatusCodes.Status501NotImplemented);
        }
        #endregion

        #region HttpPut (Update)

        #endregion

        #region HttpDelete (Delete)

        #endregion
    }
}