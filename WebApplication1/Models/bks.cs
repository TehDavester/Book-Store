using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Models
{
    public class bks
    {

        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Isbn is required ")]
        public int ISBN10 { get; set; }

        [Required(ErrorMessage = "Title is required")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Author is required")]
        public string Author { get; set; }

        [Required(ErrorMessage = "Date is required")]
        public DateTime Publishdate { get; set; }

        [Required(ErrorMessage = "Price is required")]
        public float Price { get; set; }

        [Required(ErrorMessage = "Publisher is required")]
        public string Publisher { get; set; }

        

        public int Stock { get; set; }
        //delete this to restore previous 
        public ctgs Category { get; set; }
        public int CategoryId{get;set;}



    }
}