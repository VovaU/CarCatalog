using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CarCatalog.Core;
using CarCatalog.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace CarCatalog.Persistence
{
    public class VehicleRepositoy:IVehicleRepository 
    {
        private readonly CarCatalogDbContext context;

        public VehicleRepositoy(CarCatalogDbContext context)
        {
            this.context = context;
        }

        public async Task<Vehicle> GetVehicle(int id,bool includeRelated=true)
        {
            if (!includeRelated)
                return await context.Vehicles.FindAsync(id);
            return  await context.Vehicles
                .Include(v => v.Features)
                .ThenInclude(vf => vf.Feature)
                .Include(v => v.Model)
                .ThenInclude(m => m.Make)
                .SingleOrDefaultAsync(v => v.Id == id);
        }

        public void Add(Vehicle vehicle)
        {
            context.Vehicles.Add(vehicle);
        }

        public void Remove(Vehicle vehicle)
        {
            context.Remove(vehicle);
        }
}
}
