using AssetManagement.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AssetManagement.Context
{
    public class MyContext : DbContext
    {
        public MyContext(DbContextOptions<MyContext> options) : base(options) { }
        public DbSet<User> User { get; set; }
        public DbSet<Request> Request { get; set; }
        public DbSet<Item> Item { get; set; }
        public DbSet<Borrow> Borrow { get; set; }
        public DbSet<Return> Return { get; set; }

    }
}
