using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
namespace WebApplication1.Models
{
    public class ctgs
    {
        [Key]
        public int Id { get; set; }
        public string CategoryName { get; set; }
    }
}