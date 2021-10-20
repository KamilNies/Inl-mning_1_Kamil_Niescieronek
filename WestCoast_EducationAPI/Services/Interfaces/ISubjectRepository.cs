using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WestCoast_EducationAPI.Entities;

namespace WestCoast_EducationAPI.Services
{
    public interface ISubjectRepository
    {
        Task<bool> AddNewSubjectAsync(Subject subject);
        bool UpdateSubject(Subject subject);
        bool RemoveSubject(Subject subject);
        Task<IList<Subject>> ListAllSubjectsAsync();
        Task<Subject> FindSubjectByIdAsync(int id);
        Task<IList<Subject>> FindSubjectByNameAsync(string name);
    }
}
