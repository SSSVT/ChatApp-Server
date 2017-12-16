using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ESChatServer.Areas.v1.Models.Database;
using ESChatServer.Areas.v1.Models.Database.Interfaces;
using ESChatServer.Areas.v1.Models.Database.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ESChatServer.Areas.v1.Controllers
{
    [Produces("application/json")]
    [Area("v1")]
    public class UsersController : Controller
    {
        #region Fields
        protected readonly IUsersRepository _usersRepository;
        protected readonly DatabaseContext _databaseContext;
        #endregion

        public UsersController(DatabaseContext context)
        {
            this._databaseContext = context;
            this._usersRepository = new UsersRepository(context);
        }        

        [HttpGet]
        public IActionResult Index()
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                return Ok(this._usersRepository.FindAll());
            }
            catch (Exception ex)
            {
                //TODO: SaveException
                return StatusCode(StatusCodes.Status500InternalServerError);
            }            
        }

        [HttpGet]
        public async Task<IActionResult> IndexAsync()
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                return Ok(await this._usersRepository.FindAllAsync());
            }
            catch (Exception ex)
            {
                //TODO: SaveException
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}