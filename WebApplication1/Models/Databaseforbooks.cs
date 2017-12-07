using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
namespace WebApplication1.Models
{
    public class Databaseforbooks:DbContext
    {
        public DbSet<bks> books_table { get; set; }
        public DbSet<ctgs> categories_table  { get; set; } 
        public DbSet<UsserAccount> accounts_table { get; set; }
        public DbSet<CreditCard> card_table { get; set; }


    }
}