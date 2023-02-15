using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HogwartsPotions.Dto;
using HogwartsPotions.Models;
using HogwartsPotions.Models.Entities;
using Microsoft.EntityFrameworkCore;

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

        public async Task<List<ResponseStudent>> GetAll()
        {
            List<Student> students = await _context.Students.ToListAsync();
            return students.Select(s => new ResponseStudent().MapTo(s)).ToList();
        }
    }
}
