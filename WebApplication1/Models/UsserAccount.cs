using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
namespace WebApplication1.Models
{
    public class UsserAccount
    {


        [Key]
        public int UsedId { get; set; }// primary key 



        [Required(ErrorMessage = "Please enter your First Name")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Please enter your LastName")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Please enter your UserName")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Please enter your Email")]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Please enter your Phone number")]
        
        public double  PhoneNumber { get; set; }


        [Required(ErrorMessage = "Please enter your address")]
        public string Address { get; set; }

        public CreditCard card { get; set; }
        public int? cardId { get; set; }


        [Required(ErrorMessage = "Please enter your Passwordress")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Compare("Password", ErrorMessage = "Passwords need to match")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }

        public bool IsAdmin { get; set; }




    }
}