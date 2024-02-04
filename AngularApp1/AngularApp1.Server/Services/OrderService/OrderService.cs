using Microsoft.EntityFrameworkCore;
using AngularApp1.Server.DataContext;
using AngularApp1.Server.Models;
using AngularApp1.Server.Helpers;



namespace AngularApp1.Server.Services.OrderService
{
    public class OrderService : IOrderService
    {
        private readonly AppDbContext _context;
        public OrderService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Order> GetOrderByIdAsync(Guid id)
        {
            return await _context.Orders.SingleOrDefaultAsync(x => x.OrderId == id);
        }

        public async Task<IEnumerable<Order>> GetAllOrdersAsync()
        {
            return await _context.Orders.ToListAsync();
        }

        public async Task AddOrderAsync(Order order)
        {
            _context.Orders.Add(order);
            await _context.SaveChangesAsync();
        }

        public async Task<Order> CreateOrderAsync(Guid UserId, Product productAdded)
        {
            var order = new Order();
            order.OrderId = Guid.NewGuid();
            order.UserId = UserId;
            order.Status = false;
            order.Amount = productAdded.Price;
            order.Date = DateTime.Now;
            _context.Orders.Add(order);
            await _context.SaveChangesAsync();
            return order;
        }

        public async Task CompleteOrderAsync(Guid orderId, OrderInformation orderInfo)
        {
            var order = await _context.Orders.SingleOrDefaultAsync(x => x.OrderId == orderId);
            order.Address = orderInfo.Address;
            order.Date = DateTime.Now;
            order.Status = true;
            await _context.SaveChangesAsync();
        }

        public async Task<Order> GetOrderByUserIdAsync(Guid UserId)
        {
            var order = await _context.Orders.SingleOrDefaultAsync(x => x.UserId == UserId);
            return order;
        }
    }
}
