using AutoMapper;
using e_commerce_web.core.DTOs;
using e_commerce_web.core.Models;

namespace e_commerce_web.core.Converters
{
    public class PaymentDtoToPaymentConverter : ITypeConverter<PaymentDto, Payment>
    {
        public Payment Convert(PaymentDto source, Payment destination, ResolutionContext context)
        {
            if (destination == null)
            {
                destination = new Payment();
            }
            if (source == null)
            {
                return destination;
            }

            destination.Id = source.Id;
            destination.Reference = source.Reference;
            destination.Amount = source.Amount;
            destination.OrderId = source.OrderId;
            destination.Installment = source.Installment;
            destination.DueDate = source.DueDate;
            destination.CreatedAt = source.CreatedAt;
            destination.PaymentDate = source.PaymentDate;
            destination.ShippingReceiver = source.ShippingReceiver;
            destination.ShippingEmail = source.ShippingEmail;
            destination.ShippingPhone = source.ShippingPhone;
            destination.ShippingReceiverNic = source.ShippingReceiverNic;
            destination.ShippingAddress = source.ShippingAddress;
            destination.SpecialInstructions = source.SpecialInstructions;

            return destination;
        }
    }
}