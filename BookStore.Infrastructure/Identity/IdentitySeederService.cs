using BookStore.Core.Domain.IdentityEntities;
using BookStore.Core.Enums;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Infrastructure.Identity
{
	public class IdentitySeederService : IHostedService
	{
		private readonly IServiceProvider _serviceProvider;

		public IdentitySeederService(IServiceProvider serviceProvider)
		{
			_serviceProvider = serviceProvider;
		}

		public async Task StartAsync(CancellationToken cancellationToken)
		{
			using var scope = _serviceProvider.CreateScope();
			var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<ApplicationRole>>();
			var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();

			await SeedRolesAndAdminUser(roleManager, userManager);
		}

		public Task StopAsync(CancellationToken cancellationToken) => Task.CompletedTask;

		private static async Task SeedRolesAndAdminUser(RoleManager<ApplicationRole> roleManager, UserManager<ApplicationUser> userManager)
		{
			// Ensure the roles exist
			if (!await roleManager.RoleExistsAsync(UserRoleOptions.Admin.ToString()))
			{
				await roleManager.CreateAsync(new ApplicationRole { Name = UserRoleOptions.Admin.ToString() });
			}

			if (!await roleManager.RoleExistsAsync(UserRoleOptions.User.ToString()))
			{
				await roleManager.CreateAsync(new ApplicationRole { Name = UserRoleOptions.User.ToString() });
			}

			// Create the admin user if it doesn't already exist
			var adminUser = await userManager.FindByEmailAsync("admin@example.com");
			if (adminUser == null)
			{
				adminUser = new ApplicationUser
				{
					UserName = "admin@example.com",
					Email = "admin@example.com",
					EmailConfirmed = true
				};

				var result = await userManager.CreateAsync(adminUser, "Admin@123");

				if (result.Succeeded)
				{
					await userManager.AddToRoleAsync(adminUser, UserRoleOptions.Admin.ToString());
				}
			}
		}
	}

}
