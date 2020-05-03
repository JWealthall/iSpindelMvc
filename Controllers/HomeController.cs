using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using iSpindelMvc.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using iSpindelMvc.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;

namespace iSpindelMvc.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly LogDb _logDb;

        public HomeController(ILogger<HomeController> logger, LogDbContext context)
        {
            _logger = logger;
            _logDb = new LogDb(context);
        }

        public IActionResult Index()
        {
            //return View(_logDb.SummaryData(current: true));
            return View(_logDb.SummaryData());
        }

        public IActionResult About()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
