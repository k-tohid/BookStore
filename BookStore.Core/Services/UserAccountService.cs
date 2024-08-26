using BookStore.Core.Domain.IdentityEntities;
using BookStore.Core.DTO;
using BookStore.Core.Enums;
using BookStore.Core.Interfaces;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Core.Services
{
	public class UserAccountService : IUserAccountService
	{
		private readonly UserManager<ApplicationUser> _userManager;
		private readonly SignInManager<ApplicationUser> _signInManager;
		private readonly RoleManager<ApplicationRole> _roleManager;

		public UserAccountService(
		UserManager<ApplicationUser> userManager,
			SignInManager<ApplicationUser> signInManager, 
			RoleManager<ApplicationRole> roleManager)
		{
			_userManager = userManager;
			_signInManager = signInManager;
			_roleManager = roleManager;
		}


		// *************************************************	SignInUserAsync	*************************************************
		public async Task SignInUserAsync(ApplicationUser user, bool isPersistent = true)
		{
			await _signInManager.SignInAsync(user, isPersistent);
		}


		// *************************************************	RegisterUserAsync	*************************************************
		public async Task<IdentityResult> RegisterUserAsync(RegisterDTO registerDTO)
		{
			var newUser = new ApplicationUser
			{
				UserName = registerDTO.UserName,
				Email = registerDTO.Email,
				Address = registerDTO.Address,
			};

			var result = await _userManager.CreateAsync(newUser, registerDTO.Password);

			return result;
		}


		public async Task<SignInResult> SignInWithPasswordAsync(SignInDTO signInDTO)
		{
			return await _signInManager.PasswordSignInAsync(
				userName: signInDTO.UserName,
				password: signInDTO.Password,
				isPersistent: signInDTO.RememberMe,
				lockoutOnFailure: false);
		}


		public async Task<ApplicationUser?> FindByUserNameAsync(string userName)
		{
			return await _userManager.FindByNameAsync(userName);
		}


		public async Task<ApplicationUser?> FindByEmailAsync(string email)
		{
			return await _userManager.FindByEmailAsync(email);
		}


		public async Task SignOutAsync()
		{
			await _signInManager.SignOutAsync();
		}

		public Task<IdentityResult> ChangePasswordAsync(ApplicationUser user, string currentPassword, string newPassword)
		{
			throw new NotImplementedException();
		}

		public Task<IdentityResult> ConfirmEmailAsync(ApplicationUser user, string token)
		{
			throw new NotImplementedException();
		}

		public Task<string> GenerateEmailConfirmationTokenAsync(ApplicationUser user)
		{
			throw new NotImplementedException();
		}

		public Task<IdentityResult> ResetPasswordAsync(ApplicationUser user, string token, string newPassword)
		{
			throw new NotImplementedException();
		}

		public Task<string> GeneratePasswordResetTokenAsync(ApplicationUser user)
		{
			throw new NotImplementedException();
		}

		
	}
}
