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
    public class CourseController : ControllerBase
    {
        private readonly ICoursesRepository _repository;
        public CourseController(ICoursesRepository productRepository)
        {
            _repository = productRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Course>>> GetCourses()
        {
            var products = await _repository.GetAll();
            return Ok(products);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Course>> GetCourse(int id)
        {
            var product = await _repository.Get(id);
            if (product == null)
                return NotFound();

            return Ok(product);
        }

        [HttpPost]
        public async Task<ActionResult> CreateCourse(CreateCourseDto createCourse)
        {
            Course course = new()
            {
                Name = createCourse.Name,
                Subject = createCourse.Subject,
                Visibility = false,
                Description = "",
            };
            if(_repository.CourseExists(createCourse.Name) != -1)
                return BadRequest(new { message = "Kurz s týmto názvom existuje." });
            if (createCourse.Name.Length > 100 || createCourse.Name.Length < 5)
                return BadRequest(new { message = "Názov kurzu musí mať medzi 5 až 100 znakmi" });
            await _repository.Add(course);
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteCourse(int id)
        {
            await _repository.Delete(id);
            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateCourse(int id, UpdateCourseDto updateCourseDto)
        {
            Course course = new()
            {
                Id = id,
                Name = updateCourseDto.Name,
                Subject = updateCourseDto.Subject,
                Visibility = updateCourseDto.Visibility,
                Description = updateCourseDto.Description,

            };

            if (_repository.CourseExists(updateCourseDto.Name) != -1 && _repository.CourseExists(updateCourseDto.Name) != id )
                return BadRequest(new { message = "Kurz s týmto názvom existuje." });
            if (updateCourseDto.Name.Length > 100 || updateCourseDto.Name.Length < 5)
                return BadRequest(new { message = "Názov kurzu musí mať medzi 5 až 100 znakmi" });
            if (updateCourseDto.Description.Length > 256)
                return BadRequest(new { message = "Popis kurzu musí mať do 256 znakov" });

            await _repository.Update(course);

            return Ok();
        }
    }
}
       