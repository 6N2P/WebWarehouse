using Microsoft.AspNetCore.Mvc;
using Webwarehouse.Class;
using Webwarehouse.DataModels;
using Webwarehouse.Interfeces;

namespace Webwarehouse.Controllers
{
    public class CreateNomenclatureController : Controller
    {
        private readonly ILogger<CreateNomenclatureController> _logger;
        private readonly WarehousDBContext _dbContext;
        private readonly ICreateNomenclature _createrNomenclature;
       // private readonly Nomenclature _nomenclature;

        public CreateNomenclatureController(ILogger<CreateNomenclatureController> logger, WarehousDBContext context)
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
            if (Vertificate(nomenclature))
            {
                _createrNomenclature.CreateNomenclature(nomenclature);
                return RedirectToAction("Complete");
            }
            else return RedirectToAction("Error", new {er= "Количество должно быть не меньше нуля, имя номенклатуры не должно повторятся" });
        }      

        public IActionResult Complete()
        {
            ViewBag.Message = "Номенклатура успешно создана";
            return View();
        }
        public IActionResult Error(string er) => Content(er);

        private bool Vertificate(Nomenclature nomenclature)
        {
            if (nomenclature.Count < 0)
            {
                return false;
            }
            else
            {
                var n = _dbContext.Nomenclatures.Where(x => x.Name == nomenclature.Name).FirstOrDefault();
                if (n != null) return false;
            }
            return true;
        }
    }
}
