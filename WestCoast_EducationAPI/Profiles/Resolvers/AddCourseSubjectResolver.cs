using AutoMapper;
using System;
using System.Linq;
using WestCoast_EducationAPI.Context;
using WestCoast_EducationAPI.Entities;
using WestCoast_EducationAPI.ModelViews;

namespace WestCoast_EducationAPI.Profiles
{
    public class AddCourseSubjectResolver : IValueResolver<CourseViewForPosting, Course, Subject>
    {
        public Subject Resolve(CourseViewForPosting source, Course destination, Subject destMember, ResolutionContext context)
        {
            var repo = context.Items["repo"] as CoursesContext;
            var subjectEntity = repo.Subjects.FirstOrDefault(s =>
                s.Name.ToLower().Trim() == source.Subject.ToLower().Trim());

            if (subjectEntity == null)
            {
                throw new Exception($"Subject, {source.Subject}, could not be found.");
            }

            return subjectEntity;
        }
    }
}
