using AutoMapper;

namespace WestCoast_EducationAPI.Profiles
{
    public class TeacherProfile : Profile
    {
        public TeacherProfile()
        {
            CreateMap<Entities.Teacher, ModelViews.TeacherViewToBeReturned>();
            CreateMap<Entities.Teacher, ModelViews.TeacherViewToBeReturnedWithoutCourses>();
            CreateMap<ModelViews.TeacherViewForPosting, Entities.Teacher>();
            /*Teacher -> TeacherViewToBeReturned
            WestCoast_EducationAPI.Entities.Teacher -> WestCoast_EducationAPI.ModelViews.TeacherViewToBeReturned*/
        }
    }
}
