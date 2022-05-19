using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Models;

namespace WebApi.Repositories
{
    public interface IChapterRepository
    {
           
        Task<Chapter> Get(int id);
        Task<IEnumerable<Chapter>> GetAll();
        Task Add(Chapter chapter);
        Task Delete(int id);
        Task Update(Chapter chapter);
        bool ChapterExists(string name, int id);
    }

    public class ChapterRepository : IChapterRepository
    {
        private readonly IDataContext _context;
        public ChapterRepository(IDataContext context)
        {
            _context = context;

        }
        public async Task Add(Chapter chapter)
        {
            _context.Chapters.Add(chapter);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(int Id_chapter)
        {
            var itemToRemove = await _context.Chapters.FindAsync(Id_chapter);
            if (itemToRemove == null)
                throw new NullReferenceException();

            var lesson = _context.Lessons.Where(x => x.Id_chapter == Id_chapter).ToList();
            if (lesson.Any() && lesson != null)
            {
                foreach (var l in lesson)
                {
                 
                    _context.Lessons.Remove(l);
                }
            }

            _context.Chapters.Remove(itemToRemove);
            await _context.SaveChangesAsync();
        }

        public async Task<Chapter> Get(int Id_chapter)
        {
            return await _context.Chapters.FindAsync(Id_chapter);
        }

        public async Task<IEnumerable<Chapter>> GetAll()
        {
            return await _context.Chapters.ToListAsync();
        }

        public async Task Update(Chapter chapter)
        {
            var itemToUpdate = await _context.Chapters.FindAsync(chapter.Id_chapter);
            if (itemToUpdate == null)
                throw new NullReferenceException();
            itemToUpdate.Name = chapter.Name;
            itemToUpdate.Rank = chapter.Rank;
            await _context.SaveChangesAsync();
        }

        public bool ChapterExists(string name, int id)
        {
            return _context.Chapters.Where(x => x.Name == name && x.Id_course == id).Any();
        }
    }
}
