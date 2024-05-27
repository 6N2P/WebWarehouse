using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace Webwarehouse.DataModels
{
    public class WarehousDBContext : DbContext
    {
       
        public WarehousDBContext(DbContextOptions<WarehousDBContext> options) 
            : base(options)
        {
           this.Database.EnsureCreated();
        }

        public DbSet<BillOfLading> BillOfLadings { get; set; }
        public DbSet<ItemsBillOfLading> ItemsBillOfLadings { get; set; }
        public DbSet<Nomenclature> Nomenclatures { get; set; }
    }
}
