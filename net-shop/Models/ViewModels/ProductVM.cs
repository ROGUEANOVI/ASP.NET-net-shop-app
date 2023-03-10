using Microsoft.AspNetCore.Mvc.Rendering;

namespace net_shop.Models.ViewModels
{
    public class ProductVM
    {
        public Product Product { get; set; }

        public IEnumerable<SelectListItem>? ListCategories { get; set; }

        public IEnumerable<SelectListItem>? ListAppsTypes { get; set; }
    }
}
