using HogwartsPotions.Models.Entities;
using System.Collections.Generic;
using System.Linq;

namespace HogwartsPotions.Dto
{
    public class ResponseRecipe
    {
        public long Id { get; set; }

        public string Name { get; set; }
        public ResponseStudent Student { get; set; }
        public List<ResponseIngredient> Ingredients { get; set; }

        public ResponseRecipe MapTo(Recipe recipe)
        {
            ResponseStudent responseStudent = new ResponseStudent();

            Id = recipe.Id;
            Name = recipe.Name;
            Student = responseStudent.MapTo(recipe.Student);
            Ingredients = recipe.Ingredients.Select(ing => new ResponseIngredient() { Id = ing.Id, Name = ing.Name }).ToList();

            return this;
        }
    }
}
