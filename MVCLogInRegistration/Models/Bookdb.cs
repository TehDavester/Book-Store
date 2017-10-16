using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;


namespace MVCLogInRegistration.Models
{
    public class Bookdb: DbContext
    {
        public DbSet<Books> book_database { get; set; }

    }
}