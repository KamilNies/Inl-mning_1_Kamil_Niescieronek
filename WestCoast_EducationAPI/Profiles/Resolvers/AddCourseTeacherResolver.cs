using AutoMapper;
using System;
using System.Linq;
using WestCoast_EducationAPI.Context;
using WestCoast_EducationAPI.Entities;
using WestCoast_EducationAPI.ModelViews;

namespace WestCoast_EducationAPI.Profiles
{
    public class AddCourseTeacherResolver : IValueResolver<CourseViewForPosting, Course, Teacher>
    {
        public Teacher Resolve(CourseViewForPosting source, Course destination, Teacher destMember, ResolutionContext context)
        {
            var repo = context.Items["repo"] as CoursesContext;
            var teacherEntity = repo.Teachers.FirstOrDefault(t => 
                t.FirstName.ToLower().Trim() == source.TeacherFirstName.ToLower().Trim() &&
                t.LastName.ToLower().Trim() == source.TeacherLastName.ToLower().Trim());

            if (teacherEntity == null)
            {
                throw new Exception($"Teacher, {source.TeacherFirstName} {source.TeacherLastName}, could not be found.");
            }

            return teacherEntity;
        }
    }
}
