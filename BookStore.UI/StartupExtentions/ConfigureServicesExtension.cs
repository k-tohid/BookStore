using BookStore.Core.Domain.IdentityEntities;
using BookStore.Core.Interfaces.Services;
using BookStore.Core.Services;
using BookStore.Infrastructure.DbContext;
using BookStore.Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BookStore.UI.StartupExtentions
{
    public static class ConfigureServicesExtension
	{
		public static IServiceCollection ConfigureServices(this IServiceCollection services, IConfiguration configuration)
		{
			// Add services to the container.
			services.AddControllersWithViews();

			// add Services into IoC Container
			services.AddScoped<IUserAccountService, UserAccountService>();
			services.AddScoped<IUserRoleService, UserRoleService>();


			services.AddDbContext<ApplicationDbContext>(options =>
			{
				options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
			});

			services.AddIdentity<ApplicationUser, ApplicationRole>(options =>
			{
				options.Password.RequiredLength = 5;
				options.Password.RequiredUniqueChars = 1;

				options.Password.RequireDigit = false;
				options.Password.RequireLowercase = false;
				options.Password.RequireUppercase = false;
				options.Password.RequireNonAlphanumeric = false;
			})
				.AddEntityFrameworkStores<ApplicationDbContext>()
				.AddDefaultTokenProviders()
				.AddUserStore<UserStore<ApplicationUser, ApplicationRole, ApplicationDbContext, int>>()
				.AddRoleStore<RoleStore<ApplicationRole, ApplicationDbContext, int>>();

			// Register the RoleSeederService from Infrastructure
			services.AddHostedService<IdentitySeederService>();


			return services;
		}
	}
}
