﻿using Microsoft.AspNetCore.Http;
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

        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register([FromBody] AccountUserDTO userdto)
        {
            _logger.LogDebug("Running register account...");
            if (!ModelState.IsValid)
            {
                _logger.LogError("Model is not valid");
                return BadRequest(ModelState);
            }
            try
            {
                AppUser user = new AppUser(userdto);
                user.UserName = userdto.Email;
                var result = await _userManager.CreateAsync(user, userdto.Password);
                if (!result.Succeeded)
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(error.Code, error.Description);
                    }
                    _logger.LogWarning("Unauthorized access");
                    return BadRequest(ModelState);
                }

                await _userManager.AddToRoleAsync(user, "User");
                return Ok();
            }
            catch (Exception)
            {
                return Problem("Something went wrong", statusCode: 500);
            }
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] LoginUserDTO logindto)
        {
            _logger.LogDebug("Running login account...");
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var result = await _signInManager.PasswordSignInAsync(
                    logindto.Email, logindto.Password, false, false);

                if (!result.Succeeded || User.Identity.IsAuthenticated)
                {
                    _logger.LogWarning("Unauthorized access");
                    return Unauthorized(logindto);
                }
                return Ok();
            }
            catch (Exception)
            {
                return Problem("Something went wrong", statusCode: 500);
            }
        }

        [HttpPost]
        [Route("logout")]
        public async Task<IActionResult> Logout()
        {
            _logger.LogDebug("Running logoutaccount...");
            try
            {
                if (User.Identity.IsAuthenticated)
                {
                    await _signInManager.SignOutAsync();
                    return Ok();
                }
                else
                {
                    _logger.LogWarning("Unauthorized access");
                    return Unauthorized();
                }
            }
            catch (Exception)
            {
                return Problem("Something went wrong", statusCode: 500);
            }
        }
    }
}
