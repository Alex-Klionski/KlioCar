using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using KlioCarProject.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using KlioCarProject.Infrastructure;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace KlioCarProject
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public Startup(IConfiguration configuration) => Configuration = configuration;
        public IConfiguration Configuration { get; }
        public void ConfigureServices(IServiceCollection services)
        {
            //Session support
            services.AddMemoryCache();
            services.AddSession();
            services.AddDistributedMemoryCache();

            // Registration session
            services.AddScoped<Cart>(sp => SessionCart.GetCart(sp));
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            services.AddTransient<ICarRepository, EFCarRepository>();
            services.AddTransient<IOrderRepository, EFOrderRepository>();


            //Add databases
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));


            services.AddDbContext<AppIdentityDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DbConnection")));
  
            services.AddSignalR();
           

            services.ConfigureApplicationCookie(opts => opts.LoginPath = "/Users/Login");

            services.AddIdentity<AppUser, IdentityRole>(opts => {
                opts.User.RequireUniqueEmail = true;
                opts.Password.RequiredLength = 6;
                opts.Password.RequireNonAlphanumeric = false;
                opts.Password.RequireLowercase = false;
                opts.Password.RequireUppercase = false;
                opts.Password.RequireDigit = false;
            }).AddEntityFrameworkStores<AppIdentityDbContext>()
                .AddDefaultTokenProviders();

            services.AddMvc(option => option.EnableEndpointRouting = false);
             
            services.AddControllersWithViews();
            services.AddRazorPages();

           
            services.AddAuthentication()
               .AddGoogle(options =>
               {
                   options.ClientId = "543062444631-vvs36s1dce29l1vct1mjdi2unl9pqvit.apps.googleusercontent.com";
                   options.ClientSecret = "TCXuAn9IDdEig5MOiJj4CQBr";
               });
 
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Exception");
                app.UseHsts();
            }

            app.UseStatusCodePagesWithReExecute("/Error/{0}");
            app.UseHttpsRedirection();

            app.UseAuthentication();
            app.UseRouting();
            app.UseAuthorization();
            app.UseStaticFiles();
           // app.UseStatusCodePages();
            app.UseSession();
           
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
                endpoints.MapHub<ChatHub>("/chat");
            });
            
            //Navigation

            app.UseMvc(routes =>
            {    
            routes.MapRoute(
                name: null,
                template: "{category}/Page{productPage:int}",
                defaults: new { controller = "Car", action = "List" }
                );
                
            routes.MapRoute(
                 name: null,
                 template: "Page{productPage:int}",
                 defaults: new { controller = "Car", action = "List", productPage = 1 }
                 );
            
            routes.MapRoute(
                name: null,
                template: "{category}",
                defaults: new { controller = "Car", action = "List", productPage = 1 }
                );
               
            routes.MapRoute(    
                 name: null,
                 template: "",
                 defaults: new { controller = "Car", action = "List", productPage = 1 }
                 );
                
                routes.MapRoute(name: null, template: "{controller}/{action}/{id?}");
            });
            

           
            SeedData.EnsurePopulated(app);
            AppIdentityDbContext.CreateAdminAccount(app.ApplicationServices, Configuration).Wait();
        }
    }
}
