using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WestCoast_EducationAPI.Entities;
using WestCoast_EducationAPI.ModelViews;
using WestCoast_EducationAPI.Services.Interfaces;

namespace WestCoast_EducationAPI.Controllers
{
    [ApiController]
    [Route("api/course")]
    public class CourseController : ControllerBase
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public CourseController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetCourses()
        {
            var courseEntities = await unitOfWork.CourseRepository.ListAllCoursesAsync();
            return Ok(mapper.Map<List<CourseViewToBeReturned>>(courseEntities));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCourse(int id)
        {
            var courseEntity = await unitOfWork.CourseRepository.FindCourseByIdAsync(id);
            
            if (courseEntity == null)
            {
                return NotFound($"Course with identity {id} could not be found.");
            }

            return Ok(mapper.Map<CourseViewToBeReturned>(courseEntity));
        }

        [HttpGet("courseName/{name}")]
        public async Task<IActionResult> GetCoursesByName(string name)
        {
            var courseEntities = await unitOfWork.CourseRepository
                .FindCoursesByNameAsync(name);

            if (courseEntities == null)
            {
                return NotFound($"No courses found containing: {name.Trim()}.");
            }

            return Ok(mapper.Map<IEnumerable<CourseViewToBeReturned>>(courseEntities));
        }

        [HttpGet("courseSubject/{subject}")]
        public async Task<IActionResult> GetCoursesBySubject(string subject)
        {
            var courseEntities = await unitOfWork.CourseRepository
                .FindCoursesBySubjectName(subject);

            if (courseEntities == null)
            {
                return NotFound($"No courses found with subject matter: {subject.Trim()}.");
            }

            return Ok(mapper.Map<IEnumerable<CourseViewToBeReturned>>(courseEntities));
        }

        [HttpGet("coursePrice/{price}")]
        public async Task<IActionResult> GetCoursesBelowPricePoint(decimal price)
        {
            var courseEntities = await unitOfWork.CourseRepository
               .FindCoursesBelowPricePointAsync(price);

            if (courseEntities == null)
            {
                return NotFound($"No courses found below the {price} price point.");
            }

            return Ok(mapper.Map<IEnumerable<CourseViewToBeReturned>>(courseEntities));
        }

        [HttpPost]
        public async Task<IActionResult> AddCourse(CourseViewForPosting model)
        {
            try
            {
                var courseEntity = mapper.Map<Course>(model, opt => opt.Items["repo"] = unitOfWork.CoursesContext);

                if (await unitOfWork.CourseRepository.AddNewCourseAsync(courseEntity))
                {
                    if (!await unitOfWork.CompleteAsync())
                    {
                        return StatusCode(500, "Course could not be saved.");
                    }
                }

                var course = mapper.Map<CourseViewToBeReturned>(courseEntity);
                return StatusCode(201, course);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.InnerException.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCourse(int id, 
            [FromBody] CourseViewForUpdate model)
        {
            var courseEntity = await unitOfWork.CourseRepository.FindCourseByIdAsync(id);

            if (courseEntity == null)
            {
                return NotFound($"Course with identity {id} could not be found.");
            }

            mapper.Map(model, courseEntity);

            if (unitOfWork.CourseRepository.UpdateCourse(courseEntity))
            {
                if (await unitOfWork.CompleteAsync())
                {
                    return NoContent();
                }
            }

            return StatusCode(500, "Course could not be updated.");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> RemoveCourse(int id)
        {
            var courseEntity = await unitOfWork.CourseRepository.FindCourseByIdAsync(id);

            if (courseEntity == null)
            {
                return NotFound($"Course with identity {id} could not be found.");
            }

            if (unitOfWork.CourseRepository.RemoveCourse(courseEntity))
            {
                if (await unitOfWork.CompleteAsync())
                {
                    return NoContent();
                }
            }

            return StatusCode(500, "Course could not be deleted.");
        }
    }
}
