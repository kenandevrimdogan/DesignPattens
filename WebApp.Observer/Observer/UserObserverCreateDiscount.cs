using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using WebApp.Observer.Models;

namespace WebApp.Observer.Observer
{
    public class UserObserverCreateDiscount : IUserObserver
    {
        private readonly IServiceProvider _serviceProvider;

        public UserObserverCreateDiscount(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public void CreateUser(AppUser appUser)
        {
            var logger = _serviceProvider.GetRequiredService<ILogger<UserObserverCreateDiscount>>();
            var appDbContext = _serviceProvider.GetRequiredService<AppIdentityDbContext>();

            appDbContext.Discounts.Add(new Discount
            {
                UserId = appUser.Id,
                Rate = 10
            });

            appDbContext.SaveChanges();

            logger.LogInformation("Discount created");
        }
    }
}
