
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using WebApi.Dto;
using WebApi.Models;
using WebApi.Repositories;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _repository;
        private readonly IDataContext _context;
        private readonly ApplicationSettings _settings;

        public UserController(IUserRepository userrepo, IDataContext dataContext, IOptions<ApplicationSettings> settings)
        {
            _repository = userrepo;
            _context = dataContext;
            _settings = settings.Value;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetUsers()
        {
            var idk = _settings.JWT_Secret;
            var products = await _repository.GetAll();
            return Ok(products);
        }

        [HttpGet("{email}")]
        public async Task<ActionResult<User>> GettUser(string email)
        {
            var product = await _repository.Get(email);
            if (product == null)
                return NotFound();

            return Ok(product);
        }
        [HttpPost]
        public async Task<ActionResult> CreateUser(CreateUserDto createUser)
        {
            User user = new()
            {
                User_email = createUser.User_email,
                User_password = createUser.User_password
            };
            await _repository.Add(user);
            return Ok();
        }

        [HttpPost]
        [Route("Login")]
        public async Task<ActionResult> Login(CreateUserDto createUser)
        {
            
            var user = await _context.Users.FindAsync(createUser.User_email);
            var userRole = await _context.UserRoles.FindAsync(createUser.User_email);
            if (user != null && createUser.User_password == user.User_password)
            {
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new Claim[]
                    {
                        new Claim("user_email",user.User_email.ToString()),
                        new Claim("role",userRole.Role.ToString()),
                    }),
                    Expires = DateTime.UtcNow.AddMinutes(25),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_settings.JWT_Secret)), SecurityAlgorithms.HmacSha256Signature)
                };
                var tokenHandler = new JwtSecurityTokenHandler();
                var securityToken = tokenHandler.CreateToken(tokenDescriptor);
                var token = tokenHandler.WriteToken(securityToken);
                return Ok(new { token });
            }
            else
                return BadRequest(new { message = "Username or password is incorrect." });
            _context.Users.Add(user);

        }


        [HttpDelete("{email}")]
        public async Task<ActionResult> Delete(string email)
        {
            await _repository.Delete(email);
            return Ok();
        }

        [HttpGet]
        [Authorize]
        [Route("UserInfo")]
        public async Task<ActionResult> GetUserProfile()
        {
            string id = User.Claims.First(c => c.Type == "user_email").Value;
            var role = await _context.UserRoles.FindAsync(id);
            if (role == null) {
                return NotFound();
            }
            return Ok(role);

        }
    }
}
