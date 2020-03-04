using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KlioCarProject.Models
{
    public class EFCarRepository : ICarRepository
    {
        private ApplicationDbContext context;
        public EFCarRepository(ApplicationDbContext ctx)
        {
            context = ctx;
        }
        public IQueryable<Car> Cars => context.Cars;
    }
}
