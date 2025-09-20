using AutoMapper;
using e_commerce_web.core.Models;

namespace e_commerce_web.core.Converters
{
    public class OrderToOrderDtoConverter : ITypeConverter<Order, OrderDto>
    {
        public OrderDto Convert(Order source, OrderDto destination, ResolutionContext context)
        {
            if (destination == null) {
                destination = new OrderDto();
            }
            if (source == null) { 
                return destination;
            }

            destination.Id = source.Id;
            destination.Amount = source.Amount;
            destination.CreatedAt = source.CreatedAt;

            return destination;
        }
    }
}
