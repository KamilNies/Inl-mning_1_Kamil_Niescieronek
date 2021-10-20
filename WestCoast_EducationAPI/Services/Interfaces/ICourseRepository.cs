using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WestCoast_EducationAPI.Entities;

namespace WestCoast_EducationAPI.Services
{
    public interface ICourseRepository
    {
        Task<bool> AddNewCourseAsync(Course course);
        bool UpdateCourse(Course course);
        bool RemoveCourse(Course course);
        Task<IList<Course>> ListAllCoursesAsync();
        Task<Course> FindCourseByIdAsync(int id);
        Task<IList<Course>> FindCoursesByNameAsync(string name);
        Task<IList<Course>> FindCoursesBySubjectName(string subject);
        Task<IList<Course>> FindCoursesBelowPricePointAsync(decimal price);
    }
}
