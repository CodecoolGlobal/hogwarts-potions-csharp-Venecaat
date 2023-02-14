using HogwartsPotions.Models.Entities;
using System.Collections.Generic;
using System.Linq;

namespace HogwartsPotions.Dto
{
    public class ResponseRecipeWithIngredients : ResponseRecipe
    {
        public List<ResponseIngredient> Ingredients { get; set; }

        public override ResponseRecipeWithIngredients MapTo(Recipe recipe)
        {
            Id = recipe.Id;
            Name = recipe.Name;
            Ingredients = recipe.Ingredients.Select(ing => new ResponseIngredient() { Id = ing.Id, Name = ing.Name }).ToList();

            return this;
        }
    }
}
