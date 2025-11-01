using AutoMapper;
using e_commerce_web.core.DTOs;
using e_commerce_web.core.Models;

namespace e_commerce_web.core.Converters
{
    public class OrderStatusToOrderStatusDtoConverter : ITypeConverter<OrderStatus, OrderStatusDto>
    {
        public OrderStatusDto Convert(OrderStatus source, OrderStatusDto destination, ResolutionContext context)
        {
            if (destination == null)
            {
                destination = new OrderStatusDto();
            }
            if (source == null)
            {
                return destination;
            }

            destination.Id = source.Id;
            destination.Code = source.Code;
            destination.Name = source.Name;

            return destination;
        }
    }
}
