using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Prometheus.Business;
using Prometheus.Business.Reflection;
using Prometheus.Models;
using Prometheus.Scheduler;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace Prometheus.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        [JobMethod]
        public IActionResult Index()
        {
            var catchJobMethods = new CatchJobMethods().GetRuntimeJobMethods();
            //foreach (var item in catchJobMethods)
            //{
            //    RecurringJobSchduler<object> recurringJobSchduler = new RecurringJobSchduler<object>(item.TypeName);
            //    recurringJobSchduler.AddJob();
            //}

            RecurringJobSchduler<object> recurringJobSchduler = new RecurringJobSchduler<object>();
            recurringJobSchduler.AddJob();

            return View();
        }

        [JobMethod]
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        [JobMethod]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
