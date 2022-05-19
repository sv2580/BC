using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Models;

namespace WebApi.Repositories
{
    public interface ICoursesRepository
    {
        Task<Course> Get(int id);
        Task<IEnumerable<Course>> GetAll();
        Task Add(Course course);
        Task Delete(int id);
        Task Update(Course course);
        int CourseExists(string name);

        public class CoursesRepository : ICoursesRepository
        {
            private readonly IDataContext _context;
            public CoursesRepository(IDataContext context)
            {
                _context = context;

            }
            public async Task Add(Course course)
            {
                    _context.Courses.Add(course);
                await _context.SaveChangesAsync();
            }

            public async Task Delete(int id)
            {
                var itemToRemove = await _context.Courses.FindAsync(id);
                if (itemToRemove == null)
                    throw new NullReferenceException();

                var chapt = _context.Chapters.Where(x => x.Id_course == id).ToList();
                if (chapt.Any() && chapt != null)
                {
                    foreach (var ch in chapt)
                    {
                        var lesson = _context.Lessons.Where(x => x.Id_chapter == ch.Id_chapter).ToList();
                        if (lesson.Any() && lesson != null)
                        {
                            foreach (var l in lesson)
                            {
                                _context.Lessons.Remove(l);
                            }
                        }
                        _context.Chapters.Remove(ch);
                    }

                }
                _context.Courses.Remove(itemToRemove);
                await _context.SaveChangesAsync();
            }

            public async Task<Course> Get(int id)
            {
                return await _context.Courses.FindAsync(id);
            }

            public async Task<IEnumerable<Course>> GetAll()
            {
                return await _context.Courses.ToListAsync();
            }

            public async Task Update(Course course)
            {
                var itemToUpdate = await _context.Courses.FindAsync(course.Id);
                if (itemToUpdate == null)
                    throw new NullReferenceException();
                itemToUpdate.Name = course.Name;
                itemToUpdate.Subject = course.Subject;
                itemToUpdate.Visibility = course.Visibility;
                itemToUpdate.Description = course.Description;
                await _context.SaveChangesAsync();
            }

            public int CourseExists(string name)
            {
                int result = -1;
                if(_context.Courses.Where(x => x.Name == name).Any())
                    result = _context.Courses.Where(x => x.Name == name).First().Id;
                return result;
            }
        }
    }
}
