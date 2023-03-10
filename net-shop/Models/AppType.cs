using System.ComponentModel.DataAnnotations;

namespace net_shop.Models
{
    public class AppType
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "El nombre de la app es obligatorio")]
        public string Name { get; set; }
    }
}
