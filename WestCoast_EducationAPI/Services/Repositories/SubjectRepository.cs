using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WestCoast_EducationAPI.Context;
using WestCoast_EducationAPI.Entities;

namespace WestCoast_EducationAPI.Services.Repositories
{
    public class SubjectRepository : ISubjectRepository
    {
        private readonly CoursesContext coursesContext;

        public SubjectRepository(CoursesContext coursesContext)
        {
            this.coursesContext = coursesContext;
        }

        public async Task<bool> AddNewSubjectAsync(Subject subject)
        {
            try
            {
                await coursesContext.Subjects.AddAsync(subject);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<Subject> FindSubjectByIdAsync(int id)
        {
            return await coursesContext.Subjects
                .FirstOrDefaultAsync(s => s.Id == id);
        }

        public async Task<IList<Subject>> FindSubjectByNameAsync(string name)
        {
            return await coursesContext.Subjects
                .Where(c => c.Name.Trim().ToLower()
                .Contains(name.Trim().ToLower()))
                .ToListAsync();
        }

        public async Task<IList<Subject>> ListAllSubjectsAsync()
        {
            return await coursesContext.Subjects
                .OrderBy(c => c.Name).ToListAsync();
        }

        public bool RemoveSubject(Subject subject)
        {
            try
            {
                coursesContext.Subjects.Remove(subject);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool UpdateSubject(Subject subject)
        {
            try
            {
                coursesContext.Subjects.Update(subject);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
