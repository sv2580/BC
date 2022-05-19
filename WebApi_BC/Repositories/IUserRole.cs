using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Models;

namespace WebApi.Repositories
{
    public interface IUserRoleRepository
    {
        Task<UserRole> Get(string user_email);
        Task<IEnumerable<UserRole>> GetAll();
        Task Add(UserRole userRole);
        Task Delete(string user_email);
        Task Update(UserRole userRole);
    }

    public class UserRoleRepository : IUserRoleRepository
    {
        private readonly IDataContext _context;
        public UserRoleRepository(IDataContext context)
        {
            _context = context;

        }
        public async Task Add(UserRole userRole)
        {
            _context.UserRoles.Add(userRole);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(string user_email)
        {
            var itemToRemove = await _context.UserRoles.FindAsync(user_email);
            if (itemToRemove == null)
                throw new NullReferenceException();

            _context.UserRoles.Remove(itemToRemove);
            await _context.SaveChangesAsync();
        }

        public async Task<UserRole> Get(string user_email)
        {
            return await _context.UserRoles.FindAsync(user_email);
        }

        public async Task<IEnumerable<UserRole>> GetAll()
        {
            return await _context.UserRoles.ToListAsync();
        }

        public async Task Update(UserRole userRole)
        {
            var itemToUpdate = await _context.UserRoles.FindAsync(userRole.User_email);
            if (itemToUpdate == null)
                throw new NullReferenceException();
            itemToUpdate.User_email = userRole.User_email;
            itemToUpdate.Role = userRole.Role;
            await _context.SaveChangesAsync();
        }
    }

}


