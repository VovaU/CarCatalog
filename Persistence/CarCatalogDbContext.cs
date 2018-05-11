using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CarCatalog.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace CarCatalog.Persistence
{
    public class CarCatalogDbContext:DbContext
    {
        public DbSet<Make> Makes { get; set; }
        public DbSet<Feature> Features { get; set; }
        public DbSet<Vehicle> Vehicles { get; set; }

        public CarCatalogDbContext(DbContextOptions<CarCatalogDbContext> options)
        :base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<VehicleFeature>().HasKey(vf=>new {vf.VehicleId,vf.FeatureId});
            
        }

    }
}
