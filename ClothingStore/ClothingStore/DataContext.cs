using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace ClothingStore
{
    class DataContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source = ClothingData.db ");
        }
        public DbSet<Clothing> Clothes { get; set; }
        public DbSet<Type> Types { get; set; }

    }
}
