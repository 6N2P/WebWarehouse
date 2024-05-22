using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Webwarehouse.Class;
using Webwarehouse.DataModels;
using Webwarehouse.Interfeces;
using Webwarehouse.Models;

namespace Webwarehouse.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly WarehousDBContext _dbContext;
        public HomeController(ILogger<HomeController> logger, WarehousDBContext context)
        {
            _logger = logger;
            _dbContext = context;
        }

        public IActionResult Index()
        {
            ICreateNomenclatureList createNomenclatureList = new CreatorNomenclatureList();
            var nomenclatures = createNomenclatureList.CreateNomenclatureList(_dbContext);
            return View(nomenclatures);
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
    }
}
