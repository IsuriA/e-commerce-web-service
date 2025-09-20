using System.Text;
using AutoMapper;
using e_commerce_web.core.DTOs;
using e_commerce_web.core.Models;

namespace e_commerce_web.core.Converters
{
    public class UserToUserDtoConverter : ITypeConverter<User, UserDto>
    {
        public UserDto Convert(User source, UserDto destination, ResolutionContext context)
        {
            if (destination == null) {
                destination = new UserDto();
            }
            if (source == null) { 
                return destination;
            }

            string passwordHash = System.Convert.ToBase64String(Encoding.UTF8.GetBytes(source.Password));

            destination.Id = source.Id;
            destination.FirstName = source.FirstName;
            destination.LastName = source.LastName;
            destination.Phone = source.PhoneNumber;
            destination.Address = source.Address;
            destination.Username = source.Username;
            destination.Email = source.Email;

            return destination;
        }
    }
}
