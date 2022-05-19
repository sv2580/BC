using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Dto;
using WebApi.Models;
using WebApi.Repositories;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserRoleController : ControllerBase
    {
        private readonly IUserRoleRepository _repository;
        public UserRoleController(IUserRoleRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserRole>>> GetUserRoles()
        {
            var products = await _repository.GetAll();
            return Ok(products);
        }

        [HttpGet("{user_email}")]
        public async Task<ActionResult<UserRole>> GetUserRole(string user_email)
        {
            var product = await _repository.Get(user_email);
            if (product == null)
                return NotFound();

            return Ok(product);
        }

        [HttpPost]
        public async Task<ActionResult> CreateUserRole(CreateUserRoleDto createUserRole)
        {
            UserRole userRole = new()
            {
                User_email = createUserRole.User_email,
                Role = createUserRole.Role,
            };

            await _repository.Add(userRole);
            return Ok();
        }


        [HttpPut("{user_email}")]
        public async Task<ActionResult> UpdateUserRole(string user_email, UpdateUserRoleDto updateUserRoleDto)
        {
            UserRole userRole = new()
            {
                User_email = user_email,
                Role = updateUserRoleDto.Role,

            };

            await _repository.Update(userRole);
            return Ok();
        }
    }
}


