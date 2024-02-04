using AngularApp1.Server.Models;


namespace AngularApp1.Server.Services.OrderItemService
{
    public interface IOrderItemService
    {
        Task AddOrderItemAsync(Guid ProductId, Guid UserId);
        Task<IEnumerable<OrderItem>> GetOrderItemsByOrderIdAsync(Guid Id);
        Task DeleteOrderItemFromOrderAsync(Guid OrderItemId, Guid UserId);
    }
}
