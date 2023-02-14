using HogwartsPotions.Models.Entities;

namespace HogwartsPotions.Dto
{
    public class ResponseRecipe
    {
        public long Id { get; set; }

        public string Name { get; set; }

        public virtual ResponseRecipe MapTo(Recipe recipe)
        {
            Id = recipe.Id;
            Name = recipe.Name;

            return this;
        }
    }
}
