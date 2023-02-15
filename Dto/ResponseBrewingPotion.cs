using HogwartsPotions.Models.Entities;
using HogwartsPotions.Models.Enums;
using System.Collections.Generic;
using System.Linq;

namespace HogwartsPotions.Dto
{
    public class ResponseBrewingPotion
    {
        public long Id { get; set; }
        public ResponseStudent Student { get; set; }
        public BrewingStatus BrewingStatus { get; set; }
        public List<ResponseIngredient> Ingredients { get; set; }

        public ResponseBrewingPotion MapTo(Potion potion)
        {
            ResponseStudent responseStudent = new ResponseStudent();

            Id = potion.Id;
            Student = responseStudent.MapTo(potion.Student);
            Ingredients = potion.Ingredients.Select(ing => new ResponseIngredient() { Id = ing.Id, Name = ing.Name }).ToList();
            BrewingStatus = potion.BrewingStatus;

            return this;
        }
    }
}
