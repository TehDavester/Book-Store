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
    public class ctgsController : Controller
    {
        public bool Admincheck()
        {
            if (Session["UserName"] != null && (bool)Session["IsAdmin"] == true)
                return true;
            else return false;
        }
        private Databaseforbooks db = new Databaseforbooks();
       

        // GET: ctgs
        public ActionResult Index()
        {
            if (Admincheck())
            { return View(db.categories_table.ToList()); }

            else return Content("You do not have access to this data ");

        }

        // GET: ctgs/Details/5
        public ActionResult Details(int? id)
        {if (Admincheck())
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                ctgs ctgs = db.categories_table.Find(id);
                if (ctgs == null)
                {
                    return HttpNotFound();
                }
                return View(ctgs);
            }
          
                    else return Content("You do not have access to this data ");
        }

        // GET: ctgs/Create
        public ActionResult Create()
        {if (Admincheck())
            { return View(); }
            else return Content("You do not have access to this data ");
        }

        // POST: ctgs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,CategoryName")] ctgs ctgs)
        {
            if (ModelState.IsValid)
            {
                db.categories_table.Add(ctgs);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(ctgs);
        }

        // GET: ctgs/Edit/5
        public ActionResult Edit(int? id)
        {if (Admincheck())
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                ctgs ctgs = db.categories_table.Find(id);
                if (ctgs == null)
                {
                    return HttpNotFound();
                }
                return View(ctgs);
            }
            else return Content("You do not have access to this data ");
        }

        // POST: ctgs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,CategoryName")] ctgs ctgs)
        {
            if (ModelState.IsValid)
            {
                db.Entry(ctgs).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(ctgs);
        }

        // GET: ctgs/Delete/5
        public ActionResult Delete(int? id)
        {if (Admincheck())
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                ctgs ctgs = db.categories_table.Find(id);
                if (ctgs == null)
                {
                    return HttpNotFound();
                }
                return View(ctgs);
            }
            else return Content("You do not have access to this data ");
        }

        // POST: ctgs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ctgs ctgs = db.categories_table.Find(id);
            db.categories_table.Remove(ctgs);
            db.SaveChanges();
            return RedirectToAction("Index");
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
