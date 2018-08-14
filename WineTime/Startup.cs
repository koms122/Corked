using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WineTime.Data;
using WineTime.Models;
using WineTime.Services;

namespace WineTime
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
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

          

            // Add application services.
            services.AddTransient<IEmailSender>((iServiceProvider) => new EmailSender(Configuration.GetValue<string>("SendGrid.ApiKey")));

            //environment, merchantid, publickey, privatekey has to be in the same order!!!
            services.AddTransient<Braintree.IBraintreeGateway>((iServiceProvider) => new Braintree.BraintreeGateway(
                Configuration.GetValue<string>("Braintree.Environment"), 
                Configuration.GetValue<string>("Braintree.MerchantId"),
                Configuration.GetValue<string>("Braintree.PublicKey"),
                Configuration.GetValue<string>("Braintree.PrivateKey")
                ));

            services.AddTransient<System.Data.SqlClient.SqlConnection>((x) => new System.Data.SqlClient.SqlConnection(Configuration.GetConnectionString("DefaultConnection")));
            
            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, IServiceProvider services)
        {
            if (env.IsDevelopment())
            {
                app.UseBrowserLink();
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();

            app.UseAuthentication();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });

            //Adding the admin role

            //var roleManager = services.GetService<RoleManager<IdentityRole>>();
            //if (!roleManager.Roles.Any(x => x.Name == "Administrator"))
            //{
            //    roleManager.CreateAsync(new IdentityRole("Administrator")).Wait();
            //}
            
        }
    }
}