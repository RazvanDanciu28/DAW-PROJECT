using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using AngularApp1.Server.Models;
using AngularApp1.Server.Services.UnitOfWorkService;


namespace AngularApp1.Server.Controllers
{
    [Route("app/[controller]")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly ILogger<AccountsController> _logger;

        public AccountsController(UserManager<AppUser> userManager,
                               SignInManager<AppUser> signInManager,
                               ILogger<AccountsController> logger)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
        }
    }
}
