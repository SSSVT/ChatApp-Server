using ESChatServer.Areas.v1.Models.Database;
using ESChatServer.Areas.v1.Models.Database.Entities;
using ESChatServer.Areas.v1.Models.Database.Interfaces;
using ESChatServer.Areas.v1.Models.Database.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ESChatServer.Areas.v1.Controllers
{
    [Produces("application/json")]
    [Area("v1")]
    public sealed class MessagesController : Controller
    {
        #region Fields
        private readonly IMessagesRepository _messagesRepository;
        #endregion

        public MessagesController(DatabaseContext context)
        {
            this._messagesRepository = new MessagesRepository(context);
        }

        #region HttpGet (Select)
        //TODO: Is these methods userful?
        [HttpGet]
        public IActionResult GetByUserID([FromRoute] long id)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                ICollection<Message> messages = this._messagesRepository.FindByUserID(id);
                if (messages.Count == 0)
                {
                    return NotFound(id);
                }

                return Ok(messages);
            }
            catch (Exception ex)
            {
                //TODO: SaveException
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
        [HttpGet]
        public async Task<IActionResult> GetByUserIDAsync([FromRoute] long id)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                ICollection<Message> messages = await this._messagesRepository.FindByUserIDAsync(id);
                if (messages.Count == 0)
                {
                    return NotFound();
                }

                return Ok(messages);
            }
            catch (Exception ex)
            {
                //TODO: SaveException
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpGet]
        public IActionResult GetByRoomID([FromRoute] long id, [FromBody] DateTime lastMessageTime)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                ICollection<Message> messages = this._messagesRepository.FindByRoomID(id, lastMessageTime);
                if (messages.Count == 0)
                {
                    return NotFound();
                }

                return Ok(messages);
            }
            catch (Exception ex)
            {
                //TODO: SaveException
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
        [HttpGet]
        public async Task<IActionResult> GetByRoomIDAsync([FromRoute] long id, [FromBody] DateTime lastMessageTime)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                ICollection<Message> messages = await this._messagesRepository.FindByRoomIDAsync(id, lastMessageTime);
                if (messages.Count == 0)
                {
                    return NotFound();
                }

                return Ok(messages);
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
        public IActionResult Create([FromBody] Message item)
        {
            try
            {
                ModelState.Remove("ID");
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                this._messagesRepository.Add(item, true);
                return StatusCode(StatusCodes.Status201Created);
            }
            catch (Exception ex)
            {
                //TODO: SaveException
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromBody] Message item)
        {
            try
            {
                ModelState.Remove("ID");
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                await this._messagesRepository.AddAsync(item, true);
                return StatusCode(StatusCodes.Status201Created);
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