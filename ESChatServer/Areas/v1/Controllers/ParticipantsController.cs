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
    public class ParticipantsController : Controller
    {
        #region Fields
        protected readonly IParticipantsRepository _participantsRepository;
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

                return Ok(this._participantsRepository.FindByUserID(id));
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

                return Ok(await this._participantsRepository.FindByUserIDAsync(id));
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

                return Ok(this._participantsRepository.FindByRoomID(id));
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

                return Ok(await this._participantsRepository.FindByRoomIDAsync(id));
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
        public IActionResult Create([FromBody]Participant item)
        {
            try
            {
                ModelState.Remove("ID");
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                this._participantsRepository.Add(item, true);

                return Ok();
            }
            catch (Exception ex)
            {
                //TODO: SaveException
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromBody]Participant item)
        {
            try
            {
                ModelState.Remove("ID");
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                await this._participantsRepository.AddAsync(item, true);

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
        [HttpPut]
        public IActionResult Update([FromBody]Participant item)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                if (this._participantsRepository.Find(item.ID) != null)
                {
                    this._participantsRepository.Update(item, true);
                    return Ok();
                }
                else
                {
                    return BadRequest(ModelState);
                }
            }
            catch (Exception ex)
            {
                //TODO: SaveException
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
        [HttpPut]
        public async Task<IActionResult> UpdateAsync([FromBody]Participant item)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                if (await this._participantsRepository.FindAsync(item.ID) != null)
                {
                    await this._participantsRepository.UpdateAsync(item, true);
                    return Ok();
                }
                else
                {
                    return BadRequest(ModelState);
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
        public IActionResult Delete(Guid id)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                Participant item = this._participantsRepository.Find(id);
                this._participantsRepository.Remove(item, true);

                return Ok();
            }
            catch (Exception ex)
            {
                //TODO: SaveException
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
        [HttpDelete]
        public async Task<IActionResult> DeleteAsync(Guid id)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                Participant item = await this._participantsRepository.FindAsync(id);
                await this._participantsRepository.RemoveAsync(item, true);

                return Ok();
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