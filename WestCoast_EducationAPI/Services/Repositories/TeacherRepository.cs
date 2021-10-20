using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WestCoast_EducationAPI.Context;
using WestCoast_EducationAPI.Entities;

namespace WestCoast_EducationAPI.Services.Repositories
{
    public class TeacherRepository : ITeacherRepository
    {
        private readonly CoursesContext coursesContext;

        public TeacherRepository(CoursesContext coursesContext)
        {
            this.coursesContext = coursesContext;
        }

        public async Task<bool> AddNewTeacherAsync(Teacher teacher)
        {
            try
            {
                await coursesContext.Teachers.AddAsync(teacher);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<IList<Teacher>> FindTeachersByPartialEmailSearchAsync(string email)
        {
            return await coursesContext.Teachers
                .Where(c => c.Email.Trim().ToLower().Contains(email.Trim().ToLower()))
                .ToListAsync();
        }

        public async Task<Teacher> FindTeacherByIdAsync(int id, bool includeCourseNames)
        {
            if (includeCourseNames)
            {
                return await coursesContext.Teachers
                    .Include(t => t.Courses)
                    .FirstOrDefaultAsync(t => t.Id == id);
            }

            return await coursesContext.Teachers
                .FirstOrDefaultAsync(t => t.Id == id);
        }

        public async Task<IList<Teacher>> FindTeachersByFirstNameAsync(string firstName)
        {
            return await coursesContext.Teachers
                .Where(c => c.FirstName.Trim().ToLower()
                .Contains(firstName.Trim().ToLower()))
                .ToListAsync();
        }

        public async Task<IList<Teacher>> FindTeachersByLastNameAsync(string lastName)
        {
            return await coursesContext.Teachers
                .Where(c => c.LastName.Trim().ToLower()
                .Contains(lastName.Trim().ToLower()))
                .ToListAsync();
        }

        public async Task<IList<Teacher>> ListAllTeachersAsync()
        {
            return await coursesContext.Teachers.ToListAsync();
        }

        public bool RemoveTeacher(Teacher teacher)
        {
            try
            {
                coursesContext.Teachers.Remove(teacher);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool UpdateTeacher(Teacher teacher)
        {
            try
            {
                coursesContext.Teachers.Update(teacher);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
