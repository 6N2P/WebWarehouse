using Webwarehouse.DataModels;
using Webwarehouse.Interfeces;
using Webwarehouse.Models;

namespace Webwarehouse.Class
{
    public class CreatorBillOfLadingsList : ICreateBilletOfLadingsList
    {
        public CreatorBillOfLadingsList(WarehousDBContext warehousDBContext) 
        { 
            _dbContext = warehousDBContext;
        }


        private WarehousDBContext _dbContext;
        public BillOfLadingsListViewModel CreateBilletOfLadingsList()
        {
            WorkerWithDB worker = new WorkerWithDB(_dbContext);
            var list = worker.GetBillOfLadingsList();
            return new BillOfLadingsListViewModel()
            {
                BillOfLadings = list
            };
        }
    }
}
