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
using WebApp.Composite.Models;

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

            AppUser user = null;

            if (!userManager.Users.Any())
            {
                for (int i = 1; i <= 5; i++)
                {
                    user = new AppUser
                    {
                        UserName = $"user{i}",
                        Email = $"user{i}@outlook.com"
                    };

                    userManager.CreateAsync(user, "Password123*").Wait();

                }

                var newCategory1 = new Category
                {
                    Name = "Polisiye Romanları",
                    ReferenceId = 0,
                    UserId = user.Id
                };

                var newCategory2 = new Category
                {
                    Name = "Suç Romanları",
                    ReferenceId = 0,
                    UserId = user.Id
                };

                var newCategory3 = new Category
                {
                    Name = "Cinayet Romanları",
                    ReferenceId = 0,
                    UserId = user.Id
                };

                identityDbContext.AddRange(newCategory1, newCategory2, newCategory3);

                identityDbContext.SaveChanges();


                var subCategory1 = new Category
                {
                    Name = "Polisiye Romanları 2",
                    ReferenceId = newCategory1.Id,
                    UserId = user.Id
                };

                var subCategory2 = new Category
                {
                    Name = "Suç Romanları 2",
                    ReferenceId = newCategory2.Id,
                    UserId = user.Id
                };

                var subCategory3 = new Category
                {
                    Name = "Cinayet Romanları 2",
                    ReferenceId = newCategory3.Id,
                    UserId = user.Id
                };

                identityDbContext.AddRange(subCategory1, subCategory2, subCategory3);

                identityDbContext.SaveChanges();

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
