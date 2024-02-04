using Microsoft.AspNetCore.Mvc;
using System.Net;
using AngularApp1.Server.Services.OrderItemService;
using AngularApp1.Server.Services.OrderService;
using AngularApp1.Server.Helpers;



namespace AngularApp1.Server.Controllers
{
    [Route("app/[controller]")]
    [ApiController]
    public class OrderController : Controller
    {
        private readonly IOrderService _orderService;
        private readonly IOrderItemService _orderItemService;

        public OrderController(IOrderService orderService, IOrderItemService orderItemService)
        {
            _orderService = orderService;
            _orderItemService = orderItemService;
        }

        [HttpPost("{UserId}/{ProductId}")]
        public async Task<IActionResult> AddOrderItemAsync([FromRoute] Guid UserId, [FromRoute] Guid ProductId)
        {
            try
            {
                await _orderItemService.AddOrderItemAsync(ProductId, UserId);
                return StatusCode((int)HttpStatusCode.Created);
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.BadRequest, ex.Message);
            }
        }

        [HttpPut("{userId}")]
        public async Task<IActionResult> CompleteOrderAsync([FromRoute] Guid UserId, [FromBody] OrderInformation orderInfo)
        {
            var order = await _orderService.GetOrderByUserIdAsync(UserId);
            try
            {
                await _orderService.CompleteOrderAsync(order.OrderId, orderInfo);
                return StatusCode((int)HttpStatusCode.Created);
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.BadRequest, ex.Message);    
            }
        }

        [HttpDelete("{userId}/{orderItemId}")]
        public async Task<IActionResult> DeleteOrderItemAsync([FromRoute] Guid UserId, [FromRoute] Guid orderItemId)
        {
            try
            {
                await _orderItemService.DeleteOrderItemFromOrderAsync(orderItemId, UserId);
                return StatusCode((int)HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.BadRequest, ex.Message);
            }
            
        }

    }
}
