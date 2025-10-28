using System.Text;
using AutoMapper;
using e_commerce_web.core.DTOs;
using e_commerce_web.core.Models;

namespace e_commerce_web.core.Converters
{
    public class CheckoutDtoToPaymentConverter : ITypeConverter<CheckoutDto, Payment>
    {
        public Payment Convert(CheckoutDto source, Payment destination, ResolutionContext context)
        {
            if (destination == null) {
                destination = new Payment();
            }
            if (source == null) { 
                return destination;
            }
      
            destination.ShippingReceiver = source.Name;
            destination.ShippingEmail = source.Email;
            destination.ShippingPhone = source.Phone;
            destination.ShippingAddress = source.Address;
            destination.ShippingReceiverNic = source.NIC;
            destination.SpecialInstructions = source.Instructions;
            destination.Reference = source.Reference;
            destination.OrderId = source.OrderId;

            return destination;
        }
    }
}