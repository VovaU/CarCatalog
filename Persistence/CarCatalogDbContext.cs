using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CarCatalog.Models;
using Microsoft.EntityFrameworkCore;

namespace CarCatalog.Persistence
{
    public class CarCatalogDbContext:DbContext
    {
        public CarCatalogDbContext(DbContextOptions<CarCatalogDbContext> options)
        :base(options)
        {
        }
        public DbSet<Make> Makes { get; set; }
        public DbSet<Feature> Features { get; set; }

    }
}
