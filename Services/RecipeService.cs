using HogwartsPotions.Models;
using HogwartsPotions.Models.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HogwartsPotions.Services
{
    public class RecipeService : IRecipeService
    {
        private readonly HogwartsContext _context;

        public RecipeService(HogwartsContext context)
        {
            _context = context;
        }

        public async Task<List<Recipe>> GetAllRecipe() => await _context.Recipes.ToListAsync();

        public async Task<Recipe> AddRecipe(Recipe newRecipe)
        {
            await _context.Recipes.AddAsync(newRecipe);
            await _context.SaveChangesAsync();
            return newRecipe;
        }
    }
}
