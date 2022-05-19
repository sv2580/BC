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
    public class LessonController : ControllerBase
    {
        private readonly ILessonRepository _repository;
        private readonly IChapterRepository _chaptrepository;

        public LessonController(ILessonRepository repository, IChapterRepository chapterRepository)
        {
            _repository = repository;
            _chaptrepository = chapterRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Lesson>>> GetLessons()
        {
            var products = await _repository.GetAll();
            return Ok(products);
        }
        /*
                [HttpGet("{id}")]
                public async Task<ActionResult<Lesson>> GetLessonFromChapter(int Id_lesson)
                {
                    var product = await _repository.Get(Id_lesson);
                    if (product == null)
                        return NotFound();

                    return Ok(product);
                }*/

        [HttpGet("{id}")]
        public async Task<ActionResult<Lesson>> GetLessonFromChapter(int id)
        {
            var lesson = await _repository.GetAll();
            var result = lesson.Where(x => x.Id_chapter == id);
            if (result == null)
                return NotFound();

            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult> CreateLesson(CreateLessonDto createLesson)
        {
            var chapters = await _chaptrepository.GetAll();
            chapters.Where(x => x.Id_chapter == createLesson.Id_chapter);
            if (!chapters.Any())
            {
                return null;
            }
            var lessons = await _repository.GetAll(); 
            if (_repository.LessonExists(createLesson.Name, createLesson.Id_chapter))
                return BadRequest(new { message = "Lekcia s týmto názvom existuje." });
            if (createLesson.Name.Length > 100 || createLesson.Name.Length < 5)
                return BadRequest(new { message = "Názov lecie musí mať medzi 5 až 100 znakmi" });
            if (lessons.Where(x => x.Id_chapter == createLesson.Id_chapter).Count() > 30)
                return BadRequest(new { message = "Nie je možné pridať ďalšiu lekciu" });


            int count = lessons.Where(x => x.Id_chapter == createLesson.Id_chapter).Count();
            Lesson lesson = new()
            {
                Name = createLesson.Name,
                Rank = count + 1,
                Id_chapter = createLesson.Id_chapter,
                Text = createLesson.Text

            };

            await _repository.Add(lesson);
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteLesson(int id)
        {

            var lessons = await _repository.GetAll();

            Lesson thisLesson = lessons.Where(x => x.Id_lesson == id).First();
            List<Lesson> nextLessons = lessons.Where(x => x.Rank > thisLesson.Rank && x.Id_chapter == thisLesson.Id_chapter).ToList();
            int rank = thisLesson.Rank;
            if (nextLessons.Any()) { 
            foreach (Lesson lesson in nextLessons)
            {
                Lesson updatedLesson = new()
                {
                    Id_lesson = lesson.Id_lesson,
                    Name = lesson.Name,
                    Rank = rank,
                    Id_chapter = lesson.Id_chapter,
                    Text = lesson.Text
                };
                await _repository.Update(updatedLesson);

                rank++;
            }}
            await _repository.Delete(id);
            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateLesson(int Id, UpdateLessonDto updateLessonDto)
        {
            var lessons = await _repository.GetAll();
            var existingLesson = lessons.Where(x => x.Id_lesson == Id).First(); 
            if (_repository.LessonExists(updateLessonDto.Name, updateLessonDto.Id_chapter) && updateLessonDto.Name != existingLesson.Name)
                return BadRequest(new { message = "Lekcia s týmto názvom existuje." });
            if (updateLessonDto.Name.Length > 100 || updateLessonDto.Name.Length < 5)
                return BadRequest(new { message = "Názov lecie musí mať medzi 5 až 100 znakmi" });

            Lesson lesson = new()
            {
                Id_lesson = Id,
                Name = updateLessonDto.Name,
                Rank = updateLessonDto.Rank,
                Id_chapter = updateLessonDto.Id_chapter,
                Text = updateLessonDto.Text

            };

            await _repository.Update(lesson);
            return Ok();
        }
    }
}

