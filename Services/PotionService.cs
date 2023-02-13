using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ElProyecteGrande.Interfaces.Services;
using HogwartsPotions.Dto;
using HogwartsPotions.Models;
using HogwartsPotions.Models.Entities;
using HogwartsPotions.Models.Enums;
using Microsoft.EntityFrameworkCore;

namespace HogwartsPotions.Services
{
    public class PotionService : IPotionService
    {
        private readonly HogwartsContext _context;
        private readonly IStudentService _studentService;
        private readonly IRecipeService _recipeService;
        public const int MaxIngredientsForPotions = 5;

        public PotionService(HogwartsContext context, IStudentService studentService, IRecipeService recipeService)
        {
            _context = context;
            _studentService = studentService;
            _recipeService = recipeService;
        }

        public async Task<List<ResponsePotion>> GetAll()
        {
            List<Potion> potions = await _context.Potions.ToListAsync();

            return potions.Select(p => new ResponsePotion().MapTo(p)).ToList();
        }

        public async Task<ResponsePotion> Add(NewPotion newPotion)
        {
            List<Ingredient> allIngredient = await _context.Ingredients.ToListAsync();
            IEnumerable<Ingredient> ingredients = allIngredient.Where(ing => newPotion.IngredientIds.Contains(ing.Id));

            List<Recipe> recipes = await _recipeService.GetAllRecipe();
            IEnumerable<Recipe> recipesWithSameAmountOfIngredients = recipes.Where(r => r.Ingredients.Count == ingredients.Count());
            bool foundRecipe = false;

            Student student = await _studentService.GetStudentById(newPotion.StudentId);

            Potion potion = new Potion()
            {
                Name = $"{student.Name}'s brewing",
                Student = student,
                Ingredients = ingredients.ToList()
            };

            if (ingredients.Count() < MaxIngredientsForPotions)
            {
                potion.BrewingStatus = BrewingStatus.Brew;
            }
            else
            {
                foreach (Recipe recipe in recipesWithSameAmountOfIngredients)
                {
                    if (recipe.Ingredients.All(ingredients.Contains))
                    {
                        foundRecipe = true;
                        potion.Recipe = recipe;
                        potion.Name = $"{recipe.Name} replica";
                        break;
                    }
                }
                potion.BrewingStatus = foundRecipe ? BrewingStatus.Replica : BrewingStatus.Discovery;

                if (!foundRecipe)
                {
                    potion.Name = $"{student.Name}'s discovery";
                    potion.Recipe = new Recipe()
                    {
                        Name = $"{student.Name}'s discovery",
                        Student = student,
                        Ingredients = ingredients.ToList()
                    };
                    await _recipeService.AddRecipe(potion.Recipe);
                }
            }

            await _context.Potions.AddAsync(potion);
            await _context.SaveChangesAsync();

            ResponsePotion responsePotion = new ResponsePotion();

            return responsePotion.MapTo(potion);
        }
    }
}
