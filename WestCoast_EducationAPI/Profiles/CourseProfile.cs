using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WestCoast_EducationAPI.Profiles
{
    public class CourseProfile : Profile
    {
        public CourseProfile()
        {
            CreateMap<Entities.Course, ModelViews.CourseViewToBeReturned>()
              .ForMember(dest => dest.Teacher, opt => opt
              .MapFrom(src => src.Teacher.FirstName + " " + src.Teacher.LastName))
              .ForMember(dest => dest.Subject, opt => opt.MapFrom(src => src.Subject.Name));
        }
    }
}
