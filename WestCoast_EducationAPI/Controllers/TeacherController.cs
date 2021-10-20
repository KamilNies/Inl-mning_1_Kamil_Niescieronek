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
    [Route("api/teacher")]
    public class TeacherController : ControllerBase
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public TeacherController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetTeachers()
        {
            var teacherEntities = await unitOfWork.TeacherRepository.ListAllTeachersAsync();
            return Ok(mapper
                .Map<IEnumerable<TeacherViewToBeReturnedWithoutCourses>>(teacherEntities));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetTeacher(int id, bool includeCourses = false)
        {
            var teacherEntity = await unitOfWork.TeacherRepository
                .FindTeacherByIdAsync(id, includeCourses);

            if (teacherEntity == null)
            {
                return NotFound($"Teacher with identity {id} could not be found.");
            }

            if (includeCourses == true)
            {
                return Ok(mapper.Map<TeacherViewToBeReturned>(teacherEntity));
            }

            return Ok(mapper
                .Map<TeacherViewToBeReturnedWithoutCourses>(teacherEntity));
        }

        [HttpGet("firstName/{name}")]
        public async Task<IActionResult> GetTeachersByFirstName(string name)
        {
            var teacherEntities = await unitOfWork.TeacherRepository
                .FindTeachersByFirstNameAsync(name);

            if (teacherEntities == null)
            {
                return NotFound($"No teachers with first name, {name.Trim()}, found.");
            }

            return Ok(mapper
                .Map<IEnumerable<TeacherViewToBeReturnedWithoutCourses>>(teacherEntities));
        }

        [HttpGet("lastName/{name}")]
        public async Task<IActionResult> GetTeachersByLastName(string name)
        {
            var teacherEntities = await unitOfWork.TeacherRepository
                .FindTeachersByLastNameAsync(name);

            if (teacherEntities == null)
            {
                return NotFound($"No teachers with last name, {name.Trim()}, found.");
            }

            return Ok(mapper
                .Map<IEnumerable<TeacherViewToBeReturnedWithoutCourses>>(teacherEntities));
        }

        [HttpGet("email/{email}")]
        public async Task<IActionResult> GetTeachersByPartialEmailSearch(string email)
        {
            var teacherEntities = await unitOfWork.TeacherRepository
                .FindTeachersByPartialEmailSearchAsync(email);

            if (teacherEntities == null)
            {
                return NotFound($"No teachers found matching email query: {email.Trim()}");
            }

            return Ok(mapper
                .Map<IEnumerable<TeacherViewToBeReturnedWithoutCourses>>(teacherEntities));
        }

        [HttpPost]
        public async Task<IActionResult> AddTeacher(TeacherViewForPosting model)
        {
            try
            {
                var teacherEntity = mapper.Map<Teacher>(model);

                if (await unitOfWork.TeacherRepository.AddNewTeacherAsync(teacherEntity))
                {
                    if (!await unitOfWork.CompleteAsync())
                    {
                        return StatusCode(500, "Teacher could not be saved.");
                    }
                }

                var teacher = mapper
                    .Map<TeacherViewToBeReturnedWithoutCourses>(teacherEntity);
                return StatusCode(201, teacher);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.InnerException.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTeacher(int id,
            [FromBody] TeacherViewForUpdate model)
        {
            var teacherEntity = await unitOfWork.TeacherRepository
                .FindTeacherByIdAsync(id, false);

            if (teacherEntity == null)
            {
                return NotFound($"Teacher with identity {id} could not be found.");
            }

            mapper.Map(model, teacherEntity);

            if (unitOfWork.TeacherRepository.UpdateTeacher(teacherEntity))
            {
                if (await unitOfWork.CompleteAsync())
                {
                    return NoContent();
                }
            }

            return StatusCode(500, "Subject could not be updated.");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> RemoveTeacher(int id)
        {
            var teacherEntity = await unitOfWork.TeacherRepository
                .FindTeacherByIdAsync(id, false);

            if (teacherEntity == null)
            {
                return NotFound($"Teacher with identity {id} could not be found.");
            }

            if (unitOfWork.TeacherRepository.RemoveTeacher(teacherEntity))
            {
                if (await unitOfWork.CompleteAsync())
                {
                    return NoContent();
                }
            }
            
            return StatusCode(500, "Teacher could not be removed");
        }
    }
}
