using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KlioCarProject.Models
{
    public interface ICarRepository
    {
        IQueryable<Car> Cars { get; }
        void SaveCar(Car car);
        
    }
}
