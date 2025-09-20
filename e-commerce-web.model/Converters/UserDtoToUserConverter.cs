using System.Text;
using AutoMapper;
using e_commerce_web.core.DTOs;
using e_commerce_web.core.Models;

namespace e_commerce_web.core.Converters
{
    public class UserDtoToUserConverter : ITypeConverter<UserDto, User>
    {
        public User Convert(UserDto source, User destination, ResolutionContext context)
        {
            if (destination == null) {
                destination = new User();
            }
            if (source == null) { 
                return destination;
            }

            string passwordHash = System.Convert.ToBase64String(Encoding.UTF8.GetBytes(source.Password));

            destination.FirstName = source.FirstName;
            destination.LastName = source.LastName;
            destination.PhoneNumber = source.Phone;
            destination.Address = source.Address;
            destination.Username = source.Username;
            destination.Password = passwordHash;
            destination.Email = source.Email;

            return destination;
        }
    }
}
