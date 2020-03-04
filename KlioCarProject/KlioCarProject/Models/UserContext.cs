using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace KlioCarProject.Models
{
    public class UserContext : DbContext
    {
        public UserContext() : base("DefaultConnection") { }
        public DbSet<User> Users { get; set; }
    }
}
