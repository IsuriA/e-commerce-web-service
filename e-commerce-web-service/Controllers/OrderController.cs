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

        [HttpPost("addToOrder/{productId}")]
        [Authorize]
        public async Task<ActionResult> GetItemCountInCart(int productId)
        {
            await this.orderService.AddProductToOrder(productId);

            return Ok();
        }
    }
}
