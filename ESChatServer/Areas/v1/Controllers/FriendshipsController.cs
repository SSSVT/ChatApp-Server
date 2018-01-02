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
    public sealed partial class FriendshipsController : Controller
    {
        #region Fields
        private readonly IFriendshipsRepository _friendshipsRepository;
        #endregion

        public FriendshipsController(DatabaseContext context)
        {
            this._friendshipsRepository = new FriendshipsRepository(context);
        }

        #region HttpGet (Select)
        [HttpGet]
        public IActionResult GetFriendshipsByUserID([FromRoute] long id)
        {
            try
            {
                return Ok(this._friendshipsRepository.FindByUserID(id));
            }
            catch (Exception ex)
            {
                //TODO: SaveException
                return StatusCode(StatusCodes.Status500InternalServerError);
            }            
        }
        [HttpGet]
        public async Task<IActionResult> GetFriendshipsByUserIDAsync([FromRoute] long id)
        {
            try
            {
                return Ok(await this._friendshipsRepository.FindByUserIDAsync(id));
            }
            catch (Exception ex)
            {
                //TODO: SaveException
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpGet]
        public IActionResult GetFriendshipsRequestsByUserID([FromRoute] long id)
        {
            try
            {
                return Ok(this._friendshipsRepository.FindRequestsByUserID(id));
            }
            catch (Exception ex)
            {
                //TODO: SaveException
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
        [HttpGet]
        public async Task<IActionResult> GetFriendshipsRequestsByUserIDAsync([FromRoute] long id)
        {
            try
            {
                return Ok(await this._friendshipsRepository.FindRequestsByUserIDAsync(id));
            }
            catch (Exception ex)
            {
                //TODO: SaveException
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpGet]
        public IActionResult GetFriendship([FromRoute] Guid id)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                Friendship friendship = this._friendshipsRepository.Find(id);

                if (friendship == null)
                {
                    return NotFound();
                }

                return Ok(friendship);
            }
            catch (Exception ex)
            {
                //TODO: SaveException
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
        [HttpGet]
        public async Task<IActionResult> GetFriendshipAsync([FromRoute] Guid id)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                Friendship friendship = await this._friendshipsRepository.FindAsync(id);

                if (friendship == null)
                {
                    return NotFound();
                }

                return Ok(friendship);
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
        public IActionResult PostFriendship([FromBody] Friendship friendship)
        {
            try
            {
                ModelState.Remove("ID");
                ModelState.Remove("UTCServerReceived");
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                friendship.UTCServerReceived = DateTime.UtcNow;
                this._friendshipsRepository.Add(friendship, true);

                return CreatedAtAction("GetFriendship", new { id = friendship.ID }, friendship);
            }
            catch (Exception ex)
            {
                //TODO: SaveException
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
        [HttpPost]
        public async Task<IActionResult> PostFriendshipAsync([FromBody] Friendship friendship)
        {
            try
            {
                ModelState.Remove("ID");
                ModelState.Remove("UTCServerReceived");
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                friendship.UTCServerReceived = DateTime.UtcNow;
                await this._friendshipsRepository.AddAsync(friendship, true);

                return CreatedAtAction("GetFriendshipAsync", new { id = friendship.ID }, friendship);
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
        public IActionResult PutFriendship([FromRoute] Guid id, [FromBody] Friendship item)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                if (id != item.ID)
                {
                    return BadRequest();
                }

                if (this.FriendshipExists(id))
                {
                    this._friendshipsRepository.Update(item, true);
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
        public async Task<IActionResult> PutFriendshipAsync([FromRoute] Guid id, [FromBody] Friendship friendship)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                if (id != friendship.ID)
                {
                    return BadRequest();
                }

                if (await this.FriendshipExistsAsync(id))
                {
                    await this._friendshipsRepository.UpdateAsync(friendship, true);
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

        #region HttpDelete (Delete)
        [HttpDelete]
        public IActionResult DeleteFriendship([FromRoute] Guid id)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                Friendship friendship = this._friendshipsRepository.Find(id);
                if (friendship == null)
                {
                    return NotFound();
                }

                this._friendshipsRepository.Remove(friendship, true);

                return Ok(friendship);
            }
            catch (Exception ex)
            {
                //TODO: SaveException
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
        [HttpDelete]
        public async Task<IActionResult> DeleteFriendshipAsync([FromRoute] Guid id)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                Friendship friendship = await this._friendshipsRepository.FindAsync(id);
                if (friendship == null)
                {
                    return NotFound();
                }

                await this._friendshipsRepository.RemoveAsync(friendship, true);

                return Ok(friendship);
            }
            catch (Exception ex)
            {
                //TODO: SaveException
                return StatusCode(StatusCodes.Status500InternalServerError);
            }            
        }
        #endregion
    }

    public partial class FriendshipsController
    {
        private bool FriendshipExists(Guid id)
        {
            return this._friendshipsRepository.Exists(id);
        }
        private async Task<bool> FriendshipExistsAsync(Guid id)
        {
            return await this._friendshipsRepository.ExistsAsync(id);
        }
    }
}