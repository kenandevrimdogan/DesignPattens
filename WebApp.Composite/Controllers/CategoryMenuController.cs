using BasePoject.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using WebApp.Composite.Composite;
using WebApp.Composite.Models;

namespace WebApp.Composite.Controllers
{
    public class CategoryMenuController : Controller
    {
        private readonly AppIdentityDbContext _appIdentityDbContext;

        public CategoryMenuController(AppIdentityDbContext appIdentityDbContext)
        {
            _appIdentityDbContext = appIdentityDbContext;
        }

        public async Task<IActionResult> Index()
        {
            var userId = User.Claims.First(x => x.Type == ClaimTypes.NameIdentifier).Value;
            var categories = await _appIdentityDbContext.Categories.Include(x => x.Books).Where(x => x.UserId == userId).OrderBy(x => x.Id).ToListAsync();


            var menu = GetMenus(categories, new Category { Id = 0, Name = "Top Category" }, new BookComposite(0, "Top Menu"));

            ViewBag.menu = menu;

            ViewBag.selectList = menu._components.SelectMany(x => ((BookComposite)x).GetSelectListItems(""));

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(int categoryId, string bookName)
        {
            await _appIdentityDbContext.Books.AddAsync(new Book
            {
                CategoryId = categoryId,
                Name = bookName
            });

            await _appIdentityDbContext.SaveChangesAsync();

            return RedirectToAction("Index");
        }

        public BookComposite GetMenus(List<Category> categories, Category topCategory, BookComposite topBookComposite, BookComposite last = null)
        {
            categories.Where(x => x.ReferenceId == topCategory.Id).ToList().ForEach(categoryItem =>
            {
                var bookComposite = new BookComposite(categoryItem.Id, categoryItem.Name);

                categoryItem.Books.ToList().ForEach(bookItem =>
                {
                    bookComposite.Add(new BookComponent(bookItem.Id, bookItem.Name));
                });

                if (last != null)
                {
                    last.Add(bookComposite);
                }
                else
                {
                    topBookComposite.Add(bookComposite);
                }

                GetMenus(categories, categoryItem, bookComposite, last);

            });

            return topBookComposite;
        }
    }
}
