using BasePoject.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApp.Command.Models;

namespace BasePoject
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();

            using var scope = host.Services.CreateScope();

            var identityDbContext = scope.ServiceProvider.GetRequiredService<AppIdentityDbContext>();

            var userManager = scope.ServiceProvider.GetRequiredService<UserManager<AppUser>>();

            identityDbContext.Database.Migrate();

            if (!userManager.Users.Any())
            {
                for (int i = 1; i <= 5; i++)
                {
                    userManager.CreateAsync(new AppUser
                    {
                        UserName = $"user{i}",
                        Email = $"user{i}@outlook.com"
                    }, "Password123*").Wait();
                }

                for (int i = 1; i <= 30; i++)
                {
                    identityDbContext.Products.AddAsync(new Product
                    {
                        Name = $"Kalem {1}",
                        Price = i * 100,
                        Stock = i * 50
                    });
                }

                identityDbContext.SaveChangesAsync().Wait();
            }

            host.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
