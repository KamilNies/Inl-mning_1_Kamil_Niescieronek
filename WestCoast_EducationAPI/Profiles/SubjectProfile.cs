using AutoMapper;

namespace WestCoast_EducationAPI.Profiles
{
    public class SubjectProfile : Profile
    {
        public SubjectProfile()
        {
            CreateMap<Entities.Subject, ModelViews.SubjectViewToBeReturned>();
            CreateMap<ModelViews.SubjectViewForUpdate, Entities.Subject>();
            CreateMap<ModelViews.SubjectViewForPosting, Entities.Subject>();
        }
    }
}
