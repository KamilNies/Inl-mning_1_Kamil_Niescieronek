using System.Collections.Generic;

namespace WestCoast_EducationAPI.ModelViews
{
    public class TeacherViewToBeReturned
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public ICollection<CourseView> Courses { get; set; }
            = new List<CourseView>();
    }
}
