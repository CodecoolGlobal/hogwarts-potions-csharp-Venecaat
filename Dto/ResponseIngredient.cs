using HogwartsPotions.Models.Entities;

namespace HogwartsPotions.Dto
{
    public class ResponseIngredient
    {
        public long Id { get; set; }

        public string Name { get; set; }

        public ResponseIngredient MapTo(Ingredient ingredient)
        {
            Id = ingredient.Id;
            Name = ingredient.Name;

            return this;
        }
    }
}
