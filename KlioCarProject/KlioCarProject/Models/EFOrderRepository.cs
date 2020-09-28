using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace KlioCarProject.Models
{
    public class EFOrderRepository : IOrderRepository
    {
        private ApplicationDbContext context;
        public EFOrderRepository(ApplicationDbContext ctx)
        {
            context = ctx;
        }
        public IQueryable<Order> Orders => context.Orders
            .Include(o => o.Lines)
            .ThenInclude(l => l.Car);

        public void SaveOrder(Order order)
        {
            context.AttachRange(order.Lines.Select(l => l.Car));
            if (order.OrderID == 0)
            {
                context.Orders.Add(order);
            }
            context.SaveChanges();
        }
    }
}
   