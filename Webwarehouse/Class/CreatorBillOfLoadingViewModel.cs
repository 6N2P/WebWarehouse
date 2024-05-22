using Webwarehouse.DataModels;
using Webwarehouse.Interfeces;
using Webwarehouse.Models;

namespace Webwarehouse.Class
{
    public class CreatorBillOfLoadingViewModel : ICreateBillOfLadingViewModel
    {
        public CreateBillOfLadingViewModel Create(WarehousDBContext context)
        {
            IGetNomenclature createNomenclatureList =new WorkerWithDB(context);
            var nomenclatureFromDB = createNomenclatureList.GetNomenclatureList();
            List<NomenclaturaForBillOfLading> nomenclatures = new List<NomenclaturaForBillOfLading>();
            foreach(var nom in nomenclatureFromDB)
            {
                NomenclaturaForBillOfLading n = new NomenclaturaForBillOfLading()
                {
                    IdNomenclature = nom.Id,
                    Name = nom.Name,
                    CountAll =nom.Count,
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
    }
}
