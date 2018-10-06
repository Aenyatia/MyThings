using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ToDoList.Persistence.Data;
using ToDoList.Persistence.Identity;

namespace ToDoList
{
	public class Startup
	{
		public IConfiguration Configuration { get; }

		public Startup(IConfiguration configuration)
		{
			Configuration = configuration;
		}

		public void ConfigureServices(IServiceCollection services)
		{
			services.AddMvc().AddFluentValidation(options =>
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
