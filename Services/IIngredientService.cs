using System.Collections.Generic;
using HogwartsPotions.Dto;
using System.Threading.Tasks;
using HogwartsPotions.Models.Entities;

namespace HogwartsPotions.Services
{
    public interface IIngredientService
    {
        Task<List<ResponseIngredient>> GetAll();
        Task<Ingredient> Add(IngredientWithName newIngredient);
        Task<Ingredient> FindByName(string name);
        Task<Ingredient> FindById(long id);
    }
}
