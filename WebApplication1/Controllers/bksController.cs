using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class bksController : Controller
    {
        private Databaseforbooks db = new Databaseforbooks();
        
        // GET: bks
        public ActionResult Index()
        {
            if (Admincheck())
            {
                var books_table = db.books_table.Include(b => b.Category);
                return View(books_table.ToList());
            }
            else return Content("You do not have access to this data ");
        }

        // GET: bks/Details/5
        public ActionResult Details(int? id)
        {
            if (Admincheck())
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                bks bks = db.books_table.Find(id);
                if (bks == null)
                {
                    return HttpNotFound();
                }
                return View(bks);
            }
            else return Content("You do not have acces to this data");
        }

        // GET: bks/Create
        public ActionResult Create()
        {
            if (Admincheck())
            {
                ViewBag.CategoryId = new SelectList(db.categories_table, "Id", "CategoryName");
                return View();  
            }else return Content("You do not have access to this data");
        }

        // POST: bks/Create
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,ISBN10,Title,Author,Publishdate,Price,Publisher,Stock,CategoryId")] bks bks)
        {
            
           
                if (ModelState.IsValid)
                {
                    db.books_table.Add(bks);
                    db.SaveChanges();
                    TempData["Confiramtion_of_books"] = "Book has been successfully added ";
                  
                    return RedirectToAction("Index");
                }
                ViewBag.CategoryId = new SelectList(db.categories_table, "Id", "CategoryName", bks.CategoryId);
                  return View();

            
          
           


            
        }

        // GET: bks/Edit/5
        public ActionResult Edit(int? id)
        {if (Admincheck())
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                bks bks = db.books_table.Find(id);
                if (bks == null)
                {
                    return HttpNotFound();
                }
                ViewBag.CategoryId = new SelectList(db.categories_table, "Id", "CategoryName", bks.CategoryId);
                return View(bks);
            }
            else return Content("You do not have access to this data");
        }

        // POST: bks/Edit/5
       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,ISBN10,Title,Author,Publishdate,Price,Stock,CategoryId")] bks bks)
        {
            if (ModelState.IsValid)
            {
                db.Entry(bks).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CategoryId = new SelectList(db.categories_table, "Id", "CategoryName", bks.CategoryId);
            return View(bks);
        }
        // GET: bks: bks/Customer_index


        
      
        public ActionResult Customer_index(string CategoryId)
        {
            
            
            var books_table = from r in db.books_table
                              where( r.Category.CategoryName == CategoryId || CategoryId == null ||CategoryId == "") && r.Stock>0// and this faggot as well if things go bad 
                              select r;
            ViewBag.CategoryId = new SelectList(db.categories_table, "categoryName", "CategoryName");
            return View("Customer_index",books_table.ToList());
        }
        // GET: bks/Delete/5
        public ActionResult Delete(int? id)
        {if (Admincheck())
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                bks bks = db.books_table.Find(id);
                if (bks == null)
                {
                    return HttpNotFound();
                }
                return View(bks);
            }
            else return Content("You do not have access to this data");
        }
        [HttpGet]
        public ActionResult Bookcart()
        {
            List<int> booklist;
            if (TempData["booklist"] == null)
            {
                booklist = new List<int>();
            }
            else
            { booklist = TempData["booklist"] as List<int>; }

            List<int> ids = booklist.Distinct().ToList();
            var books_table = from r in db.books_table
                              where ids.Contains(r.Id)
                              select r;
            TempData["booklist"] = ids;
            return View(books_table.ToList());
        }
        [HttpPost]
        public ActionResult Bookcart(List<int> quantity)
        {
            if (Session["UsedId"] == null)
                return RedirectToAction("LogIn", "Account", new { url = Request.Url.AbsoluteUri });
            int accountid = (int)Session["UsedId"];
            if (db.accounts_table.SingleOrDefault(u => u.UsedId == accountid).cardId == null)
                return RedirectToAction("SetCC", "Account", new { url = Request.Url.AbsoluteUri });
            if (TempData["booklist"] == null)
                return RedirectToAction("empty cart");
            List<int> booklist = TempData["booklist"] as List<int>;
            float price = 0;
            for (int i = 0; i < booklist.Count; i++)
            {
                int bookid = booklist[i];
                bks book = db.books_table.SingleOrDefault(u => u.Id == bookid);
                if (book.Stock < quantity[i])
                    return Content("not enough stock for book  " + book.Title);
                price += book.Price * quantity[i];
            }
            TempData["listquantities"] = quantity;/////if things fuck up take out this 
            return RedirectToAction("Checkout", new { booklist = booklist, quantity = quantity, price = price });
        }
        [HttpGet]
        public ActionResult Checkout(List<int> booklist,List<int> quantity,float price)
        {
            ViewBag.price = price;
            return View();
        }            

        [HttpPost]
        public ActionResult Checkout(List<int> booklist, List<int> quantity, float price,string yes)
        {
            booklist = TempData["booklist"]as List<int>;/// same here 
            quantity = TempData["listquantities"] as List<int>;// if things fuck up revome these two 

            for (int i = 0; i < booklist.Count; i++)
            {
                int bookid = booklist[i];
               
                bks book = db.books_table.SingleOrDefault(u => u.Id == bookid);
               
                book.Stock =book.Stock- quantity[i];
                db.Entry(book).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
           }
            Session["confirmation"] = "Your purchase has been completed";
            return RedirectToAction("ClearCart");
        }
        // POST: bks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            bks bks = db.books_table.Find(id);
            db.books_table.Remove(bks);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public bool Admincheck()//method to check if current person that wants access to these functionalities is an admin 
        {
            if (Session["UserName"] != null && (bool)Session["IsAdmin"] == true)
                return true;
            else return false;
        }
        public ActionResult ClearCart()
        {
            TempData["booklist"] = null;
            return RedirectToAction("Customer_index");
        }
        public ActionResult Addtocart(int? id)
        {
            List<int> booklist;
            if (TempData["booklist"] == null)
            {
               booklist  = new List<int>();
            }
            else
            { booklist = TempData["booklist"] as List<int>; }
            booklist.Add(id.Value);
            TempData["booklist"] = booklist;

            string url = this.Request.UrlReferrer.AbsolutePath;

            return Redirect(url);
        }
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
        
    }
}
