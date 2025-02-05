using System.Data.Entity.Infrastructure;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using nimaptask.Models;

namespace nimaptask.Controllers
{
    public class CategoryController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // Show all categories
        public ActionResult Index()
        {
            var categories = db.Categories.ToList();
            return View(categories);
        }

        // Show form to add a new category
        public ActionResult Create()
        {
            return View();
        }

        // Add a new category
        [HttpPost]
        public ActionResult Create(Category category)
        {
            if (ModelState.IsValid)
            {
                db.Categories.Add(category);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(category);
        }

        // Show form to edit a category
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Category category = db.Categories.Find(id);
            if (category == null)
            {
                return HttpNotFound();
            }

            return View(category);
        }

        // Update category
        [HttpPost]
        public ActionResult Edit(Category category)
        {
         
            var existingCategory = db.Categories.SingleOrDefault(c => c.CategoryId == category.CategoryId);

            
            if (existingCategory == null)
            {
                ModelState.AddModelError("", "The category no longer exists.");
                return View(category); 
            }

           
            existingCategory.CategoryName = category.CategoryName;

          
            db.SaveChanges();

        
            return RedirectToAction("Index");
        }








        // Delete a category
        public ActionResult Delete(int id)
        {
            var category = db.Categories.Find(id);
            if (category == null)
            {
                return HttpNotFound();
            }
            db.Categories.Remove(category);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
