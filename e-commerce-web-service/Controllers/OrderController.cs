using e_commerce_web.core.Models;
using e_commerce_web.service;
using Microsoft.AspNetCore.Mvc;

namespace e_commerce_web_service.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        OrderService orderService;

        public OrderController(OrderService orderService)
        {
            this.orderService = orderService ?? throw new ArgumentNullException(nameof(orderService));
        }

        [HttpGet("itemCount")]
        [Authorize]
        public async Task<ActionResult> GetItemCountInCart()
        {
            int itemCount = await this.orderService.GetItemCountAsync();

            return Ok(itemCount);
        }

        [HttpGet("items")]
        [Authorize]
        public async Task<ActionResult> GetItemsInCart()
        {
            int itemCount = await this.orderService.GetItemCountAsync();

            return Ok(itemCount);
        }

        [HttpGet("current")]
        [Authorize]
        public async Task<ActionResult> GetCurrentOrder()
        {
            OrderDto currentOrder = await this.orderService.GetCurrentOrderAsync();

            return Ok(currentOrder);
        }


        [HttpPost("addToOrder/{productId}")]
        [Authorize]
        public async Task<ActionResult> AddToOder(int productId)
        {
            await this.orderService.AddProductToOrder(productId);

            return Ok();
        }

        [HttpPost("updateQuantity/{productId}/{quantity}")]
        [Authorize]
        public async Task<ActionResult> UpdateQuantity(int productId, int quantity)
        {
            await this.orderService.UpdateQuantity(productId,quantity);
            return Ok();
        }

    }
}
