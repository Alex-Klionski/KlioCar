using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KlioCarProject.Models.ViewModels
{
    public class CarsListViewModel
    {
        public IEnumerable<Car> Cars { get; set; }
        public PagingInfo PagingInfo { get; set; }
    }
}
