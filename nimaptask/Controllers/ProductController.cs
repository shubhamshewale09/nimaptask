using System.Linq;
using System.Web.Mvc;
using nimaptask.Models;

namespace nimaptask.Controllers
{
    public class ProductController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        private const int pageSize = 10;

        
        public ActionResult Index(int page = 1)
        {
            
            var products = db.Products.Include("Category")
                                      .OrderBy(p => p.ProductId)  
                                      .Skip((page - 1) * pageSize)  
                                      .Take(pageSize)  
                                      .ToList();

         
            int totalProducts = db.Products.Count();
            int totalPages = (int)System.Math.Ceiling((double)totalProducts / pageSize);

            ViewBag.TotalPages = totalPages;
            ViewBag.CurrentPage = page;

            return View(products);
        }


   
        public ActionResult Create()
        {
            ViewBag.Categories = db.Categories.ToList();
            return View();
        }

     
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
