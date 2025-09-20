using AutoMapper;
using e_commerce_web.core.DTOs;
using e_commerce_web.core.Models;

namespace e_commerce_web.core.Converters
{
    public class OrderProductToOrderItemDtoConverter : ITypeConverter<OrderProduct, OrderItemDto>
    {
        public OrderItemDto Convert(OrderProduct source, OrderItemDto destination, ResolutionContext context)
        {
            if (destination == null) {
                destination = new OrderItemDto();
            }
            if (source == null) { 
                return destination;
            }

            destination.Id = source.Id;
            destination.Quantity = source.Quantity;
            destination.OrderId = source.OrderId;

            return destination;
        }
    }
}
