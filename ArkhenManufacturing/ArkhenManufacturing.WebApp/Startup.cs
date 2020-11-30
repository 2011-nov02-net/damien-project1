using ArkhenManufacturing.Domain;
using ArkhenManufacturing.Domain.Internal;
using ArkhenManufacturing.WebApp.Misc;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
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


            /*string connectionString */ 
            _ = Configuration["ArkhenContext:ConnectionString"];

            services.AddSingleton<IRepository, InternalRepository>();
            // services.AddScoped<IRepository, DatabaseRepository>(
            //     sp => new DatabaseRepository(new DbContextOptionsBuilder<ArkhenContext>()
            //             .UseSqlServer(Configuration["ArkhenContext:ConnectionString"])
            //             .Options));
            
            services.AddScoped<Archivist>();
            services.AddScoped<IEncrypter, SaltHashEncrypter>();

            /*
             
            services.AddDbContext<ArkhenContext>(options =>
                options.UseSqlServer(Configuration["ArkhenContext:ConnectionString"])
            );
            
             */

            // Initialize the Archivist with a connection string, core logger and ef core logger

            // string connectionString = Configuration["ArkhenContext:ConnectionString"];
            // string targetPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            // var archivistLogger = new FileLogger($"{targetPath}/ArkhenManufacturing/arkhen_manufacturing.archivist.log");
            // var efCoreLogger = new FileLogger($"{targetPath}/arkhen_manufacturing.efcore.log");

            // DateTime.Now:{{0:MM/dd/yy H:mm:ss:fff}}
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env) {
            if (env.IsDevelopment()) {
                app.UseDeveloperExceptionPage();
            } else {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints => {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
