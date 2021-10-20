using AutoMapper;

namespace WestCoast_EducationAPI.Profiles
{
    public class CourseProfile : Profile
    {
        public CourseProfile()
        {
            CreateMap<Entities.Course, ModelViews.CourseViewToBeReturned>()
                .ForMember(dest => dest.TeacherFirstName, opt => opt.MapFrom(src => src.Teacher.FirstName))
                .ForMember(dest => dest.TeacherLastName, opt => opt.MapFrom(src => src.Teacher.LastName))
                .ForMember(dest => dest.Subject, opt => opt.MapFrom(src => src.Subject.Name));

            CreateMap<ModelViews.CourseViewForPosting, Entities.Course>()
                .ForMember(dest => dest.Teacher, opt => opt.MapFrom<AddCourseTeacherResolver>())
                .ForMember(dest => dest.Subject, opt => opt.MapFrom<AddCourseSubjectResolver>());

            CreateMap<ModelViews.CourseViewForUpdate, Entities.Course>();
            CreateMap<Entities.Course, ModelViews.CourseView>();
        }
    }
}
