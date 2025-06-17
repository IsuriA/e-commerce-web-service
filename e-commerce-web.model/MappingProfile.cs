using AutoMapper;
using e_commerce_web.model.Converters;
using e_commerce_web.model.DTOs;
using e_commerce_web.model.Models;

namespace e_commerce_web.model
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<InquiryDto, Inquiry>().ConvertUsing<InquiryDtoToInqueryConverter>();
            CreateMap<ProductDto, Product>().ConvertUsing<ProductDtoToProductConverter>();
            CreateMap<UserDto, User>().ConvertUsing<UserDtoToUserConverter>();
            CreateMap<Category, CategoryDto>().ConvertUsing<CategoryToCategoryDtoConverter>();
            CreateMap<Role, RoleDto>().ConvertUsing<RoleToRoleDtoConverter>();
            CreateMap<User, UserDto>().ConvertUsing<UserToUserDtoConverter>();
            CreateMap<Product, ProductDto>().ConvertUsing<ProductToProductDtoConverter>();
            CreateMap<Brand, BrandDto>().ConvertUsing<BrandToBrandDtoConverter>(); 
        }
    }
}
