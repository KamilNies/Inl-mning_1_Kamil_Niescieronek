using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WestCoast_EducationAPI.Entities;

namespace WestCoast_EducationAPI.ModelViews
{
    public class TeacherViewToBeReturned : TeacherViewToBeReturnedWithoutCourses
    {
        public ICollection<Course> Courses { get; set; }
            = new List<Course>();
    }
}
