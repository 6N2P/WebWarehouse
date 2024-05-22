using Webwarehouse.Models;

namespace Webwarehouse.Interfeces
{
    public interface ICreateBillOfLading
    {
        int CreateBillOfLading(CreateBillOfLadingViewModel billOfLading);
    }
}
