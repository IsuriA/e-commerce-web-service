using AutoMapper;
using e_commerce_web.model.DTOs;
using e_commerce_web.model.Models;

namespace e_commerce_web.model.Converters
{
    public class ProductToProductDtoConverter : ITypeConverter<Product, ProductDto>
    {
        //private readonly IMapper mapper;

        //public ProductToProductDtoConverter(IMapper mapper)
        //{
        //    this.mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        //}

        public ProductDto Convert(Product source, ProductDto destination, ResolutionContext context)
        {
            if (destination == null) {
                destination = new ProductDto();
            }
            if (source == null) { 
                return destination;
            }

            destination.Id = source.Id;
            destination.Name = source.Name;
            destination.Description = source.Description;
            destination.Price = source.Price;
            destination.Quantity = source.Quantity;
            destination.ImageUrl = source.ImageUrl;
            destination.Brand = new BrandDto { Id = source.Brand.Id, Name = source.Brand.Name };

            return destination;
        }
    }
}
