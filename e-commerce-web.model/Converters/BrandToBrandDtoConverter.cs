using AutoMapper;
using e_commerce_web.model.DTOs;
using e_commerce_web.model.Models;

namespace e_commerce_web.model.Converters
{
    public class BrandToBrandDtoConverter : ITypeConverter<Brand, BrandDto>
    {
        public BrandDto Convert(Brand source, BrandDto destination, ResolutionContext context)
        {
            if (destination == null)
            {
                destination = new BrandDto();
            }
            if (source == null)
            {
                return destination;
            }

            destination.Id = source.Id;
            destination.Name = source.Name;

            return destination;
        }
    }
}
