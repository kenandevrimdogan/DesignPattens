using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;
using WebApp.Observer.Models;

namespace BasePoject.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;

        public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(string email, string password, bool isPersistent)
        {
            var hasUser = await _userManager.FindByEmailAsync(email);

            if (hasUser == null)
            {
                return View();
            }

            var signInResult = await _signInManager.PasswordSignInAsync(hasUser, password, isPersistent, false);

            if (!signInResult.Succeeded)
            {
                return View();
            }


            return RedirectToAction(nameof(HomeController.Index), "Home");
        }

        [HttpGet]
        public async Task<IActionResult> LogOut()
        {
            await _signInManager.SignOutAsync();

            return RedirectToAction(nameof(HomeController.Index), "Home");
        }

        public IActionResult SignUp()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SignUp(UserCreateViewModel request)
        {
            var appUser = new AppUser
            {
                UserName = request.UserName,
                Email = request.Email,  
            };

            var identityResult = await _userManager.CreateAsync(appUser, request.Password);

            if (identityResult.Succeeded)
            {
                // subject

                ViewBag.message = "Üyelik işlemi başarıyla gerçekleşti";
            }
            else
            {
                ViewBag.message = identityResult.Errors.FirstOrDefault().Description;
            }

            return View();
        }
    }
}
