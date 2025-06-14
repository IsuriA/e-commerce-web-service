using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using e_commerce_web.data;
using e_commerce_web.model;
using e_commerce_web.model.Models;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace e_commerce_web.service
{
    public class UserService
    {
        private UserDataManager _userDataManager;
        private readonly AppSettings _appSettings;

        public UserService(UserDataManager userDataManager, IOptions<AppSettings> appSettings)
        {
            _userDataManager = userDataManager ?? throw new ArgumentNullException(nameof(userDataManager));
            _appSettings = appSettings.Value;
        }

        public async Task<IEnumerable<User>> GetAll()
        {
            return await _userDataManager.GetUsersAsync();
        }

        public async Task<AuthenticateResponse> AuthenticateAsync(AuthenticateRequest model)
        {
            var user = await _userDataManager.GetUserAsync(model.Username, model.Password);
            // return null if user not found
            if (user == null) return null;

            // authentication successful so generate jwt token
            var token = GenerateJwtToken(user);

            return new AuthenticateResponse(user, token);
        }

        public async Task<User> GetByIdAsync(int id)
        {
            return await _userDataManager.GetByIdAsync(id);
        }

        // helper methods
        private string GenerateJwtToken(User user)
        {
            // generate token that is valid for 7 days
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] { new Claim("id", user.Id.ToString()) }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }
    }
}