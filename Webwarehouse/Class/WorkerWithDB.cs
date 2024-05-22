using Webwarehouse.DataModels;
using Webwarehouse.Interfeces;
using Webwarehouse.Models;

namespace Webwarehouse.Class
{
    public class WorkerWithDB : IGetNomenclature, ICreateNomenclature,IRecalculateQuantity, ICreateBillOfLading, ICreateItemsBillOfLading
    {
        public WorkerWithDB(WarehousDBContext context)
        {
            _context = context;
        }

        private WarehousDBContext  _context;
        public void CreateNomenclature(Nomenclature nomenclature)
        {
            if(nomenclature != null)
            {
                _context.Nomenclatures.Add(nomenclature);
                _context.SaveChanges();
            }
        }

        public List<Nomenclature> GetNomenclatureList()
        {
            var nomenclatureFromDB= _context.Nomenclatures.ToList();
            if(nomenclatureFromDB != null && nomenclatureFromDB.Count>0)
            {
                return nomenclatureFromDB;
            }
            else
            {
                return new List<Nomenclature>()
                {
                    new Nomenclature(){Name = "Пусто"}
                };
            }

        }

        public void RecalculateQuantity(List<NomenclaturaForBillOfLading> nomenclatureList)
        {
            if (nomenclatureList != null && nomenclatureList.Count > 0)
            {
                foreach(var n in nomenclatureList)
                {
                    var nomenclatureFromDB = _context.Nomenclatures.Where(x=>x.Id == n.IdNomenclature).FirstOrDefault();
                    if (nomenclatureFromDB != null)
                    {
                        nomenclatureFromDB.Count = n.CountAll - n.CountSheeping;
                        _context.SaveChanges();
                    }
                }
            }
        }

        public int CreateBillOfLading(CreateBillOfLadingViewModel billOfLading)
        {
            int id = 0;
            if(billOfLading != null && billOfLading.Nomenclature != null && billOfLading.Nomenclature.Count>0)
            {
                BillOfLading billOfLadingFromDB = new BillOfLading()
                {
                    CustomerName = billOfLading.NameOrganisation,
                    DateCreat = billOfLading.DateCreate.ToUniversalTime()
                };

                _context.BillOfLadings.Add(billOfLadingFromDB);
                _context.SaveChanges();
                id = billOfLadingFromDB.Id;
                return id;
            }

            return id;
        }

        public void CreateItemsBillOfLading(int idBillOfLading, List<NomenclaturaForBillOfLading> nomenclatures)
        {
            if(idBillOfLading != 0 && nomenclatures != null && nomenclatures.Count > 0)
            {
                foreach(var n in nomenclatures)
                {
                    if (n.IsSheeping == true)
                    {
                        ItemsBillOfLading itemsBillOfLading = new ItemsBillOfLading()
                        {
                            BillOfLadingId = idBillOfLading,
                            NomenclatureId = n.IdNomenclature,
                            Count = n.CountSheeping
                        };
                        _context.ItemsBillOfLadings.Add(itemsBillOfLading);
                        _context.SaveChanges();
                    }
                }
            }
        }
    }
}
