using e_commerce_web.core.Models;
using Microsoft.EntityFrameworkCore;

namespace e_commerce_web.data
{
    public class IcartDataManager
    {
        private readonly ECommerceDbContext context;

        public List<CartItem> GetCart(string userId)
        {
            if (!_carts.ContainsKey(userId))
                _carts[userId] = new List<CartItem>();

            return _carts[userId];
        }

        public void AddToCart(string userId, CartItem item)
        {
            var cart = GetCart(userId);
            var existing = cart.FirstOrDefault(x => x.ProductId == item.ProductId);

            if (existing != null)
                existing.Quantity += item.Quantity;
            else
                cart.Add(item);
        }

        public void RemoveFromCart(string userId, int productId)
        {
            var cart = GetCart(userId);
            var item = cart.FirstOrDefault(x => x.ProductId == productId);
            if (item != null)
                cart.Remove(item);
        }

        public void ClearCart(string userId)
        {
            _carts[userId] = new List<CartItem>();
        }
    }
};