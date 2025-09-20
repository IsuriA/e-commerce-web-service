using AutoMapper;
using e_commerce_web.core.DTOs;
using e_commerce_web.core.Models;

namespace e_commerce_web.core.Converters
{
    public class CategoryToCategoryDtoConverter : ITypeConverter<Category, CategoryDto>
    {
        public CategoryDto Convert(Category source, CategoryDto destination, ResolutionContext context)
        {
            if (destination == null) {
                destination = new CategoryDto();
            }
            if (source == null) { 
                return destination;
            }

            destination.Id = source.Id;
            destination.Code = source.Code;
            destination.Name = source.Name;
            destination.Description = source.Description;

            return destination;
        }
    }
}
