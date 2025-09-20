using e_commerce_web.core.Models;
using e_commerce_web.service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using e_commerce_web.core.DTOs;
using e_commerce_web.data;

namespace e_commerce_web_service.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    { 
        CartService inquiryService;
    
        private readonly ProductService prodcutService;
        private readonly IWebHostEnvironment hostEnvironment;

        public CartController(CartDataManager cartDataManager)
        {
            _cartDataManager = cartDataManager;
        }

        // GET: api/cart/{userId}
        [HttpGet("{userId}")]
        public ActionResult<List<CartItem>> GetCart(string userId)
        {
            return Ok(_cartDataManager.GetCart(userId));
        }

        // POST: api/cart/add/{userId}
        [HttpPost("add/{userId}")]
        public IActionResult AddToCart(string userId, [FromBody] CartItem item)
        {
            _cartDataManager.AddToCart(userId, item);
            return Ok(new { message = "Item added to cart." });
        }

        // DELETE: api/cart/remove/{userId}/{productId}
        [HttpDelete("remove/{userId}/{productId}")]
        public IActionResult RemoveFromCart(string userId, int productId)
        {
            _cartDataManager.RemoveFromCart(userId, productId);
            return Ok(new { message = "Item removed." });
        }

        // DELETE: api/cart/clear/{userId}
        [HttpDelete("clear/{userId}")]
        public IActionResult ClearCart(string userId)
        {
            _cartDataManager.ClearCart(userId);
            return Ok(new { message = "Cart cleared." });
        }
    }
}
