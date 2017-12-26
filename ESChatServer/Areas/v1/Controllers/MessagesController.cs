using ESChatServer.Areas.v1.Models.Database;
using ESChatServer.Areas.v1.Models.Database.Entities;
using ESChatServer.Areas.v1.Models.Database.Interfaces;
using ESChatServer.Areas.v1.Models.Database.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ESChatServer.Areas.v1.Controllers
{
    [Produces("application/json")]
    [Area("v1")]
    public class MessagesController : Controller
    {
        #region Fields
        protected readonly IMessagesRepository _messagesRepository;
        #endregion

        public MessagesController(DatabaseContext context)
        {
            this._messagesRepository = new MessagesRepository(context);
        }

        #region HttpGet (Select)
        [HttpGet]
        public IActionResult GetByUserID(long id)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                return Ok(this._messagesRepository.FindByUserID(id));
            }
            catch (Exception ex)
            {
                //TODO: SaveException
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
        [HttpGet]
        public async Task<IActionResult> GetByUserIDAsync(long id)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                return Ok(await this._messagesRepository.FindByUserIDAsync(id));
            }
            catch (Exception ex)
            {
                //TODO: SaveException
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpGet]
        public IActionResult GetByRoomID(long id)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                return Ok(this._messagesRepository.FindByRoomID(id));
            }
            catch (Exception ex)
            {
                //TODO: SaveException
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
        [HttpGet]
        public async Task<IActionResult> GetByRoomIDAsync(long id)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                return Ok(await this._messagesRepository.FindByRoomIDAsync(id));
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
        public IActionResult Create([FromBody]Message item)
        {
            try
            {
                ModelState.Remove("ID");
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                this._messagesRepository.Add(item, true);
                return Ok();
            }
            catch (Exception ex)
            {
                //TODO: SaveException
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromBody]Message item)
        {
            try
            {
                ModelState.Remove("ID");
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                await this._messagesRepository.AddAsync(item, true);
                return Ok();
            }
            catch (Exception ex)
            {
                //TODO: SaveException
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
        #endregion

        #region HttpPut (Update)
        //Not supported in this version
        #endregion

        #region HttpDelete (Delete)
        //Not supported in this version
        #endregion
    }
}