using BookStore.Core.Domain.IdentityEntities;
using BookStore.Core.DTO;
using BookStore.Core.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.WebAPI.Controllers.v1
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IJwtService _jwtService;

        public AccountController(
            UserManager<ApplicationUser> userManager, 
            SignInManager<ApplicationUser> signInManager,
            IJwtService jwtService)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _jwtService = jwtService;
        }


        [HttpPost("register")]
        public async Task<ActionResult<AuthenticationResponseDTO>> PostRegister(RegisterDTO registerDTO)
        {
            var newUser = new ApplicationUser { UserName = registerDTO.UserName, Email = registerDTO.Email };
            var result = await _userManager.CreateAsync(newUser, registerDTO.Password);

            if (result.Succeeded)
            {
                await _signInManager.SignInAsync(newUser, isPersistent: true);
                var authResponse = _jwtService.CreateJwtToken(newUser);
                return Ok(authResponse);
            }

            return Problem(
                title: "User creation failed",
                detail: string.Join(", ", result.Errors.Select(e => e.Description)),
                statusCode: 400
                );
        }


        [HttpPost("login")]
        public async Task<ActionResult<ApplicationUser>> PostLogin(SignInDTO signInDTO)
        {
            var result = await _signInManager.PasswordSignInAsync(signInDTO.UserName, signInDTO.Password, isPersistent: true, lockoutOnFailure: false);

            if (result.Succeeded)
            {
                var user = _userManager.FindByNameAsync(signInDTO.UserName);

                return Ok(user);
            }

            return Problem("Invalid username or password");
        }


        [HttpPost("logout")]
        public async Task<ActionResult> GetLogout()
        {
            await _signInManager.SignOutAsync();
            return NoContent();
        }


        [Authorize]
        [HttpPost("sayHello")]
        public async Task<ActionResult> GetHello()
        {

            return Ok("Hello");
        }

    }
}
