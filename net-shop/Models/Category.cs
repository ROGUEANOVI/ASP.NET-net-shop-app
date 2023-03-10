using System.ComponentModel.DataAnnotations;

namespace net_shop.Models
{
    public class Category
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "El nombre de la categoria es obligatorio")]
        public string Name { get; set; }

        [Required(ErrorMessage ="El numero de orden es obligatorio")]
        [Range(1, int.MaxValue, ErrorMessage ="El numero orden debe ser mayor que 0")]
        public int ShowOrder { get; set; }
    }
}
