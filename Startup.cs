using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Mission09_hwatso02.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mission09_hwatso02
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        
        //set up configuration
        public IConfiguration Configuration { get; set; }
        public Startup (IConfiguration temp)
        {
            Configuration = temp;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();

            //add dbcontext for sqlite
            services.AddDbContext<BookstoreContext>(options =>
            {
                options.UseSqlite(Configuration["ConnectionStrings:BookstoreDBConnection"]);
            });

            //de-couple
            services.AddScoped<IBookstoreRepository, EFBookstoreRepository>();

            //razor pages
            services.AddRazorPages();

            //sessions
            services.AddDistributedMemoryCache();
            services.AddSession();

            services.AddScoped<Cart>(c => SessionCart.GetCart(c));
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            //refers to wwwroot
            app.UseStaticFiles();
            app.UseSession();
            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                //if given both the category and page number
                endpoints.MapControllerRoute("categorypage", "{Category}/Page{pageNum}",
                    new { Controller = "Home", action = "Index" });

                //if given only the page number
                endpoints.MapControllerRoute("Paging", "Page{pageNum}", 
                    new { Controller = "Home", action = "Index" });

                //if given only the category
                endpoints.MapControllerRoute("category", "{Category}",
                    new { Controller = "Home", action = "Index", pageNum = 1 });

                //set endpoints
                endpoints.MapDefaultControllerRoute();

                endpoints.MapRazorPages();
            });
        }
    }
}
