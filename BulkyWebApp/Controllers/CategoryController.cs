using BulkyWebApp.Data;
using BulkyWebApp.Models;
using Microsoft.AspNetCore.Mvc;

namespace BulkyWebApp.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ApplicationDbContext _db;

        public CategoryController(ApplicationDbContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            var objectCategoryList = _db.Categories.ToList();
            return View(objectCategoryList);
        }

        //GET
        public IActionResult Create()
        {
            return View();
        }

        //GET
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Category obj)
        {
            if (obj.Name == obj.DisplayOrder.ToString())
            {
                ModelState.AddModelError("name", "The DisplayOrder Cannot exactly match the Name.");
            }
            if (ModelState.IsValid)
            {
                _db.Categories.Add(obj);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(obj);
        }
    }
}
