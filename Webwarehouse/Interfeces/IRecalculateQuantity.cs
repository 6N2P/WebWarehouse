using Webwarehouse.Models;

namespace Webwarehouse.Interfeces
{
    public interface IRecalculateQuantity
    {
        void RecalculateQuantity(List<NomenclaturaForBillOfLading> nomenclatureList);
    }
}
