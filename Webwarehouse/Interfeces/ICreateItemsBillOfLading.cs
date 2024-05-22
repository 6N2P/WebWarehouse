using Webwarehouse.Models;

namespace Webwarehouse.Interfeces
{
    public interface ICreateItemsBillOfLading
    {
        void CreateItemsBillOfLading(int idBillOfLading, List<NomenclaturaForBillOfLading> nomenclatures);
    }
}
