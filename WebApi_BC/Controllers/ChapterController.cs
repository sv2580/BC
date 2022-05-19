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
    public class ChapterController : ControllerBase
    {
        private readonly IChapterRepository _repository;
        private readonly ICoursesRepository _course_repository;

        public ChapterController(IChapterRepository repository, ICoursesRepository coursesRepository)
        {
            _repository = repository;
            _course_repository = coursesRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Chapter>>> GetChapters()
        {
            var products = await _repository.GetAll();
            return Ok(products);
        }


        [HttpGet("{id_course}")]
        public async Task<ActionResult<IEnumerable<Chapter>>> GetChapters(int id_course)
        {
            var chapters = await _repository.GetAll();
            var result = chapters.Where(x => x.Id_course == id_course);
            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult> CreateChapter(CreateChapterDto createChapter)
        {
            var courses = await _course_repository.GetAll();
            courses.Where(x => x.Id == createChapter.Id_course);
            if (!courses.Any())
            {
                return BadRequest();
            }

            if (_repository.ChapterExists(createChapter.Name, createChapter.Id_course))
                return BadRequest(new { message = "Kapitola s týmto názvom existuje." });
            if (createChapter.Name.Length > 100 || createChapter.Name.Length < 5)
                return BadRequest(new { message = "Názov kapitoly musí mať medzi 5 až 100 znakmi" });
            var chapters = await _repository.GetAll();
            if (chapters.Where(x => x.Id_course == createChapter.Id_course).Count() >= 30)
                return BadRequest(new { message = "Nie je možné pridať ďalšiu kapitolu" });

            int count = chapters.Where(x => x.Id_course == createChapter.Id_course).Count();

            Chapter chapter = new()
            {
                Name = createChapter.Name,
                Rank = count + 1,
                Id_course = createChapter.Id_course

            };

            await _repository.Add(chapter);
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteChapter(int id)
        {
            var chapters = await _repository.GetAll();
            Chapter thisChapter = chapters.Where(x => x.Id_chapter == id).First();
            List<Chapter> nextChapters = chapters.Where(x => x.Rank > thisChapter.Rank && x.Id_course == thisChapter.Id_course).ToList();
            int rank = thisChapter.Rank;
            
            if (nextChapters.Any())
            {
                foreach (Chapter chapter in nextChapters)
                {
                    Chapter updatedChapter = new()
                    {
                        Id_chapter = chapter.Id_chapter,
                        Name = chapter.Name,
                        Rank = rank,
                        Id_course = chapter.Id_course
                    };
                    await _repository.Update(updatedChapter);
                    rank++;

                }
            }

            await _repository.Delete(id);
            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateChapter(int Id, UpdateChapterDto updateChapterDto)
        {

            var chapters = await _repository.GetAll();
            var existingLesson = chapters.Where(x => x.Id_chapter == Id).First();
            if (_repository.ChapterExists(updateChapterDto.Name, updateChapterDto.Id_course) && updateChapterDto.Name != existingLesson.Name)
                return BadRequest(new { message = "Kapitola s týmto názvom existuje." });
            if (updateChapterDto.Name.Length > 100 || updateChapterDto.Name.Length < 5)
                return BadRequest(new { message = "Názov musí mať medzi 5 až 100 znakmi" });

            Chapter chapter = new()
            {
                Id_chapter = Id,
                Name = updateChapterDto.Name,
                Rank = updateChapterDto.Rank,
                Id_course = updateChapterDto.Id_course
            };

            await _repository.Update(chapter);
            return Ok();
        }
    }


}
