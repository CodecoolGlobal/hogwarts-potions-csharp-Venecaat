using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ElProyecteGrande.Interfaces.Services;
using HogwartsPotions.Dto;
using HogwartsPotions.Models.Entities;
using Microsoft.AspNetCore.Http;
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
        public async Task<ActionResult<List<ResponsePotion>>> GetAllPotion()
        {
            return await _service.GetAll();
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<ActionResult<ResponsePotion>> AddNewPotion(NewPotion newPotion)
        {
            ResponsePotion potion = await _service.Add(newPotion);
            return StatusCode(StatusCodes.Status201Created, potion);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<List<PotionWithIdAndName>>> GetAStudentsPotions(long id)
        {
            return await _service.GetPotionsByStudentId(id);
        }

        [HttpPost("brew")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<ActionResult<ResponsePotion>> AddNewBrewingPotion([FromBody]long studentId)
        {
            ResponseBrewingPotion brewingPotion = await _service.AddBrewingPotion(studentId);
            return StatusCode(StatusCodes.Status201Created, brewingPotion);
        }

        [HttpPut("{potionId}/add")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<ActionResult<ResponseBrewingPotion>> UpdateBrewingPotion(long potionId, [FromBody]IngredientWithName ingredient)
        {
            ResponseBrewingPotion brewingPotion = await _service.AddIngredientToBrewingPotion(potionId, ingredient);

            return StatusCode(StatusCodes.Status201Created, brewingPotion);
        }

        [HttpGet("{potionId}/help")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<List<ResponseRecipeWithIngredients>>> GetHelpForBrewingPotion(long potionId)
        {
            List<ResponseRecipeWithIngredients> recipes = await _service.GetRecipesForBrewingPotion(potionId);
            return recipes.Any() ? recipes : StatusCode(StatusCodes.Status404NotFound, "We don't know any recipe with these ingredients!");
        }
    }
}
