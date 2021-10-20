using System.ComponentModel.DataAnnotations;

namespace WestCoast_EducationAPI.ModelViews
{
    public class SubjectViewForPosting
    {
        [MaxLength(50)]
        public string Name { get; set; }
    }
}
