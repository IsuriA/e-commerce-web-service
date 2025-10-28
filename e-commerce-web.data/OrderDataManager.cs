using e_commerce_web.core.Models;
using Microsoft.EntityFrameworkCore;

namespace e_commerce_web.data
{
    public class OrderDataManager
    {
        private readonly ECommerceDbContext context;

        public OrderDataManager(ECommerceDbContext dbContext)
        {
            this.context = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        public async Task<int> GetItemCountInCartAsync(int userId, int orderId)
        {
            return await this.context.OrderProducts
                .Include(op => op.Order)
                .Where(o => o.Order.UserId == userId && o.OrderId == orderId)
                .CountAsync();
        }

        public async Task<List<OrderProduct>> GetPaymentDueOrders()
        {
            return await this.context.OrderProducts
                .Include(op => op.Order)
                .Include(op => op.Order.User)
                .ToListAsync();
        }

        public async Task<Order> GetOrderForUserByStatus(int userId, int orderStatusId)
        {
            return await this.context.Orders
                .FirstOrDefaultAsync(o => o.UserId == userId && o.StatusId == orderStatusId);
        }

        public async Task<List<OrderProduct>> GetOrderItemsByOrderIdAsync(int orderId)
        {
            return await this.context.OrderProducts.Include(op => op.Product).Include(op => op.Product.Brand)
                .Where(o => o.OrderId == orderId).ToListAsync();
        }

        public async Task<Order> CreateNewOrderAsync(Order newOrder)
        {
            await this.context.Orders.AddAsync(newOrder);
            await this.context.SaveChangesAsync();

            return newOrder;
        }

        public async Task AddUpdateItemToOrder(OrderProduct orderItem)
        {
            if (orderItem.Id == 0)
            {
                await this.context.OrderProducts.AddAsync(orderItem);
            }

            await this.context.SaveChangesAsync();
        }

        public async Task RemoveProductFromOrder(OrderProduct orderProduct)
        {
            this.context.OrderProducts.Remove(orderProduct);
            await this.context.SaveChangesAsync();
        }

        public async Task CheckoutAsync(Payment payment)
        {
            await this.context.Payments.AddAsync(payment);
            await this.context.SaveChangesAsync();
        }

        public async Task UpdateOrderStatusAsync(int orderId, int orderStatusId)
        {
            Order order = await this.context.Orders
                .FirstAsync(o => o.Id == orderId);
            order.StatusId = orderStatusId;
            await this.context.SaveChangesAsync();
        }
    }
}