using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WestCoast_EducationAPI.ModelViews
{
    public class TeacherViewToBeReturnedWithoutCourses
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
    }
}
