using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MyThings.Application.Mappers;
using MyThings.Application.Services;
using MyThings.Infrastructure.Data;
using MyThings.Infrastructure.Identity;
using MyThings.Web.Filters;

namespace MyThings.Web
{
	public class Startup
	{
		public IConfiguration Configuration { get; }

		public Startup(IConfiguration configuration)
			=> Configuration = configuration;

		public void ConfigureServices(IServiceCollection services)
		{
			services.AddMvc()
				.SetCompatibilityVersion(CompatibilityVersion.Version_2_1)
				.AddFluentValidation(options =>
				{
					options.RegisterValidatorsFromAssemblyContaining<Startup>();
					options.LocalizationEnabled = false;
				});

			services.AddDbContext<ApplicationDbContext>(options =>
				options.UseSqlServer(Configuration.GetConnectionString("ApplicationConnection")));

			services.AddDbContext<IdentityDbContext>(options =>
				options.UseSqlServer(Configuration.GetConnectionString("IdentityConnection")));

			services.AddIdentity<ApplicationUser, IdentityRole>(options =>
					options.User.RequireUniqueEmail = true)
				.AddEntityFrameworkStores<IdentityDbContext>()
				.AddDefaultTokenProviders();

			services.AddScoped<TaskService>();
			services.AddScoped<CategoryService>();
			services.AddScoped<IUserContext, UserContext>();
			//services.AddScoped<UserContextFilter>();

			services.AddSingleton(AutoMapperConfiguration.Configure());
		}

		public void Configure(IApplicationBuilder app, IHostingEnvironment env)
		{
			if (env.IsDevelopment())
				app.UseDeveloperExceptionPage();

			app.UseStaticFiles();
			app.UseAuthentication();
			app.UseMvcWithDefaultRoute();
		}
	}
}
