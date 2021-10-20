using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WestCoast_EducationAPI.Context;
using WestCoast_EducationAPI.Entities;

namespace WestCoast_EducationAPI.Services.Repositories
{
    public class CourseRepository : ICourseRepository
    {
        private readonly CoursesContext coursesContext;

        public CourseRepository(CoursesContext coursesContext)
        {
            this.coursesContext = coursesContext;
        }

        public async Task<bool> AddNewCourseAsync(Course course)
        {
            try
            {
                await coursesContext.Courses.AddAsync(course);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<Course> FindCourseByIdAsync(int id)
        {
            return await coursesContext.Courses
                .Include(c => c.Teacher)
                .Include(c => c.Subject)
                .FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<IList<Course>> FindCoursesByNameAsync(string name)
        {
            return await coursesContext.Courses
                .Include(c => c.Teacher)
                .Include(c => c.Subject)
                .Where(c => c.Name.Trim().ToLower().Contains(name.Trim().ToLower()))
                .ToListAsync();
        }

        public async Task<IList<Course>> FindCoursesBelowPricePointAsync(decimal price)
        {
            return await coursesContext.Courses
                .Include(c => c.Teacher)
                .Include(c => c.Subject)
                .Where(c => c.Price <= price).ToListAsync();
        }

        public async Task<IList<Course>> FindCoursesBySubjectName(string subject)
        {
            return await coursesContext.Courses
                .Include(c => c.Teacher)
                .Include(c => c.Subject)
                .Where(c => c.Subject.Name.Trim().ToLower()
                .Contains(subject.Trim().ToLower()))
                .ToListAsync();
        }

        public async Task<IList<Course>> ListAllCoursesAsync()
        {
            return await coursesContext.Courses
                .Include(c => c.Teacher)
                .Include(c => c.Subject).ToListAsync();
        }

        public bool RemoveCourse(Course course)
        {
            try
            {
                coursesContext.Courses.Remove(course);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool UpdateCourse(Course course)
        {
            try
            {
                coursesContext.Courses.Update(course);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
