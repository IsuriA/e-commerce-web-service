using e_commerce_web.model.Models;
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

        public async Task<Order> GetOrderForUserByStatus(int userId, int orderStatusId)
        {
            return await this.context.Orders
                .FirstOrDefaultAsync(o => o.UserId == userId && o.StatusId == orderStatusId);
        }

        public async Task<Order> CreateNewOrder(Order newOrder)
        {
            await this.context.Orders.AddAsync(newOrder);
            await this.context.SaveChangesAsync();

            return newOrder;
        }

        public async Task AddItemToOrder(OrderProduct ordeItem) {
            await this.context.OrderProducts.AddAsync(ordeItem);
            await this.context.SaveChangesAsync();
        }
    }
}