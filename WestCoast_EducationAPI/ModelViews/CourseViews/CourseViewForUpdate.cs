using System.ComponentModel.DataAnnotations;

namespace WestCoast_EducationAPI.ModelViews
{
    public class CourseViewForUpdate
    {
        [Required]
        [MaxLength(150)]
        public string Name { get; set; }
        public decimal? Price { get; set; }
        [MaxLength(2500)]
        public string Description { get; set; }
    }
}
