using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace net_shop.Models
{
    public class Product
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage ="El nombre del producto es requerido")]
        public string Name { get; set; }

        [Required(ErrorMessage ="La descripcion del producto es requerida")]
        public string Description { get; set; }

        public string LongDescription { get; set; }

        [Required(ErrorMessage ="El precio del producto es requerido")]
        [Range(1, double.MaxValue, ErrorMessage ="El precio debe ser mayor a 0")]
        public double Price { get; set; }

        public string? ImageUrl { get; set; }

        //Foreigns Keys
        public int CategoryId { get; set; }

        [ForeignKey("CategoryId")]
        public virtual Category? Category { get; set; }

        public int AppTypeId { get; set; }

        [ForeignKey("AppTypeId")]
        public virtual AppType? AppType { get; set; }
    }
}
