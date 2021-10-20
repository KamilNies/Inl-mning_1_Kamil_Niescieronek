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
    [Route("api/subject")]
    public class SubjectController : ControllerBase
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public SubjectController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetSubjects()
        {
            var subjectEntities = await unitOfWork.SubjectRepository.ListAllSubjectsAsync();
            return Ok(mapper.Map<IEnumerable<SubjectViewToBeReturned>>(subjectEntities));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetSubject(int id)
        {
            var subjectEntity = await unitOfWork.SubjectRepository.FindSubjectByIdAsync(id);

            if (subjectEntity == null)
            {
                return NotFound($"Subject with identity {id} could not be found.");
            }

            return Ok(mapper.Map<SubjectViewToBeReturned>(subjectEntity));
        }

        [HttpGet("subject/{name}")]
        public async Task<IActionResult> GetSubjectsByName(string name)
        {
            var subjectEntities = await unitOfWork.SubjectRepository
                .FindSubjectByNameAsync(name);

            if (subjectEntities == null)
            {
                return NotFound($"No subjects found containing: {name.Trim()}.");
            }

            return Ok(mapper.Map<IEnumerable<SubjectViewToBeReturned>>(subjectEntities));
        }

        [HttpPost]
        public async Task<IActionResult> AddSubject(SubjectViewForPosting model)
        {
            try
            {
                var subjectEntity = mapper.Map<Subject>(model);

                if (await unitOfWork.SubjectRepository.AddNewSubjectAsync(subjectEntity))
                {
                    if (!await unitOfWork.CompleteAsync())
                    {
                        return StatusCode(500, "Subject could not be added.");
                    }
                }

                var subject = mapper.Map<SubjectViewToBeReturned>(subjectEntity);
                return StatusCode(201, subject);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.InnerException.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateSubject(int id, 
            [FromBody] SubjectViewForUpdate model)
        {
            var subjectEntity = await unitOfWork.SubjectRepository.FindSubjectByIdAsync(id);

            if (subjectEntity == null)
            {
                return NotFound($"Subject with identity {id} could not be found.");
            }

            mapper.Map(model, subjectEntity);

            if (unitOfWork.SubjectRepository.UpdateSubject(subjectEntity))
            {
                if (await unitOfWork.CompleteAsync())
                {
                    return NoContent();
                }
            }

            return StatusCode(500, "Subject could not be updated.");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> RemoveSubject(int id)
        {
            var subjectEntity = await unitOfWork.SubjectRepository.FindSubjectByIdAsync(id);

            if (subjectEntity == null)
            {
                return NotFound($"Subject with identity {id} could not be found.");
            }

            if (unitOfWork.SubjectRepository.RemoveSubject(subjectEntity))
            {
                if (await unitOfWork.CompleteAsync())
                {
                    return NoContent();
                }
            }

            return StatusCode(500, "Subject could not be deleted.");
        }
    }
}
