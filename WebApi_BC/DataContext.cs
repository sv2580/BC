using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using WebApi.Models;


namespace WebApi
{
    public class DataContext : DbContext, IDataContext
    {
        DbSet<Course> IDataContext.Courses { get; set; }
        DbSet<Chapter> IDataContext.Chapters { get; set; }
        DbSet<Lesson> IDataContext.Lessons { get; set; }
        DbSet<UserRole> IDataContext.UserRoles { get; set; }
        DbSet<UserPermission> IDataContext.UserPermissions { get; set; }
        DbSet<User> IDataContext.Users { get; set; }


        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }


    }
}
