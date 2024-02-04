using AngularApp1.Server.Models;
using AngularApp1.Server.Services.UnitOfWorkService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Linq;
using AngularApp1.Server.DataContext;
using AngularApp1.Server.Models;
using AngularApp1.Server.Services.UnitOfWorkService;

namespace AngularApp1.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UserController : ControllerBase
    {
        private readonly IUnitOfWorkService _unitOfWork;
        private readonly ILogger<UserController> _logger;
        private const string errorDbMessage = "DB Error: Cant find user with this id";

        public UserController(IUnitOfWorkService unitOfWork,
                                 ILogger<UserController> logger)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetUsers()
        {
            _logger.LogDebug("Running getting all users (ADMIN ONLY)...");
            var users = (await _unitOfWork.Users.GetAll()).Select(a => new ApplicationUserDTO(a)).ToList();
            return Ok(users);
        }
        [HttpGet("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<ApplicationUserDTO>> GetUser(string id)
        {
            _logger.LogDebug("Running getting a user (ADMIN ONLY)...");
            var user = await _unitOfWork.Users.GetById(id);

            if (user == null)
            {
                _logger.LogError("DB Error!");
                return NotFound(errorDbMessage);
            }

            return new ApplicationUserDTO(user);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteUser(string id)
        {
            _logger.LogDebug("Running deleting a user (ADMIN ONLY)...");
            var user = await _unitOfWork.Users.GetById(id);

            if (user == null)
            {
                _logger.LogError("DB Error!");
                return NotFound(errorDbMessage);
            }

            if (user.Id == _unitOfWork.getUserManager().GetUserId(User))
            {
                _logger.LogWarning("Unauthorized access");
                return Unauthorized();
            }

            await _unitOfWork.Users.Delete(user);
            _unitOfWork.Save();
            return Ok(user);
        }

    }
}