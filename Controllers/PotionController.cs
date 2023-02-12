using System.Collections.Generic;
using System.Threading.Tasks;
using ElProyecteGrande.Interfaces.Services;
using HogwartsPotions.Models.Entities;
using Microsoft.AspNetCore.Mvc;

namespace HogwartsPotions.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PotionController : Controller
    {
        private readonly IPotionService _service;

        public PotionController(IPotionService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<List<Potion>>> GetAllPotion()
        {
            return await _service.GetAll();
        }
    }
}
