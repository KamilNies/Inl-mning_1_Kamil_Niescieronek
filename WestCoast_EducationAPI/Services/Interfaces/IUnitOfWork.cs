using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WestCoast_EducationAPI.Context;

namespace WestCoast_EducationAPI.Services.Interfaces
{
    public interface IUnitOfWork
    {
        ICourseRepository CourseRepository { get; }
        ISubjectRepository SubjectRepository { get; }
        ITeacherRepository TeacherRepository { get; }
        CoursesContext CoursesContext { get; }
        Task<bool> CompleteAsync();
        bool HasChanges();
    }
}
