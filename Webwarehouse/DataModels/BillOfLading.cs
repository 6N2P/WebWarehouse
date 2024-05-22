using System.ComponentModel.DataAnnotations;

namespace Webwarehouse.DataModels
{
    public class BillOfLading
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string CustomerName { get; set; } = null!;
        [Required]
        public DateTime DateCreat { get; set; }
    }
}
