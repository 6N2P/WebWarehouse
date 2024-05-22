using Microsoft.AspNetCore.Mvc;
using Webwarehouse.Class;
using Webwarehouse.DataModels;
using Webwarehouse.Interfeces;
using Webwarehouse.Models;

namespace Webwarehouse.Controllers
{
    public class CreateBillOfLadingController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly WarehousDBContext _dbContext;
        private readonly ICreateBillOfLadingViewModel _createrBillOfLading;
        private int _billOfLadingId;
        

        public CreateBillOfLadingController(ILogger<HomeController> logger, WarehousDBContext context)
        {
            _logger = logger;
            _dbContext = context;
            _createrBillOfLading = new CreatorBillOfLoadingViewModel();
        }

        public IActionResult Create()
        {
            var billOfLading = _createrBillOfLading.Create(_dbContext);
            return View(billOfLading);
        }

        [HttpPost]
        public IActionResult Create(CreateBillOfLadingViewModel billOfLading)
        {
            if (billOfLading.Nomenclature != null && billOfLading.Nomenclature.Count > 0 && IsSheepingItems(billOfLading.Nomenclature))
            {
                //Пересчитываю общее колличество позиций в бд
                IRecalculateQuantity recalculater = new WorkerWithDB(_dbContext);
                recalculater.RecalculateQuantity(billOfLading.Nomenclature);

                //Создаю товарную накладную и получаю ее Id
                ICreateBillOfLading createBillOfLading = new WorkerWithDB(_dbContext);
                _billOfLadingId = createBillOfLading.CreateBillOfLading(billOfLading);

                ICreateItemsBillOfLading createItemsBillOfLading = new WorkerWithDB(_dbContext);
                createItemsBillOfLading.CreateItemsBillOfLading(_billOfLadingId, billOfLading.Nomenclature);
                


                return RedirectToAction("Complete",new { billOfLadingId = _billOfLadingId });
            }
            return View(billOfLading);
        }

        public IActionResult Complete(int billOfLadingId)
        {
            //Создаю объект с данными для товарной накладной
            CreatorBillOfLoadingFromDB creator = new CreatorBillOfLoadingFromDB();
            var billOfLadingNew = creator.Create(_dbContext, billOfLadingId);
            return View(billOfLadingNew);
        }

        private bool IsSheepingItems(List<NomenclaturaForBillOfLading> nomenclature)
        {
            foreach (var n in nomenclature)
            {
                if (n.IsSheeping == true) return true;
            }
            return false;
        }

      
     
    }
}
