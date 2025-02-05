using System.Linq;
using System.Web.Mvc;
using nimaptask.Models;

namespace nimaptask.Controllers
{
    public class ProductController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        private const int pageSize = 10;

        // Show all products
        public ActionResult Index(int page = 1)
        {
            // Calculate the skip value for pagination
            var products = db.Products.Include("Category")
                                      .OrderBy(p => p.ProductId)  
                                      .Skip((page - 1) * pageSize)  // Skip products based on the page number
                                      .Take(pageSize)  // Take only 10 products for the page
                                      .ToList();

            // Get total number of products to calculate total pages
            int totalProducts = db.Products.Count();
            int totalPages = (int)System.Math.Ceiling((double)totalProducts / pageSize);

            ViewBag.TotalPages = totalPages;
            ViewBag.CurrentPage = page;

            return View(products);
        }


        // Show form to add a new product
        public ActionResult Create()
        {
            ViewBag.Categories = db.Categories.ToList();
            return View();
        }

        // Add a new product
        [HttpPost]
        public ActionResult Create(Product product)
        {
            if (ModelState.IsValid)
            {
                db.Products.Add(product);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Categories = db.Categories.ToList();
            return View(product);
        }

        // Show form to edit a product
        public ActionResult Edit(int id)
        {
            var product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            ViewBag.Categories = db.Categories.ToList();
            return View(product);
        }

        // Update product
        [HttpPost]
        public ActionResult Edit(Product product)
        {
            
            var existingProduct = db.Products.SingleOrDefault(p => p.ProductId == product.ProductId);

            
            if (existingProduct == null)
            {
                return HttpNotFound(); 
            }

           
            existingProduct.ProductName = product.ProductName;
            existingProduct.CategoryId = product.CategoryId; 

         
            db.SaveChanges();

           
            return RedirectToAction("Index");
        }

        // Delete a product
        public ActionResult Delete(int id)
        {
            var product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            db.Products.Remove(product);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
