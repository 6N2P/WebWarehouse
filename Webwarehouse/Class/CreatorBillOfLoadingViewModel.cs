using Webwarehouse.DataModels;
using Webwarehouse.Interfeces;
using Webwarehouse.Models;

namespace Webwarehouse.Class
{
    public class CreatorBillOfLoadingViewModel : ICreateBillOfLadingViewModel
    {
        public CreateBillOfLadingViewModel Create(WarehousDBContext context)
        {
            IGetNomenclature createNomenclatureList = new WorkerWithDB(context);
            var nomenclatureFromDB = createNomenclatureList.GetNomenclatureList();
            List<NomenclaturaForBillOfLading> nomenclatures = new List<NomenclaturaForBillOfLading>();
            foreach (var nom in nomenclatureFromDB)
            {
                NomenclaturaForBillOfLading n = new NomenclaturaForBillOfLading()
                {
                    IdNomenclature = nom.Id,
                    Name = nom.Name,
                    CountAll = nom.Count,
                    CountSheeping = 0,
                    IsSheeping = false
                };
                nomenclatures.Add(n);
            }

            CreateBillOfLadingViewModel result = new CreateBillOfLadingViewModel()
            {
                NameOrganisation = "Организация",
                DateCreate = DateTime.Now,
                Nomenclature = nomenclatures
            };


            return result;
        }

        public CreateBillOfLadingViewModel Create(WarehousDBContext context, int idBillOdLading)
        {
            CreateBillOfLadingViewModel resul = new CreateBillOfLadingViewModel();
            List<NomenclaturaForBillOfLading> nomenclatures = new List<NomenclaturaForBillOfLading>();

            var billOfLading = context.BillOfLadings.Where(x => x.Id == idBillOdLading).FirstOrDefault();
            if (billOfLading != null)
            {
                resul.NameOrganisation = billOfLading.CustomerName;
                resul.DateCreate = billOfLading.DateCreat;

                var row = context.ItemsBillOfLadings.Where(x => x.BillOfLadingId == idBillOdLading).ToList();

                foreach (var item in row)
                {
                    //получаю номенклатуру изБД
                    var nomenclature = context.Nomenclatures.Where(x => x.Id == item.NomenclatureId).FirstOrDefault();

                    NomenclaturaForBillOfLading nomen = new NomenclaturaForBillOfLading()
                    {
                        Name = nomenclature.Name,
                        CountSheeping = item.Count
                    };
                    nomenclatures.Add(nomen);
                }
                resul.Nomenclature = nomenclatures;
            }
            return resul;
        }
    }
}
