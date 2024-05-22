using System.Diagnostics.Metrics;
using Webwarehouse.DataModels;
using Webwarehouse.Interfeces;
using Webwarehouse.Models;

namespace Webwarehouse.Class
{
    public class CreatorNomenclatureList : ICreateNomenclatureList
    {
        public NomenclaturaListViewModel CreateNomenclatureList(WarehousDBContext context)
        {
            NomenclaturaListViewModel result = new NomenclaturaListViewModel();
            
            IGetNomenclature workerWithDB = new WorkerWithDB(context);
            var nomenclatureList = workerWithDB.GetNomenclatureList();
            if (nomenclatureList != null)
            { 
                result.Nomenclatures = nomenclatureList;
            }
            return result;
        }
    }
}
