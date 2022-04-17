using BasePoject.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using WebApp.ChainOfResponsibility.ChainOfResponsibility;
using WebApp.ChainOfResponsibility.Models;

namespace BasePoject.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly AppIdentityDbContext _appIdentityDbContext;

        public HomeController(ILogger<HomeController> logger, AppIdentityDbContext appIdentityDbContext)
        {
            _logger = logger;
            _appIdentityDbContext = appIdentityDbContext;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public async Task<IActionResult> SendEmail()
        {
            var products = await _appIdentityDbContext.Products.ToListAsync();

            var excelProcessHandler = new ExcelProcessHandler<Product>();
            var zipFileProcessHandler = new ZiplFileProcessHandler<Product>();
            var sendEmailProcessHandler = new SendEmailProcessHandler("product", "test@gmail.com", "Subject", "Body");

            excelProcessHandler.SetNext(zipFileProcessHandler).SetNext(sendEmailProcessHandler);

            excelProcessHandler.Handle(products);

            return View(nameof(Index));
        }
    }
}
