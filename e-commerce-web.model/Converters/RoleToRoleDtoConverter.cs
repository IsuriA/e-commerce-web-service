using AutoMapper;
using e_commerce_web.model.Models;

namespace e_commerce_web.model.Converters
{
    public class RoleToRoleDtoConverter : ITypeConverter<Role, RoleDto>
    {
        public RoleDto Convert(Role source, RoleDto destination, ResolutionContext context)
        {
            if (destination == null)
            {
                destination = new RoleDto();
            }
            if (source == null)
            {
                return destination;
            }

            destination.Id = source.Id;
            destination.Name = source.Name;
            destination.AccessLevel = source.AccessLevel;

            return destination;
        }
    }
}
