using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using WebApi.Models;

namespace WebApi.Repositories
{
    public interface IUserRepository
    {
           
        Task<User> Get(string email);
        Task<IEnumerable<User>> GetAll();
        Task Add(User user);
        Task Delete(string email);

    }

    public class UserRepository : IUserRepository
    {
        private readonly IDataContext _context;
        public UserRepository(IDataContext context)
        {
            _context = context;

        }
     
        public async Task<User> Get(string email)
        {
            return await _context.Users.FindAsync(email);
        }

        public async Task<IEnumerable<User>> GetAll()
        {
            return await _context.Users.ToListAsync();
        }

        public async Task Add(User user)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
        }



        public async Task Delete(string email)
        {
            var itemToRemove = await _context.Users.FindAsync(email);
            if (itemToRemove == null)
                throw new NullReferenceException();

            _context.Users.Remove(itemToRemove);
            await _context.SaveChangesAsync();
        }
    }
}
