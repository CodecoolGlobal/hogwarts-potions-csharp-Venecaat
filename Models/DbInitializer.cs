﻿using HogwartsPotions.Models.Entities;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;
using System.Linq;
using HogwartsPotions.Models.Enums;

namespace HogwartsPotions.Models
{
    public static class DbInitializer
    {
        public static void Seed(IApplicationBuilder applicationBuilder)
        {
            using var serviceScope = applicationBuilder.ApplicationServices.CreateScope();
            var context = serviceScope.ServiceProvider.GetService<HogwartsContext>();

            if (context == null)
            {
                return;
            }

            _ = context.Database.EnsureCreated();

            // Rooms
            if (context.Rooms.Any()) return;

            Room room1 = new()
            {
                Capacity = 4,
                Residents = new HashSet<Student>()
            };
            Room room2 = new()
            {
                Capacity = 4,
                Residents = new HashSet<Student>()
            };
            Room room3 = new()
            {
                Capacity = 4,
                Residents = new HashSet<Student>()
            };

            // Students
            if (context.Students.Any()) return;

            Student student1 = new()
            {
                Name = "Sol Elderberry",
                HouseType = HouseType.Ravenclaw,
                PetType = PetType.Cat,
                Room = room1
            };
            Student student2 = new()
            {
                Name = "Fennel Inkwood",
                HouseType = HouseType.Gryffindor,
                PetType = PetType.Owl,
                Room = room2
            };
            Student student3 = new()
            {
                Name = "Dakota Nightingale",
                HouseType = HouseType.Ravenclaw,
                PetType = PetType.None,
                Room = room1
            };
            Student student4 = new()
            {
                Name = "Angel Lambkins",
                HouseType = HouseType.Hufflepuff,
                PetType = PetType.Owl,
                Room = room2
            };
            Student student5 = new()
            {
                Name = "Shell Firethron",
                HouseType = HouseType.Slytherin,
                PetType = PetType.Rat,
                Room = room3
            };
            room1.Residents.Add(student1);
            room2.Residents.Add(student2);
            room1.Residents.Add(student3);
            room2.Residents.Add(student4);
            room3.Residents.Add(student5);

            context.Rooms.AddRange(room1);
            context.Students.AddRange(student1, student2, student3, student4, student5);
            context.SaveChanges();

            // Ingredients
            if (context.Ingredients.Any()) return;

            Ingredient fairyWing = new() { Name = "Fairy Wing" };
            Ingredient dragonLiver = new() { Name = "Dragon Liver" };
            Ingredient dandelionRoot = new() { Name = "Dandelion Root" };
            Ingredient avocado = new() { Name = "Avocado" };
            Ingredient crocodileHeart = new() { Name = "Crocodile Heart" };
            Ingredient knotgrass = new() { Name = "Knotgrass" };
            Ingredient mandrakeRoot = new() { Name = "Mandrake Root" };
            Ingredient vervain = new() { Name = "Vervain" };
            Ingredient wormwood = new() { Name = "Wormwood" };
            Ingredient bubotuberPus = new() { Name = "Bubotuber Pus" };
            Ingredient dittany = new() { Name = "Dittany" };
            Ingredient unicornTailhair = new() { Name = "Unicorn Tailhair" };
            Ingredient daisyRoot = new() { Name = "Daisy Root" };
            Ingredient shrivelfig = new() { Name = "Shrivelfig" };
            Ingredient caterpillar = new() { Name = "Caterpillar" };
            Ingredient ratSpleen = new() { Name = "Rat Spleen" };
            Ingredient leechJuice = new() { Name = "Leech Juice" };
            Ingredient cowbane = new() { Name = "Cowbane" };

            context.Ingredients.AddRange(fairyWing, dragonLiver, dandelionRoot, avocado, crocodileHeart, knotgrass, mandrakeRoot, vervain,
                wormwood, bubotuberPus, dittany, unicornTailhair, daisyRoot, shrivelfig, caterpillar, ratSpleen, leechJuice, cowbane);
            context.SaveChanges();

            // Recipes
            if (context.Recipes.Any()) return;

            Recipe healingPotionRecipe = new()
            {
                Name = "Healing Potion",
                Student = student1,
                Ingredients = new List<Ingredient>()
                {
                    dittany, dragonLiver, bubotuberPus, unicornTailhair, wormwood
                }
            };

            Recipe shrinkingPotionRecipe = new()
            {
                Name = "Shrinking Potion",
                Student = student1,
                Ingredients = new List<Ingredient>()
                {
                    daisyRoot, shrivelfig, leechJuice, cowbane, wormwood
                }
            };

            context.Recipes.AddRange(healingPotionRecipe, shrinkingPotionRecipe);
            context.SaveChanges();

            // Potions
            if (context.Potions.Any()) return;

            Potion healingPotion = new()
            {
                Name = "Healing Potion",
                Student = student1,
                Ingredients = new List<Ingredient>()
                {
                    dittany, dragonLiver, bubotuberPus, unicornTailhair, wormwood
                },
                BrewingStatus = BrewingStatus.Discovery,
                Recipe = healingPotionRecipe,
            };

            Potion shrinkingPotion = new()
            {
                Name = "Shrinking Potion",
                Student = student1,
                Ingredients = new List<Ingredient>()
                {
                    daisyRoot, shrivelfig, leechJuice, cowbane, wormwood
                },
                BrewingStatus = BrewingStatus.Discovery,
                Recipe = shrinkingPotionRecipe,
            };

            context.Potions.AddRange(healingPotion, shrinkingPotion);
            context.SaveChanges();
        }
    }
}
