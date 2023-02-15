using HogwartsPotions.Models.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;

namespace HogwartsPotions.Dto
{
    public class IngredientWithName
    {
        public string Name { get; set; }

        public IngredientWithName MapTo(Ingredient ingredient)
        {
            Name = ingredient.Name;

            return this;
        }
    }
}
