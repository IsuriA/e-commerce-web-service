using AutoMapper;
using e_commerce_web.data;
using e_commerce_web.model.DTOs;
using e_commerce_web.model.Models;
using Microsoft.AspNetCore.Http;

namespace e_commerce_web.service
{
    public class OrderService
    {
        private readonly OrderDataManager orderDataManager;
        private readonly LookupDataManager lookupDataManager;
        private readonly IMapper mapper;
        private const string NEW_ORDER_STATUS_CODE = "NEW";
        private readonly IHttpContextAccessor httpContextAccessor;

        public OrderService(
            OrderDataManager orderDataManager,
            LookupDataManager lookupDataManager,
            IHttpContextAccessor httpContextAccessor,
            IMapper mapper)
        {
            this.orderDataManager = orderDataManager ?? throw new ArgumentNullException(nameof(orderDataManager));
            this.lookupDataManager = lookupDataManager ?? throw new ArgumentNullException(nameof(lookupDataManager));
            this.mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            this.httpContextAccessor = httpContextAccessor ?? throw new ArgumentNullException(nameof(httpContextAccessor));
        }

        public async Task<int> GetItemCountAsync()
        {
            UserDto user = (UserDto)this.httpContextAccessor.HttpContext.Items["User"];
            Order currentOrder = await this.GetCreateOrder(user.Id);

            return await orderDataManager.GetItemCountInCartAsync(user.Id, currentOrder.Id);
        }

        public async Task AddProductToOrder(int productId)
        {
            UserDto user = (UserDto)this.httpContextAccessor.HttpContext.Items["User"];
            Order currentOrder = await this.GetCreateOrder(user.Id);

            await this.orderDataManager.AddItemToOrder(new OrderProduct
            {
                OrderId = currentOrder.Id,
                ProductId = productId,
            });
        }

        private async Task<Order> GetCreateOrder(int userId) {
            int newOrderStatusId = (await this.lookupDataManager.GetOrderStatusesAsync())
                .FirstOrDefault(os => os.Code.Equals(NEW_ORDER_STATUS_CODE, StringComparison.InvariantCultureIgnoreCase))?.Id
                  ?? throw new ApplicationException($"{nameof(OrderStatus)} NEW is not configured");

            Order pendingOrder = await this.orderDataManager.GetOrderForUserByStatus(userId, newOrderStatusId);
            if (pendingOrder != null) {
                return pendingOrder;
            }

            return await this.orderDataManager.CreateNewOrder(new Order
            {
                UserId = userId,
                StatusId = newOrderStatusId,
            });
        }
    }
}