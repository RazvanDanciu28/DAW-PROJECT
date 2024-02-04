using AngularApp1.Server.DataContext;
using AngularApp1.Server.Models;
using Microsoft.EntityFrameworkCore;
using AngularApp1.Server.Services.OrderService;


namespace AngularApp1.Server.Services.OrderItemService
{
    public class OrderItemService : IOrderItemService
    {
        private readonly AppDbContext _context;
        public readonly IOrderService _orderService;

        public OrderItemService(AppDbContext context, IOrderService orderService)
        {
            _context = context;
            _orderService = orderService;
        }

        public async Task AddOrderItemAsync(Guid ProductId, Guid UserId)
        {
            var orderItem = new OrderItem();
            var product = await _context.Products.FirstOrDefaultAsync(p => p.ProductId == ProductId);
            orderItem.OrderItemId = Guid.NewGuid();
            orderItem.ProductId = ProductId;
            if (!_context.Orders.Any(x => x.UserId == UserId && x.Status == false))
            {
                var newOrder = await _orderService.CreateOrderAsync(UserId, product);
                orderItem.OrderId = newOrder.OrderId;
            }
            else
            {
                var currentOrder = await _orderService.GetOrderByUserIdAsync(UserId);
                orderItem.OrderId = currentOrder.OrderId;
                currentOrder.Amount = currentOrder.Amount + product.Price;
            }
            _context.OrderItems.Add(orderItem);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<OrderItem>> GetOrderItemsByOrderIdAsync(Guid Id)
        {
            var orderItems = await _context.OrderItems.Where(x => x.OrderId == Id).ToListAsync();
            return orderItems;
        }

        public async Task DeleteOrderItemFromOrderAsync(Guid OrderItemId, Guid UserId)
        {
            var orderItem = await _context.OrderItems.SingleOrDefaultAsync(x => x.OrderItemId == OrderItemId);
            var productToDelete = await _context.Products.SingleOrDefaultAsync(x => x.ProductId == orderItem.ProductId);
            var order = await _context.Orders.SingleOrDefaultAsync(x => x.UserId == UserId);
            order.Amount = order.Amount - productToDelete.Price;
            _context.OrderItems.Remove(orderItem);
            await _context.SaveChangesAsync();
        }
    }
}
