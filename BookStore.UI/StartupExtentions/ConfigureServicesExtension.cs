using Asp.Versioning;
using BookStore.Core.Domain.IdentityEntities;
using BookStore.Core.Interfaces.Repositories;
using BookStore.Core.Interfaces.Services;
using BookStore.Core.Services;
using BookStore.Infrastructure.DbContext;
using BookStore.Infrastructure.Identity;
using BookStore.Infrastructure.Repositories;
using BookStore.Infrastructure.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace BookStore.UI.StartupExtentions
{
    public static class ConfigureServicesExtension
	{
		public static IServiceCollection ConfigureServices(this IServiceCollection services, IConfiguration configuration)
		{
            services.AddControllers();
            services.AddControllersWithViews();

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen(c =>
			{
                //c.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, "api.xml"));
                c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo { Title = "API V1", Version = "v1" });
                c.SwaggerDoc("v2", new Microsoft.OpenApi.Models.OpenApiInfo { Title = "API V2", Version = "v2" });

                // Custom filter to exclude controllers with "api/" prefix
                c.DocInclusionPredicate((docName, apiDesc) =>
				{
					// Exclude endpoints with "api/" prefix
					var routePrefix = "api/";
					var route = apiDesc.RelativePath;

					// Include only routes that do not start with the prefix
					return route.StartsWith(routePrefix, StringComparison.InvariantCultureIgnoreCase);
				});
			});

			services.AddApiVersioning(option =>
            {
                option.AssumeDefaultVersionWhenUnspecified = true; //This ensures if client doesn't specify an API version. The default version should be considered. 
                option.DefaultApiVersion = new ApiVersion(1, 0); //This we set the default API version
                option.ReportApiVersions = true; //The allow the API Version information to be reported in the client  in the response header. This will be useful for the client to understand the version of the API they are interacting with.

                //------------------------------------------------//
                option.ApiVersionReader = ApiVersionReader.Combine(
                    new QueryStringApiVersionReader("api-version"),
                    new HeaderApiVersionReader("X-Version"),
                    new MediaTypeApiVersionReader("ver")); //This says how the API version should be read from the client's request, 3 options are enabled 1.Querystring, 2.Header, 3.MediaType. 
                                                           //"api-version", "X-Version" and "ver" are parameter name to be set with version number in client before request the endpoints.
            }).AddApiExplorer(options => {
                options.GroupNameFormat = "'v'VVV"; //The say our format of our version number “‘v’major[.minor][-status]”
                options.SubstituteApiVersionInUrl = true; //This will help us to resolve the ambiguity when there is a routing conflict due to routing template one or more end points are same.
            });



            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            //services.AddEndpointsApiExplorer();
            //services.AddSwaggerGen();

   //         services.AddSwaggerGen(c =>
			//{
			//	//c.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, "api.xml"));

			//	// Custom filter to exclude controllers with "api/" prefix
			//	c.DocInclusionPredicate((docName, apiDesc) =>
			//	{
			//		// Exclude endpoints with "api/" prefix
			//		var routePrefix = "api/";
			//		var route = apiDesc.RelativePath;

			//		// Include only routes that do not start with the prefix
			//		return route.StartsWith(routePrefix, StringComparison.InvariantCultureIgnoreCase);
			//	});
			//});
			


			// *******************************************************

            // add Services into IoC Container
            services.AddScoped<IUserAccountService, UserAccountService>();
			services.AddScoped<IUserRoleService, UserRoleService>();

			services.AddScoped<ICategoryRepository, CategoryRepository>();
			services.AddScoped<ICategoryService, CategoryService>();

			services.AddScoped<IBookRepository, BookRepository>();
			services.AddScoped<IBookService, BookService>();

			services.AddTransient<IBookImageService, BookImageService>();


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
