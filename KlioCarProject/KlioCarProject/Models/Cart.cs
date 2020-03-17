using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KlioCarProject.Models
{
    public class Cart
    {
        private List<CartLine> lineCollection = new List<CartLine>();
        public virtual void AddItem(Car car, int quantity)
        {
            CartLine line = lineCollection
                .Where(p => p.Car.CarID == car.CarID)
                .FirstOrDefault();

            if (line == null)
            {
                lineCollection.Add(new CartLine
                {
                    Car = car,
                    Quantity = quantity
                }); ;
            }
            else
            {
                line.Quantity += quantity;
            }
        }

        public virtual void RemoveLine(Car car) =>
            lineCollection.RemoveAll(l => l.Car.CarID == car.CarID);

        public virtual decimal ComputeTotalValue() =>
            lineCollection.Sum(e => e.Car.Price * e.Quantity);

        public virtual void Clear() => lineCollection.Clear();

        public virtual IEnumerable<CartLine> Lines => lineCollection;
    }

    public class CartLine
    {
        public int CartLineID { get; set; }
        public Car Car { get; set; }
        public int Quantity { get; set; }
    }
}