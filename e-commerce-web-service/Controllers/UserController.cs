using e_commerce_web.model;
using e_commerce_web.core.DTOs;
using e_commerce_web.core.Models;
using e_commerce_web.service;
using Microsoft.AspNetCore.Mvc;

namespace e_commerce_web_service.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : Controller
    {
        private readonly UserService userService;

        public UserController(UserService userService)
        {
            this.userService = userService ?? throw new ArgumentNullException(nameof(userService));
        }

        [HttpGet]
        [Authorize]
        public async Task<IEnumerable<User>> GetUsers()
        {
            return await this.userService.GetAll();
        }

        [HttpGet("customers")]
        //[Authorize]
        public async Task<IEnumerable<User>> GetAllCustomers()
        {
            return await this.userService.GetAllCustomers();
        }

        [HttpPost("authenticate")]
        public async Task<IActionResult> Authenticate(AuthenticateRequest model)
        {
            var response = await this.userService.AuthenticateAsync(model);

            if (response == null)
                return BadRequest(new { message = "Username or password is incorrect" });

            return Ok(response);
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] UserDto dto)
        {
            var user = await this.userService.RegisterAsync(dto);

            if (user == null)
                return BadRequest(new { message = "Username already exists." });

            return Ok(new { message = "User registered successfully." });
        }

        [HttpPost("logout")]
        public async Task<IActionResult> Logout()
        {
            this.userService.Logout();

            return Ok();
        }
    }
}