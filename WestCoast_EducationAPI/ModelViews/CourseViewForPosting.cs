using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WestCoast_EducationAPI.ModelViews
{
    public class CourseViewForPosting
    {
        [Required]
        [MaxLength(150)]
        public string Name { get; set; }
        public decimal? Price { get; set; }
        [MaxLength(2500)]
        public string Description { get; set; }
        [MaxLength(50)]
        public string TeacherFirstName { get; set; }
        [MaxLength(50)]
        public string TeacherLastName { get; set; }
        [MaxLength(50)]
        public string Subject { get; set; }
    }
}
