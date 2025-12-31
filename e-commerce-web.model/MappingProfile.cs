using AutoMapper;
using e_commerce_web.core.Converters;
using e_commerce_web.core.DTOs;
using e_commerce_web.core.Models;

namespace e_commerce_web.model
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<InquiryDto, Inquiry>().ConvertUsing<InquiryDtoToInqueryConverter>();
            CreateMap<ProductDto, Product>().ConvertUsing<ProductDtoToProductConverter>();
            CreateMap<UserDto, User>().ConvertUsing<UserDtoToUserConverter>();
            CreateMap<CheckoutDto, Payment>().ConvertUsing<CheckoutDtoToPaymentConverter>();
            CreateMap<PaymentDto, Payment>().ConvertUsing<PaymentDtoToPaymentConverter>();

            CreateMap<OrderStatus, OrderStatusDto>().ConvertUsing<OrderStatusToOrderStatusDtoConverter>();
            CreateMap<Payment, PaymentDto>().ConvertUsing<PaymentToPaymentDtoConverter>();
            CreateMap<Category, CategoryDto>().ConvertUsing<CategoryToCategoryDtoConverter>();
            CreateMap<Role, RoleDto>().ConvertUsing<RoleToRoleDtoConverter>();
            CreateMap<User, UserDto>().ConvertUsing<UserToUserDtoConverter>();
            CreateMap<Product, ProductDto>().ConvertUsing<ProductToProductDtoConverter>();
            CreateMap<Brand, BrandDto>().ConvertUsing<BrandToBrandDtoConverter>();
            CreateMap<Order, OrderDto>().ConvertUsing<OrderToOrderDtoConverter>();
            CreateMap<OrderProduct, OrderItemDto>().ConvertUsing<OrderProductToOrderItemDtoConverter>();
            CreateMap<PaymentMethod, PaymentMethodDto>().ConvertUsing<PaymentMethodToPaymentMethodDtoConverter>();
        }
    }
}