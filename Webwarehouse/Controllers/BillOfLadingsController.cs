using ClosedXML.Excel;
using DocumentFormat.OpenXml.Drawing.Diagrams;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Webwarehouse.Class;
using Webwarehouse.DataModels;
using Webwarehouse.Interfeces;
using Webwarehouse.Models;

namespace Webwarehouse.Controllers
{
    public class BillOfLadingsController : Controller
    {
        private readonly ILogger<BillOfLadingsController> _logger;
        private readonly WarehousDBContext _dbContext;
        private readonly ICreateBilletOfLadingsList _createrList;
        public BillOfLadingsController(ILogger<BillOfLadingsController> logger, WarehousDBContext context)
        {
            _logger = logger;
            _dbContext = context;
            _createrList = new CreatorBillOfLadingsList(_dbContext);
        }
        public IActionResult Index()
        {
            var billOfLoadings = _createrList.CreateBilletOfLadingsList();
            return View(billOfLoadings);
        }

        [HttpPost]
        public IActionResult Index(BillOfLadingsListViewModel billOfLading)
        {
            CreatorBillOfLoadingViewModel creatorBillOfLading = new CreatorBillOfLoadingViewModel();
            var nomenklatures = creatorBillOfLading.Create(_dbContext, billOfLading.IdBillOfLading);
            if (nomenklatures.Nomenclature != null)
            {
                using (var workbook = new XLWorkbook())
                {
                    var worksheet = workbook.Worksheets.Add("BillOfLading");
                    var currentRow = 1;
                    worksheet.Cell(currentRow, 1).Value = nomenklatures.NameOrganisation;
                    worksheet.Cell(currentRow, 2).Value = nomenklatures.DateCreate;

                    #region Рамка
                    worksheet.Range(currentRow, 1, currentRow, 1).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                    worksheet.Range(currentRow, 2, currentRow, 2).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                    worksheet.Range(currentRow, 1, currentRow, 1).Style.Border.TopBorder = XLBorderStyleValues.Thin;
                    worksheet.Range(currentRow, 2, currentRow, 2).Style.Border.TopBorder = XLBorderStyleValues.Thin;
                    worksheet.Range(currentRow, 1, currentRow, 1).Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                    worksheet.Range(currentRow, 2, currentRow, 2).Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                    worksheet.Range(currentRow, 2, currentRow, 2).Style.Border.RightBorder = XLBorderStyleValues.Thin;
                    #endregion Рамка

                    currentRow = 2;
                    worksheet.Cell(currentRow, 1).Value = "Наименование";
                    worksheet.Cell(currentRow, 2).Value = "Кол-во. шт.";

                    #region Рамка 2
                    worksheet.Range(currentRow, 1, currentRow, 1).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                    worksheet.Range(currentRow, 2, currentRow, 2).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                    worksheet.Range(currentRow, 1, currentRow, 1).Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                    worksheet.Range(currentRow, 2, currentRow, 2).Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                    worksheet.Range(currentRow, 2, currentRow, 2).Style.Border.RightBorder = XLBorderStyleValues.Thin;
                    #endregion Рамка 2

                    worksheet.Range(1, 2, 2, 2).Style.Border.RightBorder = XLBorderStyleValues.Medium;
                    worksheet.Range(1, 1, 2, 1).Style.Border.LeftBorder = XLBorderStyleValues.Medium;
                    worksheet.Range(2, 1, 2, 2).Style.Border.BottomBorder = XLBorderStyleValues.Medium;
                    worksheet.Range(1, 1, 1, 2).Style.Border.TopBorder = XLBorderStyleValues.Medium;

                    foreach (var nomenclature in nomenklatures.Nomenclature)
                    {
                        currentRow++;
                        worksheet.Cell(currentRow, 1).Value = nomenclature.Name;
                        worksheet.Cell(currentRow, 2).Value = nomenclature.CountSheeping;

                        #region Рамка 2
                        worksheet.Range(currentRow, 1, currentRow, 1).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                        worksheet.Range(currentRow, 2, currentRow, 2).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                        worksheet.Range(currentRow, 1, currentRow, 1).Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                        worksheet.Range(currentRow, 2, currentRow, 2).Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                        worksheet.Range(currentRow, 2, currentRow, 2).Style.Border.RightBorder = XLBorderStyleValues.Thin;
                        #endregion Рамка 2
                    }

                    using (var stream = new MemoryStream())
                    {
                        workbook.SaveAs(stream);
                        var content = stream.ToArray();

                        return File(
                            content,
                            "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                            "BilletOfLading.xlsx");
                    }

                }
            }
            else
            {
                return RedirectToAction("Index");
            }
        }
    }
}

