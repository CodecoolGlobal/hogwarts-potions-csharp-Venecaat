using HogwartsPotions.Dto;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using HogwartsPotions.Services;

namespace HogwartsPotions.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StudentController : Controller
    {
        private readonly IStudentService _service;

        public StudentController(IStudentService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<List<ResponseStudent>>> GetAllStudent()
        {
            return await _service.GetAll();
        }
    }
}
