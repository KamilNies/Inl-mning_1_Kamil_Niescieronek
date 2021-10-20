using System.Threading.Tasks;
using WestCoast_EducationAPI.Context;
using WestCoast_EducationAPI.Services.Interfaces;
using WestCoast_EducationAPI.Services.Repositories;

namespace WestCoast_EducationAPI.Services
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly CoursesContext coursesContext;

        public UnitOfWork(CoursesContext coursesContext)
        {
            this.coursesContext = coursesContext;
        }

        public ICourseRepository CourseRepository => new CourseRepository(coursesContext);

        public ISubjectRepository SubjectRepository => new SubjectRepository(coursesContext);

        public ITeacherRepository TeacherRepository => new TeacherRepository(coursesContext);

        public CoursesContext CoursesContext => coursesContext;

        public async Task<bool> CompleteAsync()
        {
            return await coursesContext.SaveChangesAsync() > 0;
        }

        public bool HasChanges()
        {
            return coursesContext.ChangeTracker.HasChanges();
        }
    }
}
