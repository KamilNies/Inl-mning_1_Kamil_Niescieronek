using System.ComponentModel.DataAnnotations;

namespace WestCoast_EducationAPI.ModelViews
{
    public class TeacherViewForPosting
    {
        [MaxLength(50)]
        public string FirstName { get; set; }
        [MaxLength(50)]
        public string LastName { get; set; }
        [MaxLength(100)]
        public string Email { get; set; }
    }
}
