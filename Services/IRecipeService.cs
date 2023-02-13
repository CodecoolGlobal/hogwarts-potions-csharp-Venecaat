using HogwartsPotions.Models.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HogwartsPotions.Services
{
    public interface IRecipeService
    {
        Task<Recipe> AddRecipe(Recipe newRecipe);
        Task<List<Recipe>> GetAllRecipe();
    }
}
