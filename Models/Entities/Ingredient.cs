﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace HogwartsPotions.Models.Entities
{
    public class Ingredient
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        public string Name { get; set; }
        public ICollection<Recipe> Recipes { get; set; } = new List<Recipe>();
        public ICollection<Potion> Potions { get; set; } = new List<Potion>();
    }
}
