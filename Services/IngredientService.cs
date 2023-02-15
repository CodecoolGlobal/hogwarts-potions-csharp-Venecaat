using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HogwartsPotions.Dto;
using HogwartsPotions.Models;
using HogwartsPotions.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace HogwartsPotions.Services
{
    public class IngredientService : IIngredientService
    {
        private readonly HogwartsContext _context;

        public IngredientService(HogwartsContext context)
        {
            _context = context;
        }

        public async Task<List<ResponseIngredient>> GetAll()
        {
            List<Ingredient> ingredients = await _context.Ingredients.ToListAsync();
            return ingredients.Select(ing => new ResponseIngredient().MapTo(ing)).ToList();
        }

        public async Task<Ingredient> Add(IngredientWithName newIngredient)
        {
            Ingredient ingredient = await FindByName(newIngredient.Name);

            if (ingredient is null)
            {
                ingredient = new Ingredient() {Name = newIngredient.Name};

                await _context.Ingredients.AddAsync(ingredient);
                await _context.SaveChangesAsync();

                ingredient = await FindByName(newIngredient.Name);
            }

            return ingredient;
        }

        public async Task<Ingredient> FindByName(string name)
        {
            Ingredient ingredient = await _context.Ingredients
                .FirstOrDefaultAsync(ing => ing.Name.ToLower() == name.ToLower());
            return ingredient;
        }

        public async Task<Ingredient> FindById(long id)
        {
            Ingredient ingredient = await _context.Ingredients
                .FirstOrDefaultAsync(ing => ing.Id == id);
            return ingredient;
        }
    }
}
