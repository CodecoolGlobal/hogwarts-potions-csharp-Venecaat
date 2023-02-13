using System.Threading.Tasks;
using HogwartsPotions.Models.Entities;

namespace HogwartsPotions.Services
{
    public interface IStudentService
    {
        Task<Student> GetStudentById(long id);
    }
}
