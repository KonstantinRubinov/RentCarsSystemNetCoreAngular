using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Collections.Generic;
using System.Text;

namespace RentCarsServerCore
{
	public class Startup
	{
		public Startup(IConfiguration configuration)
		{
			Configuration = configuration;
		}

		public IConfiguration Configuration { get; }

		// This method gets called by the runtime. Use this method to add services to the container.
		public void ConfigureServices(IServiceCollection services)
		{
			if (GlobalVariable.logicType == 3)
			{
				new IfMongoDb().AddMongoData();
			}

			services.AddCors(options =>
			{
				options.AddDefaultPolicy(
				builder =>
				{
					builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
				});
			});
			services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Latest);
			services.AddControllers().AddJsonOptions(options => options.JsonSerializerOptions.WriteIndented = true);

			// configure strongly typed settings objects
			var appSettingsSection = Configuration.GetSection("AppSettings");
			services.Configure<AppSettings>(appSettingsSection);

			// configure jwt authentication
			var appSettings = appSettingsSection.Get<AppSettings>();
			var key = Encoding.ASCII.GetBytes(appSettings.Secret);
			services.AddAuthentication(x =>
			{
				x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
				x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
			})
			.AddJwtBearer(x =>
			{
				x.RequireHttpsMetadata = false;
				x.SaveToken = true;
				x.TokenValidationParameters = new TokenValidationParameters
				{
					ValidateIssuerSigningKey = true,
					IssuerSigningKey = new SymmetricSecurityKey(key),
					ValidateIssuer = false,
					ValidateAudience = false
				};
			});




			if (GlobalVariable.logicType == 1)
			{
				// configure DI for application services
				services.AddScoped<IUsersRepository, SqlUsersManager>();
				services.AddScoped<IBranchRepository, SqlBranchManager>();
				services.AddScoped<ICarForRentRepository, SqlCarForRentManager>();
				services.AddScoped<ICarsRepository, SqlCarsManager>();
				services.AddScoped<ICarTypeRepository, SqlCarTypeManager>();
				services.AddScoped<IFullCarDataRepository, SqlFullCarDataManager>();
				services.AddScoped<IMessagesRepository, SqlMessagesManager>();
				services.AddScoped<IRoleRepository, SqlRoleManager>();
				services.AddScoped<ISearchRepository, SqlSearchManager>();
				services.AddScoped<IPriceRepository, SqlPriceManager>();
			}
			else if (GlobalVariable.logicType == 2)
			{
				// configure DI for application services
				services.AddScoped<IUsersRepository, MySqlUsersManager>();
				services.AddScoped<IBranchRepository, MySqlBranchManager>();
				services.AddScoped<ICarForRentRepository, MySqlCarForRentManager>();
				services.AddScoped<ICarsRepository, MySqlCarsManager>();
				services.AddScoped<ICarTypeRepository, MySqlCarTypeManager>();
				services.AddScoped<IFullCarDataRepository, MySqlFullCarDataManager>();
				services.AddScoped<IMessagesRepository, MySqlMessagesManager>();
				services.AddScoped<IRoleRepository, MySqlRoleManager>();
				services.AddScoped<ISearchRepository, MySqlSearchManager>();
				services.AddScoped<IPriceRepository, MySqlPriceManager>();
			}
			else
			{
				// configure DI for application services
				services.AddScoped<IUsersRepository, MongoUsersManager>();
				services.AddScoped<IBranchRepository, MongoBranchManager>();
				services.AddScoped<ICarForRentRepository, MongoCarForRentManager>();
				services.AddScoped<ICarsRepository, MongoCarsManager>();
				services.AddScoped<ICarTypeRepository, MongoCarTypeManager>();
				services.AddScoped<IFullCarDataRepository, MongoFullCarDataManager>();
				services.AddScoped<IMessagesRepository, MongoMessagesManager>();
				services.AddScoped<IRoleRepository, MongoRoleManager>();
				services.AddScoped<ISearchRepository, MongoSearchManager>();
				services.AddScoped<IPriceRepository, MongoPriceManager>();
			}
			services.AddRazorPages();

		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IHostingEnvironment env)
		{
			app.UseCors();
			app.UseRouting();
			app.UseAuthentication();
			app.UseAuthorization();

			//app.UseAuthorization();
			//app.UseRouting();


			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}
			else
			{
				app.UseExceptionHandler("/Home/Error");
				// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
				app.UseHsts();
			}
			app.UseHttpsRedirection();
			app.UseDefaultFiles(new DefaultFilesOptions { DefaultFileNames = new List<string> { "index.html" } });
			app.UseStaticFiles();

			app.UseEndpoints(endpoints =>
			{
				endpoints.MapControllerRoute(
					name: "default",
					pattern: "{controller=Home}/{action=Index}/{id?}");
			});
		}
	}
}
