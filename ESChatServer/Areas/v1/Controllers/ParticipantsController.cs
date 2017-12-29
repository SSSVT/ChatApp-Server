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
    public sealed partial class ParticipantsController : Controller
    {
        #region Fields
        private readonly IParticipantsRepository _participantsRepository;
        #endregion

        public ParticipantsController(DatabaseContext context)
        {
            this._participantsRepository = new ParticipantsRepository(context);
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

                ICollection<Participant> participants = this._participantsRepository.FindByUserID(id);
                return Ok(participants);
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

                ICollection<Participant> participants = await this._participantsRepository.FindByUserIDAsync(id);
                return Ok(participants);
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

                ICollection<Participant> participants = this._participantsRepository.FindByRoomID(id);
                return Ok(participants);
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

                ICollection<Participant> participants = await this._participantsRepository.FindByRoomIDAsync(id);
                return Ok(participants);
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
        public IActionResult PostParticipant([FromBody] Participant item)
        {
            try
            {
                ModelState.Remove("ID");
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                this._participantsRepository.Add(item, true);

                return StatusCode(StatusCodes.Status201Created, item);
            }
            catch (Exception ex)
            {
                //TODO: SaveException
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
        [HttpPost]
        public async Task<IActionResult> PostParticipantAsync([FromBody] Participant item)
        {
            try
            {
                ModelState.Remove("ID");
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                await this._participantsRepository.AddAsync(item, true);

                return StatusCode(StatusCodes.Status201Created, item);
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
        public IActionResult DeleteParticipant([FromRoute] Guid id)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                Participant item = this._participantsRepository.Find(id);
                this._participantsRepository.Remove(item, true);

                return Ok(item);
            }
            catch (Exception ex)
            {
                //TODO: SaveException
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
        [HttpDelete]
        public async Task<IActionResult> DeleteParticipantAsync([FromRoute] Guid id)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                Participant item = await this._participantsRepository.FindAsync(id);
                await this._participantsRepository.RemoveAsync(item, true);

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

    public partial class ParticipantsController
    {
        private bool ParticipantExists(Guid id)
        {
            return this._participantsRepository.Exists(id);
        }
        private async Task<bool> ParticipantExistsAsync(Guid id)
        {
            return await this._participantsRepository.ExistsAsync(id);
        }
    }
}