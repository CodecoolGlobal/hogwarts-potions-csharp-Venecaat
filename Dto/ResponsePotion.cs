using HogwartsPotions.Models.Enums;
using System.Collections.Generic;
using System.Linq;
using HogwartsPotions.Models.Entities;

namespace HogwartsPotions.Dto
{
    public class ResponsePotion
    {
        public long Id { get; set; }

        public string Name { get; set; }
        public ResponseStudent Student { get; set; }
        public List<ResponseIngredient> Ingredients { get; set; }
        public BrewingStatus BrewingStatus { get; set; }
        public ResponseRecipe Recipe { get; set; }

        public ResponsePotion MapTo(Potion potion)
        {
            ResponseStudent responseStudent = new ResponseStudent();
            ResponseRecipe responseRecipe = new ResponseRecipe();

            Id = potion.Id;
            Name = potion.Name;
            Student = responseStudent.MapTo(potion.Student);
            Ingredients = potion.Ingredients.Select(ing => new ResponseIngredient() {Id = ing.Id, Name = ing.Name}).ToList();
            BrewingStatus = potion.BrewingStatus;
            if (potion.Recipe is not null) Recipe = responseRecipe.MapTo(potion.Recipe);

            return this;
        }
    }
}
