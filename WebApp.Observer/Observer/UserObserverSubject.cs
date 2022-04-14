using System.Collections.Generic;
using WebApp.Observer.Models;

namespace WebApp.Observer.Observer
{
    public class UserObserverSubject
    {
        private readonly List<IUserObserver> _userObservers;

        public UserObserverSubject()
        {
            _userObservers = new List<IUserObserver>();
        }

        public void RegisterObserver(IUserObserver userObserver)
        {
            _userObservers.Add(userObserver);
        }

        public void UnRegisterObserver(IUserObserver userObserver)
        {
            _userObservers.Remove(userObserver);
        }

        public void NotifyObservers(AppUser appUser)
        {
            foreach (var userObserver in _userObservers)
            {
                userObserver.CreateUsered(appUser);
            }
        }
    }
}
