using System.ComponentModel.DataAnnotations;

namespace Webwarehouse.DataModels
{
    public class ItemsBillOfLading
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public int BillOfLadingId { get; set; }
        [Required]
        public int NomenclatureId { get; set; }
        [Required]
        public int Count { get; set; }
    }
}
