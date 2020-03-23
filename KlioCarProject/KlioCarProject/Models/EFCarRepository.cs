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
        
        public void SaveCar(Car car)
        {
            if(car.CarID == 0)
            {
                context.Cars.Add(car);
            }
            else
            {
                Car dbEntry = context.Cars.FirstOrDefault(p => p.CarID == car.CarID);
                if(dbEntry != null)
                {
                    dbEntry.Model = car.Model;
                    dbEntry.Category = car.Category;
                    dbEntry.Price = car.Price;
                    dbEntry.Type = car.Type;
                    dbEntry.Engine = car.Engine;
                }
            }
            context.SaveChanges();
        }
    }
}
