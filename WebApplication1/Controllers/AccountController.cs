using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models;
namespace WebApplication1.Controllers
{
    public class AccountController : Controller
    {
        Databaseforbooks db = new Databaseforbooks();
        public bool Admincheck()
        {
            if (Session["UserName"] != null && (bool)Session["IsAdmin"] == true)
                return true;
            else return false;
        }

        // GET: Account
        public ActionResult Index()
        {
            if (Admincheck())
            {
                return View(db.accounts_table.ToList());

            }
            else return Content("You do not have access to this data");
        }
        //methods for registration
        [HttpGet]
        public ActionResult Register()
        {

            return View();

        }

        [HttpPost]

        public ActionResult Register(UsserAccount account)
        {


            if (ModelState.IsValid)// checks for form errors 
            {
                if (IsUnique(account))// check database for username -- username needs to be uniqie 
                {
                    db.accounts_table.Add(account);
                    db.SaveChanges();
                    TempData["Confirmation"] = account.FirstName + " " + account.LastName + " registration was successful";
                    //Confirmation message
                    return RedirectToAction("LogIn");
                }
                else ViewBag.message = "username or password is already in use, please use another.";
            }
            return View();

        }
        protected bool IsUnique(UsserAccount user)// method to check for username, email uniqueness 
        {
            var username = db.accounts_table.SingleOrDefault(a => a.UserName == user.UserName);
            var email = db.accounts_table.SingleOrDefault(a => a.Email == user.Email);
            if (username == null && email == null) return true;
            else return false;

        }
        //end methods for registration
        //begin methods for log in
        [HttpGet]
        public ActionResult SetCC()
        {

            if (!(Session["UsedId"] == null))
            {
                int id = (int)Session["UsedId"];
               UsserAccount usr= db.accounts_table.SingleOrDefault( u=> u.UsedId == id);
                if (usr.cardId==null)
                   return View();
            }
            return Content("not logged on or credit card information already set");
        }
        [HttpPost]
        public ActionResult SetCC(CreditCard card)
            {
            if (ModelState.IsValid)
            {
                db.card_table.Add(card);
                db.SaveChanges();
                var carid = db.card_table.SingleOrDefault(u => u.CCNum == card.CCNum).Id;
                int id = (int)Session["UsedId"];
                UsserAccount user = db.accounts_table.SingleOrDefault(u => u.UsedId == id);
                user.cardId = carid;
                db.Entry(user).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Customer_index", "bks");
            }
             return View();
            }
            


        [HttpPost]
        public ActionResult LogIn( UsserAccount user)
        { //check if password and username are in db 
            var usr = db.accounts_table.SingleOrDefault(u => u.UserName == user.UserName && u.Password == user.Password);
           
            if (usr != null)
            {

                Session["UsedId"] = usr.UsedId;
                Session["UserName"] = usr.UserName.ToString();
                Session["IsAdmin"] = usr.IsAdmin;


                return RedirectToAction("LoggedIn");

            }
            else ModelState.AddModelError("", "User name or password wrong ");
            return View();



         
        }// view for successfull login 
        [HttpGet]
        public ActionResult LogIn()
        {
            if (TempData["Confirmation"] != null)
                ViewBag.Message = TempData["Confirmation"].ToString();
            return View();
        }
        public ActionResult LoggedIn()
        {
            if (Session["UsedId"] != null)
            {

                return View();


            }
            else return RedirectToAction("LogIn");



        }
        public ActionResult Logout()
        {
            Session["UsedId"] = null;
            Session["UserName"] = null;
            Session["IsAdmin"] = null;
            string url = this.Request.UrlReferrer.AbsolutePath;

            return Redirect(url);
        }
    }
}