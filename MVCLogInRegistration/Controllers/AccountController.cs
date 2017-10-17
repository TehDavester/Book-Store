using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVCLogInRegistration.Models;


namespace MVCLogInRegistration.Controllers
{
    public class AccountController : Controller
    {
        CustomersDbContext db = new CustomersDbContext();



        // GET: Account
        public ActionResult Index()
        {
            return View(db.customerInformation.ToList());
        }
      // menthods for registration/////////////////////////////////////////////////////////////////////////////////////////////////////////////
        [HttpGet]
        public ActionResult Register()
        {
            return View();

        }

        [HttpPost]
        public ActionResult Register(UsserAccount account )
        {

            if (ModelState.IsValid)
            {
                
                if (IsUnique(account))
                {
                    db.customerInformation.Add(account);
                    db.SaveChanges();
                    ModelState.Clear();

                     
                    return RedirectToAction("Index");
                    ViewBag.Message = account.FirstName + " " + account.LastName + " registration was successful";
                }
                else ViewBag.message = "username or password is already in use, please use another.";
            }
           
            return View();

        }// end of methods for registration ///////////////////////////////////////////////////////////////////////////////////////////////////////

        // begin methods for LogIN//////////////////////////////////////////////////////////////////////////////////////////////////////////

        //validating uniqueness
        protected bool IsUnique( UsserAccount user)
        {
            var username = db.customerInformation.SingleOrDefault(a => a.UserName == user.UserName);
            var email = db.customerInformation.SingleOrDefault(a => a.Email == user.Email);
            if (username == null && email == null) return true;
            else return false;

        }// end of validation//////////////////////////////////////////////////////////////////

        public ActionResult LogIn()
        {
            return View();
        }

        [HttpPost]
        public ActionResult LogIn(UsserAccount user)
        {
           
                var usr = db.customerInformation.SingleOrDefault(u => u.UserName == user.UserName && u.Password == user.Password);
                if (usr != null)
                { 

                    Session["UsedId"] = usr.UsedId.ToString();
                    Session["UserName"] = usr.UserName.ToString();
                    Session["IsAdmin"] = usr.IsAdmin;


                    return RedirectToAction("LoggedIn");

                }
            
            else ModelState.AddModelError("", "User name or password wrong ");
            return View();

            

        }
        public ActionResult LoggedIn()
        {
            if (Session["UsedId"] != null)
            {
                
                return View();


            }
            else return RedirectToAction("LogIn");


        }//end methods for login ///////////////////////////////////////////////////////////////////////////////////////////////////////

    }
}