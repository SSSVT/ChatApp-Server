using ESChatServer.Areas.v1.Models.Application.Objects;
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
        private readonly IUsersRepository _usersRepository;
        #endregion

        public FriendshipsController(DatabaseContext context)
        {
            this._friendshipsRepository = new FriendshipsRepository(context);
            this._usersRepository = new UsersRepository(context);
        }

        #region HttpGet (Select)
        [HttpGet]
        public IActionResult GetByUserID([FromRoute] long id)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                return Ok(this._friendshipsRepository.FindByUserID(id));
            }
            catch (Exception ex)
            {
                //TODO: SaveException
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
        public async Task<IActionResult> GetByUserIDAsync([FromRoute] long id)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                return Ok(await this._friendshipsRepository.FindByUserIDAsync(id));
            }
            catch (Exception ex)
            {
                //TODO: SaveException
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpGet]
        public IActionResult GetAcceptedByUserID([FromRoute] long id)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                return Ok(this._friendshipsRepository.FindAcceptedByUserID(id));
            }
            catch (Exception ex)
            {
                //TODO: SaveException
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
        [HttpGet]
        public async Task<IActionResult> GetAcceptedByUserIDAsync([FromRoute] long id)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                return Ok(await this._friendshipsRepository.FindAcceptedByUserIDAsync(id));
            }
            catch (Exception ex)
            {
                //TODO: SaveException
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpGet]
        public IActionResult GetReceivedAndPendingByUserID([FromRoute] long id)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                return Ok(this._friendshipsRepository.FindReceivedAndPendingByUserID(id));
            }
            catch (Exception ex)
            {
                //TODO: SaveException
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
        [HttpGet]
        public async Task<IActionResult> GetReceivedAndPendingByUserIDAsync([FromRoute] long id)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                return Ok(await this._friendshipsRepository.FindReceivedAndPendingByUserIDAsync(id));
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

        [HttpGet]
        public IActionResult IsUserFriend([FromRoute] long id)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                string username = UserObtainer.GetCurrentUserUsername(this.User.Claims);
                User currUser = this._usersRepository.FindByUsername(username);

                return Ok(this._friendshipsRepository.IsFriend(currUser.ID, id));
            }
            catch (Exception ex)
            {
                //TODO: SaveException
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
        [HttpGet]
        public async Task<IActionResult> IsUserFriendAsync([FromRoute] long id)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                string username = UserObtainer.GetCurrentUserUsername(this.User.Claims);
                User currUser = await this._usersRepository.FindByUsernameAsync(username);

                return Ok(await this._friendshipsRepository.IsFriendAsync(currUser.ID, id));
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
                ModelState.Remove("UTCAccepted");
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
                ModelState.Remove("UTCAccepted");
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
        public IActionResult AcceptFriendship([FromRoute] Guid id)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                Friendship friendship = this._friendshipsRepository.Find(id);
                if (friendship != null)
                {
                    string username = UserObtainer.GetCurrentUserUsername(this.User.Claims);
                    User user = this._usersRepository.FindByUsername(username);

                    if (user.ID == friendship.IDRecipient)
                    {
                        friendship.UTCAccepted = DateTime.UtcNow;
                        this._friendshipsRepository.Update(friendship, true);
                    }
                    else
                    {
                        return Unauthorized();
                    }
                }
                else
                {
                    return NotFound(id);
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
        public async Task<IActionResult> AcceptFriendshipAsync([FromRoute] Guid id)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                Friendship friendship = await this._friendshipsRepository.FindAsync(id);
                if (friendship != null)
                {
                    string username = UserObtainer.GetCurrentUserUsername(this.User.Claims);
                    User user = await this._usersRepository.FindByUsernameAsync(username);

                    if (user.ID == friendship.IDRecipient)
                    {
                        friendship.UTCAccepted = DateTime.UtcNow;
                        await this._friendshipsRepository.UpdateAsync(friendship, true);
                    }
                    else
                    {
                        return Unauthorized();
                    }
                }
                else
                {
                    return NotFound(id);
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