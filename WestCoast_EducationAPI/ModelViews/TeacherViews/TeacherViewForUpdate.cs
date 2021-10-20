using System.Collections.Generic;

namespace WestCoast_EducationAPI.ModelViews
{
    public class TeacherViewForUpdate
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public ICollection<CourseViewForUpdate> Courses { get; set; }
            = new List<CourseViewForUpdate>();
    }
}
