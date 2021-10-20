using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WestCoast_EducationAPI.Entities;

namespace WestCoast_EducationAPI.Context
{
    public class CoursesContext : DbContext
    {
        public DbSet<Course> Courses { get; set; }
        public DbSet<Subject> Subjects { get; set; }
        public DbSet<Teacher> Teachers { get; set; }
        public CoursesContext(DbContextOptions options) : base(options)
        {
        }
    }
}
