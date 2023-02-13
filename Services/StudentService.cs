using System.Threading.Tasks;
using HogwartsPotions.Models;
using HogwartsPotions.Models.Entities;

namespace HogwartsPotions.Services
{
    public class StudentService : IStudentService
    {
        private readonly HogwartsContext _context;

        public StudentService(HogwartsContext context)
        {
            _context = context;
        }

        public async Task<Student> GetStudentById(long id) => await _context.Students.FindAsync(id);
    }
}
