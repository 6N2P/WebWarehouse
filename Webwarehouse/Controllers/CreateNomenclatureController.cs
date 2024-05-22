using Microsoft.AspNetCore.Mvc;
using Webwarehouse.Class;
using Webwarehouse.DataModels;
using Webwarehouse.Interfeces;

namespace Webwarehouse.Controllers
{
    public class CreateNomenclatureController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly WarehousDBContext _dbContext;
        private readonly ICreateNomenclature _createrNomenclature;
       // private readonly Nomenclature _nomenclature;

        public CreateNomenclatureController(ILogger<HomeController> logger, WarehousDBContext context)
        {
            _logger = logger;
            _dbContext = context;
            _createrNomenclature = new WorkerWithDB(context);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Nomenclature nomenclature) 
        { 
            _createrNomenclature.CreateNomenclature(nomenclature);
            return RedirectToAction("Complete");
        }

        public IActionResult Complete()
        {
            ViewBag.Message = "Номенклатура успешно создана";
            return View();
        }
    }
}
