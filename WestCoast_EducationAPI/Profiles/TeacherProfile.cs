using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WestCoast_EducationAPI.Profiles
{
    public class TeacherProfile : Profile
    {
        public TeacherProfile()
        {
            CreateMap<Entities.Teacher, ModelViews.TeacherViewToBeReturned>();
            CreateMap<Entities.Teacher, ModelViews.TeacherViewToBeReturnedWithoutCourses>();
        }
    }
}
