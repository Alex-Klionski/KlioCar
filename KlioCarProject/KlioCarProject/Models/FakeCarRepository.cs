using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KlioCarProject.Models
{
    public class FakeCarRepository : ICarRepository
    {
        public IQueryable<Car> Cars => new List<Car>
        {
            new Car { Model="Tesla", Price = 54 },
            new Car { Model = "Bmw", Price = 34 },
            new Car { Model = "Audi", Price = 43 }
        }.AsQueryable<Car>();

    }
}
