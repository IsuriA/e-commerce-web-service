using AutoMapper;
using e_commerce_web.model.DTOs;
using e_commerce_web.model.Models;

namespace e_commerce_web.model.Converters
{
    public class ProductDtoToProductConverter : ITypeConverter<ProductDto, Product>
    {
        public Product Convert(ProductDto source, Product destination, ResolutionContext context)
        {
            if (destination == null) {
                destination = new Product();
            }
            if (source == null) { 
                return destination;
            }

            destination.Name = source.Name;
            destination.Description = source.Description;
            destination.Price = source.Price;
            destination.Quantity = source.Quantity;
            destination.CategoryId = source.Category?.Id ?? throw new ArgumentNullException(nameof(ProductDto.Category));
            destination.BrandId = source.Brand?.Id ?? throw new ArgumentNullException(nameof(ProductDto.Brand));
            destination.ImageUrl = source.ImageUrl;

            return destination;
        }
    }
}
