using ArkhenManufacturing.DataAccess;
using ArkhenManufacturing.Domain;
// using ArkhenManufacturing.Domain.Internal;
using ArkhenManufacturing.Domain.Database;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace ArkhenManufacturing.WebApp
{
    public class Startup
    {
        public Startup(IConfiguration configuration) {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services) {
            services.AddControllersWithViews();

            string connectionString = Configuration["ArkhenContext:ConnectionString"];

            services.AddDbContext<ArkhenContext>(options =>
                options.UseSqlServer(connectionString));

            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<ArkhenContext>()
                .AddDefaultTokenProviders();

            // services.AddSingleton<IRepository, InternalRepository>();
            services.AddScoped<IRepository, DatabaseRepository>(
                sp => new DatabaseRepository(new DbContextOptionsBuilder<ArkhenContext>()
                        .UseSqlServer(connectionString)
                        .Options));

            services.AddScoped<Archivist>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env) {
            if (env.IsDevelopment()) {
                app.UseDeveloperExceptionPage();
            } else {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();
            app.UseAuthentication();

            app.UseEndpoints(endpoints => {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
