using Microsoft.AspNetCore.Mvc;
using Webwarehouse.Class;
using Webwarehouse.DataModels;
using Webwarehouse.Interfeces;
using Webwarehouse.Models;

namespace Webwarehouse.Controllers
{
    public class CreateBillOfLadingController : Controller
    {
        private readonly ILogger<CreateBillOfLadingController> _logger;
        private readonly WarehousDBContext _dbContext;
        private readonly ICreateBillOfLadingViewModel _createrBillOfLading;
        private int _billOfLadingId;
        

        public CreateBillOfLadingController(ILogger<CreateBillOfLadingController> logger, WarehousDBContext context)
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
                if (Vertification(billOfLading))
                {
                    //Пересчитываю общее колличество позиций в бд
                    IRecalculateQuantity recalculater = new WorkerWithDB(_dbContext);
                    recalculater.RecalculateQuantity(billOfLading.Nomenclature);

                    //Создаю товарную накладную и получаю ее Id
                    ICreateBillOfLading createBillOfLading = new WorkerWithDB(_dbContext);
                    _billOfLadingId = createBillOfLading.CreateBillOfLading(billOfLading);

                    ICreateItemsBillOfLading createItemsBillOfLading = new WorkerWithDB(_dbContext);
                    createItemsBillOfLading.CreateItemsBillOfLading(_billOfLadingId, billOfLading.Nomenclature);

                    return RedirectToAction("Complete", new { billOfLadingId = _billOfLadingId });
                }
                else
                {
                    return RedirectToAction("Error", new { er = "На складе нет такого количества товара" });
                }

            }
            return View(billOfLading);
        }

        private bool Vertification(CreateBillOfLadingViewModel billOfLading)
        {
            foreach(var item in billOfLading.Nomenclature)
            {
                if(item.IsSheeping == true && item.CountSheeping <=0)
                {
                    return false;
                }
                else if(item.CountSheeping > item.CountAll)
                {
                    return false;
                }
            }
            return true;
        }

        public IActionResult Complete(int billOfLadingId)
        {
            //Создаю объект с данными для товарной накладной
            CreatorBillOfLoadingFromDB creator = new CreatorBillOfLoadingFromDB();
            var billOfLadingNew = creator.Create(_dbContext, billOfLadingId);
            return View(billOfLadingNew);
        }
        public IActionResult Error(string er) => Content(er);
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
