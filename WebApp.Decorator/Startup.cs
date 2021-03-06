using BasePoject.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Build.Framework;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using WebApp.Decorator.Decorator;
using WebApp.Decorator.Repositories;

namespace BasePoject
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
            services.AddMemoryCache();
            services.AddLogging();
            services.AddHttpContextAccessor();

            // The First 
            //services.AddScoped<IProductRepository>(serviceProvider =>
            //{
            //    var context = serviceProvider.GetRequiredService<AppIdentityDbContext>();
            //    var memoryCache = serviceProvider.GetRequiredService<IMemoryCache>();
            //    var logService = serviceProvider.GetRequiredService<ILogger<ProductRepositoryLoggingDecorator>>();

            //    var productRepository = new ProductRepository(context);

            //    var cacheDecorator = new ProductRepositoryCacheDecorator(productRepository, memoryCache);
            //    var logDecorator = new ProductRepositoryLoggingDecorator(productRepository, logService);

            //    return logDecorator;
            //});

            // The seond
            //services.AddScoped<IProductRepository, ProductRepository>()
            //    .Decorate<IProductRepository, ProductRepositoryCacheDecorator>()
            //    .Decorate<IProductRepository, ProductRepositoryLoggingDecorator>();

            // The Third
            services.AddScoped<IProductRepository>(serviceProvider =>
            {
                var httpContextAccessor = serviceProvider.GetRequiredService<IHttpContextAccessor>();
                var context = serviceProvider.GetRequiredService<AppIdentityDbContext>();
                var memoryCache = serviceProvider.GetRequiredService<IMemoryCache>();
                var logService = serviceProvider.GetRequiredService<ILogger<ProductRepositoryLoggingDecorator>>();
                var productRepository = new ProductRepository(context);

                if (httpContextAccessor.HttpContext.User.Identity.Name == "user1")
                {
                    var cacheDecorator = new ProductRepositoryCacheDecorator(productRepository, memoryCache);
                    return cacheDecorator;
                }
                else if (httpContextAccessor.HttpContext.User.Identity.Name == "user2")
                {
                    var logDecorator = new ProductRepositoryLoggingDecorator(productRepository, logService);
                    return logDecorator;
                }

                return productRepository;
            });


            services.AddDbContext<AppIdentityDbContext>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("SqlServer"));
            });

            services.AddIdentity<AppUser, IdentityRole>(options =>
            {
                options.User.RequireUniqueEmail = true;
            }).AddEntityFrameworkStores<AppIdentityDbContext>();

            services.AddControllersWithViews();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
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

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
