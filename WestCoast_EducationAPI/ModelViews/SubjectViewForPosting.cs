using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WestCoast_EducationAPI.ModelViews
{
    public class SubjectViewForPosting
    {
        [MaxLength(50)]
        public string Name { get; set; }
    }
}
