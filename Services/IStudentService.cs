using System.Collections.Generic;
using System.Threading.Tasks;
using HogwartsPotions.Dto;
using HogwartsPotions.Models.Entities;

namespace HogwartsPotions.Services
{
    public interface IStudentService
    {
        Task<Student> GetStudentById(long id);
        Task<List<ResponseStudent>> GetAll();
    }
}
