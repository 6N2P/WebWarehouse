using System.ComponentModel.DataAnnotations;

namespace Webwarehouse.DataModels
{
    public class Nomenclature
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; } = null!;
        
        [Range(0, int.MaxValue, ErrorMessage = "Только числа больше 0")]
        public int Count { get; set; }
    }
}
