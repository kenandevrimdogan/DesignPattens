using BasePoject.Models;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;

namespace WebApp.Observer.Observer
{
    public class UserOberverWriteToConsole : IUserObserver
    {
        private readonly IServiceProvider _serviceProvider;

        public UserOberverWriteToConsole(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public void CreateUser(AppUser appUser)
        {
            var logger = _serviceProvider.GetRequiredService<ILogger<UserOberverWriteToConsole>>();

            logger.LogInformation($"User created: {appUser.Id}");
        }
    }
}
