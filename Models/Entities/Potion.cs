using HogwartsPotions.Models.Enums;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace HogwartsPotions.Models.Entities
{
    public class Potion
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        public string Name { get; set; }
        public Student Student { get; set; }
        public ICollection<Ingredient> Ingredients { get; set; }
        public BrewingStatus BrewingStatus { get; set; }
        public Recipe Recipe { get; set; }
    }
}
