using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CarCatalog.Core;

namespace CarCatalog.Persistence
{
    public class UnitOfWork:IUnitOfWork
    {
        private readonly CarCatalogDbContext context;
        public UnitOfWork(CarCatalogDbContext context)
        {
            this.context = context;
        }
        public async Task CompleteAsync()
        {
            await context.SaveChangesAsync();

        }
    }
}
