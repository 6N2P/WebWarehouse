using System.ComponentModel.DataAnnotations;

namespace Webwarehouse.DataModels
{
    public class Nomenclature
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; } = null!;
        [Required]
        public int Count { get; set; }
    }
}
