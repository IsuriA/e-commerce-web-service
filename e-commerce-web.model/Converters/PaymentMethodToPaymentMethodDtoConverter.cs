using AutoMapper;
using e_commerce_web.core.DTOs;
using e_commerce_web.core.Models;

namespace e_commerce_web.core.Converters
{
    public class PaymentMethodToPaymentMethodDtoConverter : ITypeConverter<PaymentMethod, PaymentMethodDto>
    {
        public PaymentMethodDto Convert(PaymentMethod source, PaymentMethodDto destination, ResolutionContext context)
        {
            if (destination == null)
            {
                destination = new PaymentMethodDto();
            }
            if (source == null)
            {
                return destination;
            }

            destination.Id = source.Id;
            destination.Name = source.Name;
            destination.Code = source.Code;

            return destination;
        }
    }
}
