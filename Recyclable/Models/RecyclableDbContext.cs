using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace Recyclable.Models
{
    public class RecyclableDbContext : DbContext
    {
        public RecyclableDbContext() : base("name=DefaultConnection")
        {
        }
        public DbSet<RecyclableType> RecyclableTypes { get; set; }
        public DbSet<RecyclableItem> RecyclableItems { get; set; }
    }
}