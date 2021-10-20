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

            var course = new CourseViewToBeReturned
            {
                Id = courseEntity.Id,
                Name = courseEntity.Name,
                Price = courseEntity.Price,
                Description = courseEntity.Description,
                Teacher = $"{courseEntity.Teacher.FirstName.Trim()}" +
                $" {courseEntity.Teacher.LastName.Trim()}",
                Subject = courseEntity.Subject.Name
            };

            return Ok(course);
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

            var courses = new List<CourseViewToBeReturned>();

            foreach (var course in courseEntities)
            {
                courses.Add(new CourseViewToBeReturned
                {
                    Id = course.Id,
                    Name = course.Name,
                    Price = course.Price,
                    Description = course.Description,
                    Teacher = $"{course.Teacher.FirstName.Trim()}" +
                    $" {course.Teacher.LastName.Trim()}",
                    Subject = course.Subject.Name
                });
            }

            return Ok(courses);
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

            var courses = new List<CourseViewToBeReturned>();

            foreach (var course in courseEntities)
            {
                courses.Add(new CourseViewToBeReturned
                {
                    Id = course.Id,
                    Name = course.Name,
                    Price = course.Price,
                    Description = course.Description,
                    Teacher = $"{course.Teacher.FirstName.Trim()}" +
                    $" {course.Teacher.LastName.Trim()}",
                    Subject = course.Subject.Name
                });
            }

            return Ok(courses);
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

            var courses = new List<CourseViewToBeReturned>();

            foreach (var course in courseEntities)
            {
                courses.Add(new CourseViewToBeReturned
                {
                    Id = course.Id,
                    Name = course.Name,
                    Price = course.Price,
                    Description = course.Description,
                    Teacher = $"{course.Teacher.FirstName.Trim()}" +
                    $" {course.Teacher.LastName.Trim()}",
                    Subject = course.Subject.Name
                });
            }

            return Ok(courses);
        }

        [HttpPost]
        public async Task<IActionResult> AddCourse(CourseViewForPosting model)
        {
            
            try
            {
                var courseEntity = mapper.Map<Course>(model);

                if (await unitOfWork.CourseRepository.AddNewCourseAsync(courseEntity))
                {
                    if (!await unitOfWork.CompleteAsync())
                    {
                        return StatusCode(500, "Course could not be added.");
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
            [FromBody] CourseViewForUpdate course)
        {
            var courseEntity = await unitOfWork.CourseRepository.FindCourseByIdAsync(id);

            if (courseEntity == null)
            {
                return NotFound($"Course with identity {id} could not be found.");
            }

            mapper.Map(courseEntity, course);

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
