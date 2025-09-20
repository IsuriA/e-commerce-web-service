using e_commerce_web.core.DTOs;

namespace e_commerce_web.model
{
    public class AuthenticateResponse
    {
        public UserDto User { get; set; }

        public string Token { get; set; }

        public AuthenticateResponse(UserDto user, string token)
        {
            User = user;
            Token = token;
        }
    }
}
