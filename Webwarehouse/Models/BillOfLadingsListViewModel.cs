using Webwarehouse.DataModels;

namespace Webwarehouse.Models
{
    public class BillOfLadingsListViewModel
    {
        public int IdBillOfLading {  get; set; }
        public IEnumerable<BillOfLading>? BillOfLadings { get; set; }
    }
}
