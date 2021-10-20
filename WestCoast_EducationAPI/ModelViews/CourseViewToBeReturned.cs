using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WestCoast_EducationAPI.Entities;

namespace WestCoast_EducationAPI.ModelViews
{
    public class CourseViewToBeReturned
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal? Price { get; set; }
        public string Description { get; set; }
        public string Teacher { get; set; }
        public string Subject { get; set; }
    }
}
