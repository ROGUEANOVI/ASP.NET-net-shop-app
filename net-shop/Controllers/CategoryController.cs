using Microsoft.AspNetCore.Mvc;
using net_shop.Data;
using net_shop.Models;

namespace net_shop.Controllers
{
    public class CategoryController : Controller
    {
        private readonly AppDbContext _dbContext;

        public CategoryController(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public IActionResult Index()
        {
            IEnumerable<Category> listCategory = _dbContext.Category;
            
            return View(listCategory);
        }

        //GET CREATE
        public IActionResult Create()
        {
            return View();
        }

        //POST CREATE
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Category category)
        {
            if (ModelState.IsValid)
            {
                _dbContext.Category.Add(category);
                _dbContext.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View(category);
        }

        //GET EDIT
        public IActionResult Edit(int? id)
        {
            if (id==null || id==0)
            {
                return NotFound();
            }

            var category = _dbContext.Category.Find(id);

            return View(category);
        }


        //POST EDIT
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Category category)
        {
            if (ModelState.IsValid)
            {
                _dbContext.Category.Update(category);
                _dbContext.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View(category);

        }

        //GET DELETE
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            var category = _dbContext.Category.Find(id);

            return View(category);
        }


        //POST DELETE
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(Category category)
        {   
            if (category == null)
            {
                return NotFound();
            }

            _dbContext.Category.Remove(category);
            _dbContext.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

    }
}
