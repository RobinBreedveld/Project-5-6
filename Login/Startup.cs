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
using login2.Data;
using login2.Models;
using login2.Services;

namespace login2
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
            ConfigureAuth(app);   
            createRolesandUsers();   
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseNpgsql(@"Host=localhost;Database=LoginDB;Username=postgres;Password=admin"));

            services.AddIdentity<ApplicationUser, IdentityRole>(x => { x.Password.RequiredLength = 2; x.Password.RequireUppercase = false; x.Password.RequireLowercase = false; x.Password.RequireNonAlphanumeric = false; })
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            // Add application services.
            services.AddTransient<IEmailSender, EmailSender>();

            services.AddMvc();
        }
        // In this method we will create default User roles and Admin user for login   
        private void createRolesandUsers()   
        {   
            ApplicationDbContext context = new ApplicationDbContext();   
   
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));   
            var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));   
   
   
            // In Startup iam creating first Admin Role and creating a default Admin User    
            if (!roleManager.RoleExists("Admin"))   
            {   
   
                // first we create Admin rool   
                var role = new Microsoft.AspNet.Identity.EntityFramework.IdentityRole();   
                role.Name = "Admin";   
                roleManager.Create(role);   
   
                //Here we create a Admin super user who will maintain the website                  
   
                var user = new ApplicationUser();   
                user.UserName = "shanu";   
                user.Email = "syedshanumcain@gmail.com";   
   
                string userPWD = "A@Z200711";   
   
                var chkUser = UserManager.Create(user, userPWD);   
   
                //Add default User to Role Admin   
                if (chkUser.Succeeded)   
                {   
                    var result1 = UserManager.AddToRole(user.Id, "Admin");   
   
                }   
            }   
   
            // creating Creating Manager role    
            if (!roleManager.RoleExists("Manager"))   
            {   
                var role = new Microsoft.AspNet.Identity.EntityFramework.IdentityRole();   
                role.Name = "Manager";   
                roleManager.Create(role);   
   
            }   
   
            // creating Creating Employee role    
            if (!roleManager.RoleExists("Employee"))   
            {   
                var role = new Microsoft.AspNet.Identity.EntityFramework.IdentityRole();   
                role.Name = "Employee";   
                roleManager.Create(role);   
   
            }   
        } 

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseBrowserLink();
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
        }
    }
}
