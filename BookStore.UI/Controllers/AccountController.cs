using BookStore.Core.Domain.IdentityEntities;
using BookStore.Core.DTO;
using BookStore.Core.Enums;
using BookStore.Core.Interfaces.Services;
using BookStore.Core.Services;
using BookStore.UI.Filters.ActionFilters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SignInResult = Microsoft.AspNetCore.Identity.SignInResult;



namespace BookStore.UI.Controllers
{
    public class AccountController : Controller
	{
		private readonly IUserAccountService _userAccountService;
		private readonly IUserRoleService _userRoleService;

		private readonly ILogger<AccountController> _logger;

		// constructor
		public AccountController(
			IUserAccountService userAccountService,
			IUserRoleService userRoleService,
			ILogger<AccountController> logger
			)
		{
			_userAccountService = userAccountService;
			_userRoleService = userRoleService;
			_logger = logger;
		}

		#region Register
		// *************************************************	Register	*************************************************
		[Route("/register", Name = "Register")]
		[HttpGet]
		[RedirectAuthenticatedFilter]
		public IActionResult Register()
		{
			return View();
		}



		[Route("/register", Name = "Register")]
		[HttpPost]
		[RedirectAuthenticatedFilter]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Register(RegisterDTO registerDTO)
		{
			if (registerDTO == null)
			{
				ModelState.AddModelError(string.Empty, "Invalid register attempt.");
				return View();
			}

			if (!ModelState.IsValid)
			{
				return View(registerDTO);
			}

			// Register the user
			var result = await _userAccountService.RegisterUserAsync(registerDTO);

			if (result.Succeeded)
			{
				var user = await _userAccountService.FindByUserNameAsync(registerDTO.UserName);

				if (user == null)
				{
					ModelState.AddModelError(string.Empty, "User not found after registration.");
					return View(registerDTO);
				}

				// Assign the default role using RoleManagementService
				var roleResult = await _userRoleService.AssignDefaultRoleAsync(user);

				if (!roleResult.Succeeded)
				{
					// Handle role assignment errors
					foreach (var error in roleResult.Errors)
					{
						ModelState.AddModelError(string.Empty, $"Role assignment failed: {error.Description}");
					}
					return View(registerDTO); // Return the view with errors
				}

				// Sign in the user
				await _userAccountService.SignInUserAsync(user, isPersistent: true);
				return RedirectToAction("Index", "Home");
			}

			// Handle registration errors
			foreach (var error in result.Errors)
			{
				ModelState.AddModelError(string.Empty, error.Description);
			}

			return View(registerDTO);
		}

		#endregion


		#region SignIn
		// *************************************************	SignIn	*************************************************

		[HttpGet]
		[Route("/SignIn", Name = "SignIn")]
		[RedirectAuthenticatedFilter]
		public IActionResult SignIn()
		{
			return View();
		}


		[HttpPost]
		[Route("/SignIn", Name = "SignIn")]
		[RedirectAuthenticatedFilter]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> SignIn(SignInDTO signInDTO)
		{
			// Null check to handle cases where the model binding might fail
			if (signInDTO == null)
			{
				ModelState.AddModelError(string.Empty, "Invalid sign in attempt.");
				return View();
			}

			if (!ModelState.IsValid)
			{
				return View(signInDTO);
			}

			var result = await _userAccountService.SignInWithPasswordAsync(signInDTO);

			switch (result)
			{
				case SignInResult success when success.Succeeded:
					// Sign-in was successful
					return RedirectToAction("Index", "Home");

				case SignInResult lockout when lockout.IsLockedOut:
					// User is locked out
					ModelState.AddModelError(string.Empty, "This account is locked out.");
					return View(signInDTO);

				case SignInResult failure when failure.IsNotAllowed:
					// User is not allowed to sign in (e.g., email not confirmed)
					ModelState.AddModelError(string.Empty, "You must confirm your email address before you can log in.");
					return View(signInDTO);

				case SignInResult failure:
					// Sign-in failed due to invalid credentials
					ModelState.AddModelError(string.Empty, "Invalid login attempt.");
					return View(signInDTO);

				default:
					// Handle any unexpected result
					ModelState.AddModelError(string.Empty, "An unknown error occurred.");
					return View(signInDTO);
			}

		}

		#endregion


		#region SignOut

		[HttpPost]
		[Authorize]
		public async Task<IActionResult> SignOutUser()
		{
			await _userAccountService.SignOutAsync();
			return RedirectToAction("Index", "Home");
		}

		#endregion
	}
}
