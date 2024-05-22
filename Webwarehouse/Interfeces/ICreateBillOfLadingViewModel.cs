using Webwarehouse.Class;
using Webwarehouse.DataModels;
using Webwarehouse.Models;

namespace Webwarehouse.Interfeces
{
    public interface ICreateBillOfLadingViewModel
    {
        CreateBillOfLadingViewModel Create(WarehousDBContext context);
    }
}
