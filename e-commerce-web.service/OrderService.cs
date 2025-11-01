using AutoMapper;
using e_commerce_web.data;
using e_commerce_web.core.DTOs;
using e_commerce_web.core.Models;
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
            Order currentOrder = await this.GetCreateNewOrder(user.Id);

            return await this.orderDataManager.GetItemCountInCartAsync(user.Id, currentOrder.Id);
        }

        public async Task<OrderDto> GetCurrentOrderAsync()
        {
            UserDto user = (UserDto)this.httpContextAccessor.HttpContext.Items["User"];
            Order pendingOrderModel = await this.GetCreateNewOrder(user.Id);
            OrderDto pendingOrderDto = this.mapper.Map<OrderDto>(pendingOrderModel);

            List<OrderProduct> orderItemModels = await this.orderDataManager.GetOrderItemsByOrderIdAsync(pendingOrderModel.Id);
            pendingOrderDto.Items = orderItemModels.Select(obj =>
            {
                OrderItemDto itemDto = this.mapper.Map<OrderItemDto>(obj);
                itemDto.Product = this.mapper.Map<ProductDto>(obj.Product);

                return itemDto;
            }).ToList();

            return pendingOrderDto;
        }

        public async Task<OrderDto> GetOrderByIdAsync(int orderId)
        {
            UserDto user = (UserDto)this.httpContextAccessor.HttpContext.Items["User"];
            Role role = (await this.lookupDataManager.GetRolesAsync()).First(r => r.Id == user.Role.Id);
            RoleDto roleDto = this.mapper.Map<RoleDto>(role);
            Order orderModel = await this.orderDataManager.GetOrderById(orderId, roleDto.AccessLevel >= 100 ? user.Id : null);
            if (orderModel == null) {
                return new OrderDto();
            }

            OrderDto orderDto = this.mapper.Map<OrderDto>(orderModel);
            orderDto.Status = this.mapper.Map<OrderStatusDto>(
                (await this.lookupDataManager.GetOrderStatusesAsync()).FirstOrDefault(os => os.Id == orderModel.StatusId));
            List<OrderProduct> orderItemModels = await this.orderDataManager.GetOrderItemsByOrderIdAsync(orderModel.Id);
            orderDto.Items = orderItemModels.Select(obj =>
            {
                OrderItemDto itemDto = this.mapper.Map<OrderItemDto>(obj);
                itemDto.Product = this.mapper.Map<ProductDto>(obj.Product);

                return itemDto;
            }).ToList();

            return orderDto;
        }

        public async Task<List<OrderDto>> GetPaymentDueOrders()
        {
            List<OrderProduct> orderItemModels = await this.orderDataManager.GetPaymentDueOrders();

            return orderItemModels.GroupBy(o => o.Order).Select(o =>
            {
                OrderDto orderDto = this.mapper.Map<OrderDto>(o.Key);
                orderDto.User = this.mapper.Map<UserDto>(o.Key.User);
                orderDto.Items = orderItemModels.Select(obj =>
                {
                    OrderItemDto itemDto = this.mapper.Map<OrderItemDto>(obj);
                    itemDto.Product = this.mapper.Map<ProductDto>(obj.Product);

                    return itemDto;
                }).ToList();

                return orderDto;
            }).ToList();
        }

        public async Task AddProductToOrder(int productId)
        {
            UserDto user = (UserDto)this.httpContextAccessor.HttpContext.Items["User"];
            Order currentOrder = await this.GetCreateNewOrder(user.Id);

            List<OrderProduct> orderItemModels = await this.orderDataManager.GetOrderItemsByOrderIdAsync(currentOrder.Id);
            OrderProduct orderProduct = orderItemModels.FirstOrDefault(obj => obj.ProductId == productId);
            if (orderProduct != null)
            {
                orderProduct.Quantity++;
            }
            else
            {
                orderProduct = new OrderProduct
                {
                    OrderId = currentOrder.Id,
                    ProductId = productId,
                    Quantity = 1
                };
            }

            await this.orderDataManager.AddUpdateItemToOrder(orderProduct);
        }

        public async Task UpdateQuantity(int productId, int quantity)
        {
            UserDto user = (UserDto)this.httpContextAccessor.HttpContext.Items["User"];
            Order currentOrder = await this.GetCreateNewOrder(user.Id);

            List<OrderProduct> orderItemModels = await this.orderDataManager.GetOrderItemsByOrderIdAsync(currentOrder.Id);
            OrderProduct orderProduct = orderItemModels.FirstOrDefault(obj => obj.ProductId == productId);

            if (orderProduct != null)
            {
                orderProduct.Quantity = orderProduct.Quantity + quantity;

                if (orderProduct.Quantity == 0)
                {
                    await this.orderDataManager.RemoveProductFromOrder(orderProduct);
                }
                else
                {
                    await this.orderDataManager.AddUpdateItemToOrder(orderProduct);
                }
            }
        }

        private async Task<Order> GetCreateNewOrder(int userId)
        {
            int newOrderStatusId = (await this.lookupDataManager.GetOrderStatusesAsync())
                .FirstOrDefault(os => os.Code.Equals(NEW_ORDER_STATUS_CODE, StringComparison.InvariantCultureIgnoreCase))?.Id
                ?? throw new ApplicationException($"{nameof(OrderStatus)} NEW is not configured");

            Order pendingOrder = await this.orderDataManager.GetOrderForUserByStatus(userId, newOrderStatusId);
            if (pendingOrder != null)
            {
                return pendingOrder;
            }

            return await this.orderDataManager.CreateNewOrderAsync(new Order
            {
                UserId = userId,
                StatusId = newOrderStatusId,
            });
        }

        public async Task CheckoutAsync(CheckoutDto dto)
        {
            UserDto user = (UserDto)this.httpContextAccessor.HttpContext.Items["User"];
            Payment paymentModel = this.mapper.Map<Payment>(dto);

            paymentModel.MethodId = (await this.lookupDataManager.GetPaymentMethodsAsync())
                .FirstOrDefault(os => os.Code.Equals(dto.PaymentMethod, StringComparison.InvariantCultureIgnoreCase))?.Id
                ?? throw new ApplicationException($"{nameof(PaymentMethod)} {dto.PaymentMethod} is not configured");
            paymentModel.UserId = user.Id;

            int processingOrderStatusId = (await this.lookupDataManager.GetOrderStatusesAsync())
                .FirstOrDefault(os => os.Code.Equals("PROCESSING", StringComparison.InvariantCultureIgnoreCase))?.Id
                ?? throw new ApplicationException($"{nameof(OrderStatus)} PROCESSING is not configured");

            if (dto.PaymentMethod == "PAY_NOW")
            {
                paymentModel.DueDate = DateTime.Now;
                paymentModel.Installment = 1;
                paymentModel.Amount = dto.Total;
                await orderDataManager.CheckoutAsync(paymentModel);
                await orderDataManager.UpdateOrderStatusAsync(paymentModel.OrderId, processingOrderStatusId);
                return;
            }

            DateTime dueDate = DateTime.Now;
            for (int i = 1; i <= dto.Installments; i++)
            {
                dueDate = dueDate.AddMonths(1);
                paymentModel.DueDate = dueDate;
                paymentModel.Installment = i;
                paymentModel.Id = 0;
                paymentModel.Amount = dto.Total / dto.Installments;
                await orderDataManager.CheckoutAsync(paymentModel);
            }

            await orderDataManager.UpdateOrderStatusAsync(paymentModel.OrderId, processingOrderStatusId);
        }
    }
}