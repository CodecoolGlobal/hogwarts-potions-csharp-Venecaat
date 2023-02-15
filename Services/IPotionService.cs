using System.Collections.Generic;
using System.Threading.Tasks;
using HogwartsPotions.Dto;
using HogwartsPotions.Models.Entities;

namespace ElProyecteGrande.Interfaces.Services;

public interface IPotionService
{
    Task<List<ResponsePotion>> GetAll();
    Task<ResponsePotion> Add(NewPotion newPotion);
    Task<List<PotionWithIdAndName>> GetPotionsByStudentId(long id);
    Task<ResponseBrewingPotion> AddBrewingPotion(long studentId);
    Task<ResponseBrewingPotion> AddIngredientToBrewingPotion(long potionId, IngredientWithName ingredient);
    Task<Potion> Find(long potionId);
    Task<List<ResponseRecipeWithIngredients>> GetRecipesForBrewingPotion(long potionId);
    Task<ResponseBrewingPotion> GetBrewingPotionByStudentId(long id);
}