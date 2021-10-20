using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WestCoast_EducationAPI.Entities;

namespace WestCoast_EducationAPI.Services
{
    public interface ITeacherRepository
    {
        Task<bool> AddNewTeacherAsync(Teacher teacher);
        bool UpdateTeacher(Teacher teacher);
        bool RemoveTeacher(Teacher teacher);
        Task<IList<Teacher>> ListAllTeachersAsync();
        Task<Teacher> FindTeacherByIdAsync(int id, bool includeCourseNames);
        Task<IList<Teacher>> FindTeachersByFirstNameAsync(string firstName);
        Task<IList<Teacher>> FindTeachersByLastNameAsync(string lastName);
        Task<IList<Teacher>> FindTeachersByPartialEmailSearchAsync(string email);
    }
}
