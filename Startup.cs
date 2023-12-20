using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;
using InventoryBeginners.Models;
using InventoryBeginners.Data;
using InventoryBeginners.Interfaces;
using InventoryBeginners.Repositories;
using Microsoft.AspNetCore.Identity;


namespace InventoryBeginners
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
            services.AddControllersWithViews();

            // Other repositories
            services.AddScoped<IUnit, UnitRepository>();
            services.AddScoped<IProduct, ProductRepo>();
            services.AddScoped<ISupplier, SupplierRepo>();
            services.AddScoped<ICategory, CategoryRepo>();
            services.AddScoped<IBrand, BrandRepo>();
            services.AddScoped<IProductProfile, ProductProfileRepo>();
            services.AddScoped<IProductGroup, ProductGroupRepo>();
            services.AddSingleton<IShoppingCart, ShoppingCartRepo>();
            services.AddScoped<IOrder, OrderRepo>();


            // DbContext registration
            services.AddDbContext<InventoryContext>(options => options.UseSqlServer(Configuration.GetConnectionString("dbconn")));

            // Identity registration
            services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = false)
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<InventoryContext>();
            
        }


        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public async void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
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
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");

                endpoints.MapRazorPages();

            });
            using (var scope = app.ApplicationServices.CreateScope())
            {
                var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
                var roles = new[] { "Admin", "User" };
                foreach (var role in roles)
                {
                    if (!await roleManager.RoleExistsAsync(role))
                    {
                        await roleManager.CreateAsync(new IdentityRole(role));
                    }
                }
            }
            using (var scope = app.ApplicationServices.CreateScope())
            {
                var userManager = scope.ServiceProvider.GetRequiredService<UserManager<IdentityUser>>();
                var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

                string email = "Admin1@gmail.com";
                string userName = "Admin1";
                string password = "Admin_123";

                if (await userManager.FindByEmailAsync(email) == null)
                {
                    var user = new IdentityUser
                    {
                        UserName = userName,
                        Email = email
                    };

                    await userManager.CreateAsync(user, password);

                    // Attendez la création de l'utilisateur avant d'ajouter le rôle
                    if (await userManager.FindByEmailAsync(email) != null)
                    {
                        // Ajout du rôle "Admin" à l'utilisateur
                        await userManager.AddToRoleAsync(user, "Admin");
                    }
                }
            }




        }

    }
}
