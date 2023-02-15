using System.Collections.Generic;
using System.Dynamic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using ElProyecteGrande.Interfaces.Services;
using HogwartsPotions.Dto;
using HogwartsPotions.Services;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace HogwartsPotions.Controllers
{
    public class PotionViewController : Controller
    {
        private readonly IIngredientService _ingredientService;
        private readonly IPotionService _potionService;

        public PotionViewController(IIngredientService ingredientService, IPotionService potionService)
        {
            _ingredientService = ingredientService;
            _potionService = potionService;
        }

        // GET: Main Page
        public ActionResult Index()
        {
            return View();
        }

        // GET: Create new potion page
        [HttpGet]
        public async Task<ActionResult<List<ResponseStudent>>> BrewNewPotion()
        {
            List<ResponseStudent> students = new List<ResponseStudent>();

            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync("https://localhost:44390/api/Student"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    students = JsonConvert.DeserializeObject<List<ResponseStudent>>(apiResponse);
                }
            }

            return View(students);
        }

        [HttpPost]
        [ActionName("BrewNewPotion")]
        public async Task<ActionResult> BrewNewPotionPost()
        {
            string studentId = Request.Form["studentId"];
            StudentWithId student = new StudentWithId() {Id = long.Parse(studentId)};
            StringContent content = new StringContent(JsonConvert.SerializeObject(student), Encoding.UTF8, "application/json");

            ResponseBrewingPotion potion = new ResponseBrewingPotion();

            using (var httpClient = new HttpClient())
            {
                await httpClient.PostAsync("https://localhost:44390/api/Potion/brew", content);
            }

            return View("Info", "You started brewing a potion!");
        }

        [HttpGet]
        public async Task<ActionResult<List<ResponseStudent>>> AddIngredientToPotion()
        {
            List<ResponseStudent> students = new List<ResponseStudent>();
            List<ResponseIngredient> ingredients = await _ingredientService.GetAll();
            ResponseBrewingPotion potion = await _potionService.GetBrewingPotionByStudentId(1);
            dynamic studentsIngredients = new ExpandoObject();

            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync("https://localhost:44390/api/Student"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    students = JsonConvert.DeserializeObject<List<ResponseStudent>>(apiResponse);
                }
            }

            studentsIngredients.Students = students;
            studentsIngredients.Ingredients = ingredients;
            studentsIngredients.BrewingPotion = potion;

            return View(studentsIngredients);
        }
    }
}
