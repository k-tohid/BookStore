using BookStore.Core.Domain.IdentityEntities;
using BookStore.Core.Enums;
using BookStore.Core.Interfaces.Services;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Core.Services
{
    public class UserRoleService : IUserRoleService
	{
		private readonly UserManager<ApplicationUser> _userManager;

        public UserRoleService(UserManager<ApplicationUser> userManager)
        {
			_userManager = userManager;
        }


        public async Task<IdentityResult> AssignDefaultRoleAsync(ApplicationUser user)
		{
			return await _userManager.AddToRoleAsync(user, UserRoleOptions.User.ToString());
		}
	}
}
