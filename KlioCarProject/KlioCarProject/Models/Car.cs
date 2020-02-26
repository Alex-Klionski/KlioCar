using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KlioCarProject.Models
{
    public class Car
    {
        public int CarID { get; set; }
        public decimal Price { get; set; }
        public string Model { get; set; }
        public string Type { get; set; }
        public string Engine { get; set; }
    }
}
