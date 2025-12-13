using e_commerce_web.core.DTOs;
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

        [HttpGet("paymentDueOrders")]
        [Authorize]
        public async Task<ActionResult> GetPaymentDueOrders()
        {
            List<OrderDto> dueOrders = await this.orderService.GetPaymentDueOrders();

            return Ok(dueOrders);
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

        [HttpPost("checkout")]
        public async Task<IActionResult> Register([FromBody] CheckoutDto dto)
        {
            await this.orderService.CheckoutAsync(dto);

            return Ok(new { message = "Order checked out successfully." });
        }

        [HttpGet("{orderId}")]
        [Authorize]
        public async Task<ActionResult> GetById(int orderId)
        {
            OrderDto order = await this.orderService.GetOrderByIdAsync(orderId);

            return Ok(order);
        }

        [HttpGet("payment-info/{orderId}")]
        //[Authorize]
        public async Task<ActionResult> GetPaymentInfo(int orderId)
        {
            List<PaymentDto> order = await this.orderService.GetPaymentInfoAsync(orderId);

            return Ok(order);
        }
    }
}
