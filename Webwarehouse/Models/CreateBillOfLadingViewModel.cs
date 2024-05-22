namespace Webwarehouse.Models
{
    public class CreateBillOfLadingViewModel
    {
        public string NameOrganisation { get; set; } = null!;
        public DateTime DateCreate { get; set; }
        public List<NomenclaturaForBillOfLading> Nomenclature { get; set; } = null!;

    }
    public class NomenclaturaForBillOfLading
    {
        public int IdNomenclature { get; set; }
        public string Name { get; set; } = null!;
        public int CountAll { get; set; }
        public int CountSheeping { get; set; }
        public bool IsSheeping { get; set; } = false;
    }
}
