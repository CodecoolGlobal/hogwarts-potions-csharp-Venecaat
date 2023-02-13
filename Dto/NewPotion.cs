using System.Collections.Generic;

namespace HogwartsPotions.Dto
{
    public class NewPotion
    {
        public long StudentId { get; set; }
        public ICollection<long> IngredientIds { get; set; }
    }
}
