using System.Collections.Generic;
using Webwarehouse.DataModels;
using Webwarehouse.Interfeces;
using Webwarehouse.Models;

namespace Webwarehouse.Class
{
    public  class CreatorBillOfLoadingFromDB
    {
        public  CreateBillOfLadingViewModel Create(WarehousDBContext context, int idBillOfLading)
        {
            CreateBillOfLadingViewModel result = new CreateBillOfLadingViewModel();

            var billOfLading = context.BillOfLadings.Where(x => x.Id == idBillOfLading).FirstOrDefault();
            if (billOfLading != null)
            {
                var itemBillOfLading = context.ItemsBillOfLadings.Where(x => x.BillOfLadingId == idBillOfLading).ToList();

                List<NomenclaturaForBillOfLading> listNomenclature = new List<NomenclaturaForBillOfLading>();

                foreach (var item in itemBillOfLading)
                {
                    var nomenclature = context.Nomenclatures.Where(x => x.Id == item.NomenclatureId).FirstOrDefault();

                    if (nomenclature != null)
                    {
                        NomenclaturaForBillOfLading n = new NomenclaturaForBillOfLading
                        {
                            IdNomenclature = nomenclature.Id,
                            Name = nomenclature.Name,
                            CountAll = nomenclature.Count,
                            CountSheeping = item.Count,
                            IsSheeping = true
                        };

                        listNomenclature.Add(n);
                    }
                }
                result.NameOrganisation = billOfLading.CustomerName;
                result.DateCreate = billOfLading.DateCreat;
                result.Nomenclature = listNomenclature;
            }
            return result;
        }
    }
}
