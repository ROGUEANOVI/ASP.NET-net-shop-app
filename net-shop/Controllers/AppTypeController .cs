using Microsoft.AspNetCore.Mvc;
using net_shop.Data;
using net_shop.Models;

namespace net_shop.Controllers
{
    public class AppTypeController : Controller
    {
        private readonly AppDbContext _dbContext;

        public AppTypeController(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public IActionResult Index()
        {
            IEnumerable<AppType> listAppType = _dbContext.AppType;
            
            return View(listAppType);
        }

        //GET CREATE
        public IActionResult Create()
        {
            return View();
        }

        //POST CREATE
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(AppType appType)
        {
            if (ModelState.IsValid)
            {
                _dbContext.AppType.Add(appType);
                _dbContext.SaveChanges();
                return RedirectToAction(nameof(Index));
            }

            return View(appType);
        }

        //GET EDIT
        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            var appType = _dbContext.AppType.Find(id);

            return View(appType);
        }


        //POST EDIT
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(AppType appType)
        {
            if (ModelState.IsValid)
            {
                _dbContext.AppType.Update(appType);
                _dbContext.SaveChanges();
                return RedirectToAction(nameof(Index));
            }

            return View(appType);
        }

        //GET DELETE
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            var appType = _dbContext.AppType.Find(id);

            return View(appType);
        }


        //POST DELETE
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(AppType appType)
        {
            if (appType == null)
            {
                return NotFound();
            }

            _dbContext.AppType.Remove(appType);
            _dbContext.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
    }
}
