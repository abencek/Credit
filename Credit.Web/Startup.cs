using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Newtonsoft.Json.Serialization;

using Credit.Data.App;
using Credit.Data.Identity;
using System.Reflection;

namespace Credit.Web
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }


        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("AppDbContext"))
            );
            services.AddDbContext<IdentityDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("IdentityDbContext"))
            );

            //Set less strict password requirements (do not use this setting in production)
            services.AddIdentity<IdentityUser, IdentityRole>(
                opt => {
                    opt.Password.RequireDigit = false;
                    opt.Password.RequireLowercase = false;
                    opt.Password.RequireNonAlphanumeric = false;
                    opt.Password.RequireUppercase = false;
                    opt.Password.RequiredLength = 1;
                }
            ).AddEntityFrameworkStores<IdentityDbContext>();

            services.AddTransient<IAppCustomerRepository, EFAppCustomerRepository>();
            services.AddTransient<IAppAddressRepository, EFAppAddressRepository>();
            services.AddTransient<IAppAgreementRepository, EFAppAgreementRepository>();
            services.AddTransient<IAppCommentRepository, EFAppCommentRepository>();

            services.AddControllersWithViews().AddNewtonsoftJson(opt =>
            {
                // Set default Pascal casing serialization
                opt.SerializerSettings.ContractResolver = new DefaultContractResolver();
            })
             // Load Credit.Api project
            .AddApplicationPart(Assembly.Load(new AssemblyName("Credit.Api")));

        }


        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {

            app.UseDeveloperExceptionPage();
            app.UseStatusCodePages();
            app.UseStaticFiles();
            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();


            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Customer}/{action=Index}/{id?}/{tab?}");
            });
        
        }
    }
}
