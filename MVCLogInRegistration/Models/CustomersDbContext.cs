using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace MVCLogInRegistration.Models
{
    public class CustomersDbContext: DbContext
    {

        public DbSet<UsserAccount> customerInformation { get; set;}


    }
}