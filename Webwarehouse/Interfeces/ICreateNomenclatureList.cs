using Webwarehouse.DataModels;
using Webwarehouse.Models;

namespace Webwarehouse.Interfeces
{
    public interface ICreateNomenclatureList
    {
        NomenclaturaListViewModel CreateNomenclatureList(WarehousDBContext context);
    }
}
