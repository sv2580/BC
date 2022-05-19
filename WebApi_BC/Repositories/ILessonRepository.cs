using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Models;

namespace WebApi.Repositories
{
    public interface ILessonRepository
    {
        Task<Lesson> Get(int id);
        Task<IEnumerable<Lesson>> GetAll();
        Task Add(Lesson lesson);
        Task Delete(int id);
        Task Update(Lesson lesson);
        bool LessonExists(string name, int id);
    }

    public class LessonRepository : ILessonRepository
    {
        private readonly IDataContext _context;
        public LessonRepository(IDataContext context)
        {
            _context = context;

        }
        public async Task Add(Lesson lesson)
        {
            _context.Lessons.Add(lesson);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(int Id_lesson)
        {
            var itemToRemove = await _context.Lessons.FindAsync(Id_lesson);
            if (itemToRemove == null)
                throw new NullReferenceException();

            _context.Lessons.Remove(itemToRemove);
            await _context.SaveChangesAsync();
        }

        public async Task<Lesson> Get(int Id_lesson)
        {
            return await _context.Lessons.FindAsync(Id_lesson);
        }

        public async Task<IEnumerable<Lesson>> GetAll()
        {
            return await _context.Lessons.ToListAsync();
        }

        public async Task Update(Lesson lesson)
        {
            var itemToUpdate = await _context.Lessons.FindAsync(lesson.Id_lesson);
            if (itemToUpdate == null)
                throw new NullReferenceException();
            itemToUpdate.Name = lesson.Name;
            itemToUpdate.Rank = lesson.Rank;
            itemToUpdate.Text = lesson.Text;
            await _context.SaveChangesAsync();
        }

        public bool LessonExists(string name, int id)
        {
            return _context.Lessons.Where(x => x.Name == name && x.Id_chapter == id).Any();
        }
    }

}
