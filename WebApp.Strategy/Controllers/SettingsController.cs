using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using WebApp.Strategy.Models;

namespace WebApp.Strategy.Controllers
{
    [Authorize]
    public class SettingsController : Controller
    {
        public IActionResult Index()
        {
            Settings settings = new();

            if (User.Claims.FirstOrDefault(x => x.Type == Settings.claimDatabaseType) != null)
            {
                settings.DatabaseType = (EDatabaseType)int.Parse(User.Claims.FirstOrDefault(x => x.Type == Settings.claimDatabaseType).Value);
            }else
            {
                settings.DatabaseType = settings.GetDefaultDatabaseType;
            }

            return View(settings);
        }
    }
}
