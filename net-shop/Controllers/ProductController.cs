using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using net_shop.Data;
using net_shop.Models;
using net_shop.Models.ViewModels;

namespace net_shop.Controllers
{
    public class ProductController : Controller
    {
        private readonly AppDbContext _dbContext;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public ProductController(AppDbContext dbContext, IWebHostEnvironment webHostEnvironment)
        {
            _dbContext = dbContext;
            _webHostEnvironment = webHostEnvironment;
        }

        public IActionResult Index()
        {
            IEnumerable<Product> listProduct = _dbContext.Product
                .Include(c => c.Category).Include(a => a.AppType);

            return View(listProduct);
        }

        //GET UPSERT
        public IActionResult Upsert(int? id)
        {
            ProductVM productVM = new ProductVM()
            {
                Product = new Product(),
                ListCategories = _dbContext.Category.Select(c => new SelectListItem
                {
                    Text = c.Name,
                    Value = c.Id.ToString()
                }),
                ListAppsTypes = _dbContext.AppType.Select(c => new SelectListItem
                {
                    Text = c.Name,
                    Value = c.Id.ToString()
                })
            };

            Product product = new Product();
            if (id == null)
            {
                return View(productVM);
            }
            else
            {
                productVM.Product = _dbContext.Product.Find(id);
                if (product == null)
                {
                    return NotFound();
                }
                return View(productVM);
            }
        }

        //POST UPSERT
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upsert(ProductVM productVM)
        {
            if (ModelState.IsValid)
            {
                var files = HttpContext.Request.Form.Files;
                string webRootPath = _webHostEnvironment.WebRootPath;
                if (productVM.Product.Id == 0)
                {
                    // Create
                    string upload = webRootPath + WC.ImagePath;
                    string fileName = Guid.NewGuid().ToString();
                    string extension = Path.GetExtension(files[0].FileName);

                    using (var fileStream = new FileStream(Path.Combine(upload, fileName + extension), FileMode.Create))
                    {
                        files[0].CopyTo(fileStream);
                    };

                    productVM.Product.ImageUrl = fileName + extension;

                    _dbContext.Product.Add(productVM.Product);

                }
                else 
                { 
                    // Update
                    var product = _dbContext.Product.AsNoTracking().FirstOrDefault(p => p.Id == productVM.Product.Id);

                    if (files.Count > 0)
                    {
                        string upload = webRootPath + WC.ImagePath;
                        string fileName = Guid.NewGuid().ToString();
                        string extension = Path.GetExtension(files[0].FileName);

                        // Delete img back
                        var imgRemoved = Path.Combine(upload, product.ImageUrl);
                        if(System.IO.File.Exists(imgRemoved))
                        {
                            System.IO.File.Delete(imgRemoved);
                        }

                        // Create new img
                        using (var fileStream = new FileStream(Path.Combine(upload, fileName + extension), FileMode.Create))
                        {
                            files[0].CopyTo(fileStream);
                        };
                        productVM.Product.ImageUrl = fileName + extension;
                    }
                    else
                    {
                        productVM.Product.ImageUrl = product.ImageUrl;
                    }

                    _dbContext.Product.Update(productVM.Product);
                }
                _dbContext.SaveChanges();
                return RedirectToAction("Index");
            }

            // Load list
            productVM.ListCategories = _dbContext.Category.Select(c => new SelectListItem
            {
                Text = c.Name,
                Value = c.Id.ToString()
            });
            productVM.ListAppsTypes = _dbContext.AppType.Select(c => new SelectListItem
            {
                Text = c.Name,
                Value = c.Id.ToString()
            });

            return View(productVM);
        }

        //GET DELETE
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            Product product = _dbContext.Product
                .Include(c => c.Category)
                .Include(a => a.AppType).FirstOrDefault(a => a.Id == id);

            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }


        //POST DELETE
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(Product product)
        {
            if (product == null)
            {
                return NotFound();
            }

            string upload = _webHostEnvironment.WebRootPath + WC.ImagePath;

            // Delete img back
            var imgRemoved = Path.Combine(upload, product.ImageUrl);
            if (System.IO.File.Exists(imgRemoved))
            {
                System.IO.File.Delete(imgRemoved);
            }

            _dbContext.Product.Remove(product);
            _dbContext.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
    }
}
