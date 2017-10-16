using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
namespace MVCLogInRegistration.Models
{
    public class Books
    {   [Key]
        public int Id { get; set; }
        [Required]
        public int ISBN10 { get; set; }
        [Required ]
        public string Title { get; set; }

        [Required]
        public string Author { get; set; }
        [Required]
        public DateTime Publishdate { get; set; }

        [Required]
        public float Price { get; set; }


        public int Stock { get; set; }

        public string Category { get; set; }

        
   }
}