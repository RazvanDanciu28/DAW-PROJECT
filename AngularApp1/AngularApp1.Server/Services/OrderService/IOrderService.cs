using AngularApp1.Server.Helpers;
using AngularApp1.Server.Models;

namespace AngularApp1.Server.Services.OrderService
{
    public interface IOrderService
    {
        Task<IEnumerable<Order>> GetAllOrdersAsync();
        Task<Order> GetOrderByIdAsync(Guid id);
        Task AddOrderAsync(Order order);
        Task<Order> CreateOrderAsync(Guid UserId, Product productAdded);
        Task CompleteOrderAsync(Guid orderId, OrderInformation orderInfo);
        Task<Order> GetOrderByUserIdAsync(Guid UserId);
    }
}
