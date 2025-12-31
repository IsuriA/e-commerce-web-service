using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using AutoMapper;
using e_commerce_web.data;
using e_commerce_web.model;
using e_commerce_web.core.DTOs;
using e_commerce_web.core.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace e_commerce_web.service
{
    public class UserService
    {
        private UserDataManager _userDataManager;
        private readonly AppSettings _appSettings;
        private readonly IMapper mapper;
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly LookupDataManager lookupDataManager;

        public UserService(UserDataManager userDataManager
            , IOptions<AppSettings> appSettings
            , IHttpContextAccessor httpContextAccessor
            , LookupDataManager lookupDataManager
            , IMapper mapper)
        {
            _userDataManager = userDataManager ?? throw new ArgumentNullException(nameof(userDataManager));
            _appSettings = appSettings.Value;
            this.mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            this.httpContextAccessor = httpContextAccessor ?? throw new ArgumentNullException(nameof(httpContextAccessor));
            this.lookupDataManager = lookupDataManager ?? throw new ArgumentNullException(nameof(lookupDataManager));
        }

        public async Task<IEnumerable<User>> GetAll()
        {
            return await _userDataManager.GetUsersAsync();
        }

        public async Task<IEnumerable<User>> GetAllCustomers()
        {
            int customerRoleId = (await this.lookupDataManager.GetRolesAsync())
                .FirstOrDefault(os => os.Name.Equals("CUSTOMER", StringComparison.InvariantCultureIgnoreCase))?.Id
                ?? throw new ApplicationException($"{nameof(OrderStatus)} CUSTOMER is not configured");
            return await _userDataManager.GetUsersAsync(customerRoleId);
        }

        public async Task<AuthenticateResponse> AuthenticateAsync(AuthenticateRequest model)
        {
            string passwordHash = Convert.ToBase64String(Encoding.UTF8.GetBytes(model.Password));
            var user = await _userDataManager.GetUserAsync(model.Username, passwordHash);
            // return null if user not found
            if (user == null) return null;

            UserDto dto = this.mapper.Map<UserDto>(user);
            int userRoleId = user.UserRoleUsers.FirstOrDefault()?.RoleId
                ?? throw new ApplicationException("User doesn't have any role assigned");
            Role role = (await this.lookupDataManager.GetRolesAsync()).First(r => r.Id == userRoleId);

            dto.Role = this.mapper.Map<RoleDto>(role);

            // authentication successful so generate jwt token
            var token = GenerateJwtToken(dto);

            return new AuthenticateResponse(dto, token);
        }

        public async Task<User> GetByIdAsync(int id)
        {
            return await _userDataManager.GetByIdAsync(id);
        }

        // helper methods
        private string GenerateJwtToken(UserDto user)
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

        public async Task<User?> RegisterAsync(UserDto dto)
        {
            // Check if user already exists
            var existingUser = await _userDataManager.GetUserByUsernameAsync(dto.Username);
            if (existingUser != null)
                return null; // Username already taken

            // Hash the password before saving
            User userModel = this.mapper.Map<User>(dto);

            return await _userDataManager.AddUserAsync(userModel, dto.Role.Id);
        }

        public void Logout()
        {
            this.httpContextAccessor.HttpContext.Items = null;
        }
    }
}