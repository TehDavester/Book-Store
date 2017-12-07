using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Models
{
    public class CreditCard
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Credit card number required ")]
        [RegularExpression(@"^(\d{16})$", ErrorMessage = "Please enter valid credit card number")]
        public string CCNum { get; set; }


        [Required(ErrorMessage = "Holder Name is required ")]
        public String HolderName { get; set; }

        [Required(ErrorMessage ="cvc is required ")]
       [RegularExpression(@"^(\d{3})$" ,ErrorMessage ="Invalid cvc,3 digits only ")]
        public string  cvc { get; set; }

    }
}