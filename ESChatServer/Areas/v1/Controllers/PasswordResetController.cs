using ESChatServer.Areas.v1.Models.Application.Entities;
using ESChatServer.Areas.v1.Models.Application.Objects;
using ESChatServer.Areas.v1.Models.Database;
using ESChatServer.Areas.v1.Models.Database.Entities;
using ESChatServer.Areas.v1.Models.Database.Interfaces;
using ESChatServer.Areas.v1.Models.Database.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace ESChatServer.Areas.v1.Controllers
{
    [Produces("application/json")]
    [Area("v1")]
    [AllowAnonymous]
    public class PasswordResetController : Controller
    {
        #region Fields
        private readonly IUsersRepository _usersRepository;
        private readonly IPasswordResetRepository _passwordResetRepository;
        #endregion

        public PasswordResetController(DatabaseContext context)
        {
            this._usersRepository = new UsersRepository(context);
            this._passwordResetRepository = new PasswordResetRepository(context);
        }

        /// <summary>
        /// Send email with reset token
        /// </summary>
        /// <param name="id">Username</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> ForgotPasswordAsync([FromRoute] string id)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                User user = await this._usersRepository.FindByUsernameAsync(id);
                if (user != null)
                {
                    PasswordReset reset = new PasswordReset()
                    {
                        UserId = user.ID,
                        UtcIssued = DateTime.UtcNow,
                        UtcExpiration = DateTime.UtcNow.AddMinutes(10),
                        Used = false
                    };

                    await this._passwordResetRepository.AddAsync(reset, true);

                    EmailConfig config = new EmailConfig()
                    {
                        SmtpServer = "smtp.seznam.cz",
                        Port = 25,
                        EnableSsl = false,
                        Username = "chat.projekty.sssvt@seznam.cz",
                        Password = "nHjs3gFYrDTyWnxx"
                    };
                    EmailSender sender = new EmailSender();
                    sender.Send(config, "chat.projekty.sssvt@seznam.cz", user.Email, "Password reset", reset.Id.ToString());

                    return NoContent();
                }

                return BadRequest();
            }
            catch (Exception ex)
            {
                //TODO: SaveException
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpPut]
        public async Task<IActionResult> ResetPasswordAsync([FromRoute] Guid id, [FromBody] PasswordResetModel item)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                if (item.ID != id)
                {
                    return BadRequest();
                }

                PasswordReset reset = await this._passwordResetRepository.FindValidAsync(id);
                if (reset != null)
                {
                    User user = await this._usersRepository.FindAsync(reset.UserId);

                    string salt = PasswordFactory.GenerateSalt();
                    user.PasswordSalt = salt;
                    user.PasswordHash = PasswordFactory.Hash(item.Password, salt);

                    await this._usersRepository.UpdateAsync(user, true);

                    reset.Used = true;
                    reset.UtcExpiration = DateTime.UtcNow;

                    await this._passwordResetRepository.UpdateAsync(reset, true);

                    return NoContent();
                }
                return Unauthorized();
            }
            catch (Exception ex)
            {
                //TODO: SaveException
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}