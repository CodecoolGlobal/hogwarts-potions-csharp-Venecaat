﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ElProyecteGrande.Interfaces.Services;
using HogwartsPotions.Dto;
using HogwartsPotions.Models;
using HogwartsPotions.Models.Entities;
using HogwartsPotions.Models.Enums;
using Microsoft.EntityFrameworkCore;

namespace HogwartsPotions.Services
{
    public class PotionService : IPotionService
    {
        private readonly HogwartsContext _context;
        private readonly IStudentService _studentService;
        private readonly IRecipeService _recipeService;
        private readonly IIngredientService _ingredientService;
        public const int MaxIngredientsForPotions = 5;
        private static readonly List<string> DiscoveryMsgs = new List<string>
        {
            "Congratulation! You discovered a new potion!",
            "Amazing! Looks like it's a new potion!",
            "You're a genius! We didn't know this potion!"
        };

        public PotionService(HogwartsContext context, IStudentService studentService, IRecipeService recipeService, IIngredientService ingredientService)
        {
            _context = context;
            _studentService = studentService;
            _recipeService = recipeService;
            _ingredientService = ingredientService;
        }

        public async Task<List<ResponsePotion>> GetAll()
        {
            List<Potion> potions = await _context.Potions.ToListAsync();

            return potions.Select(p => new ResponsePotion().MapTo(p)).ToList();
        }

        public async Task<ResponsePotion> Add(NewPotion newPotion)
        {
            List<Ingredient> allIngredient = await _context.Ingredients.ToListAsync();
            IEnumerable<Ingredient> ingredients = allIngredient.Where(ing => newPotion.IngredientIds.Contains(ing.Id));

            List<Recipe> recipes = await _recipeService.GetAllRecipe();
            bool foundRecipe = false;

            Student student = await _studentService.GetStudentById(newPotion.StudentId);

            Potion potion = new Potion()
            {
                Name = $"{student.Name}'s brewing",
                Student = student,
                Ingredients = ingredients.ToList()
            };

            if (ingredients.Count() < MaxIngredientsForPotions)
            {
                potion.BrewingStatus = BrewingStatus.Brew;
            }
            else
            {
                foreach (Recipe recipe in recipes)
                {
                    if (recipe.Ingredients.All(ingredients.Contains))
                    {
                        foundRecipe = true;
                        potion.Recipe = recipe;
                        potion.Name = $"{recipe.Name} replica";
                        break;
                    }
                }
                potion.BrewingStatus = foundRecipe ? BrewingStatus.Replica : BrewingStatus.Discovery;

                if (!foundRecipe)
                {
                    potion.Name = $"{student.Name}'s discovery";
                    potion.Recipe = new Recipe()
                    {
                        Name = $"{student.Name}'s discovery",
                        Student = student,
                        Ingredients = ingredients.ToList()
                    };
                    await _recipeService.AddRecipe(potion.Recipe);
                }
            }

            await _context.Potions.AddAsync(potion);
            await _context.SaveChangesAsync();

            ResponsePotion responsePotion = new ResponsePotion();

            return responsePotion.MapTo(potion);
        }

        public async Task<List<PotionWithIdAndName>> GetPotionsByStudentId(long id)
        {
            List<Potion> potions = await _context.Potions.ToListAsync();

            return potions.Select(p => new PotionWithIdAndName().MapTo(p)).ToList();
        }

        public async Task<ResponseBrewingPotion> AddBrewingPotion(long studentId)
        {
            Student student = await _studentService.GetStudentById(studentId);

            Potion potion = new Potion()
            {
                Student = student,
                BrewingStatus = BrewingStatus.Brew,
                Ingredients = new List<Ingredient>()
            };

            ResponseBrewingPotion brewingPotion = new ResponseBrewingPotion().MapTo(potion);

            await _context.Potions.AddAsync(potion);
            await _context.SaveChangesAsync();

            return brewingPotion;
        }

        public async Task<Potion> Find(long potionId)
        {
            Potion potion = await _context.Potions
                .FirstOrDefaultAsync(p => p.Id == potionId);
            return potion;
        }

        public async Task<ResponseBrewingPotion> AddIngredientToBrewingPotion(long potionId, IngredientWithName ingredient)
        {
            Potion potion = await Find(potionId);
            Ingredient ingredientToAdd = await _ingredientService.Add(ingredient);
            potion.Ingredients.Add(ingredientToAdd);

            if (potion.Ingredients.Count == MaxIngredientsForPotions)
            {
                List<Ingredient> allIngredient = await _context.Ingredients.ToListAsync();
                IEnumerable<Ingredient> ingredients = allIngredient.Where(ing => potion.Ingredients.Contains(ing));

                List<Recipe> recipes = await _recipeService.GetAllRecipe();
                bool foundRecipe = false;

                Student student = potion.Student;

                foreach (Recipe recipe in recipes)
                {
                    if (recipe.Ingredients.All(ingredients.Contains))
                    {
                        foundRecipe = true;
                        potion.Recipe = recipe;
                        potion.Name = $"{recipe.Name} replica";
                        break;
                    }
                }
                potion.BrewingStatus = foundRecipe ? BrewingStatus.Replica : BrewingStatus.Discovery;
            }
            ResponseBrewingPotion brewingPotion = new ResponseBrewingPotion().MapTo(potion);

            _context.Update(potion);
            await _context.SaveChangesAsync();

            return brewingPotion;
        }
        public async Task<ResponsePotion> NameAndFinalizePotion(long potionId, string name)
        {
            Potion potion = await Find(potionId);

            potion.Name = name;
            potion.Recipe = new Recipe()
            {
                Name = name,
                Student = potion.Student,
                Ingredients = potion.Ingredients.ToList()
            };
            ResponsePotion newPotion = new ResponsePotion().MapTo(potion);

            await _recipeService.AddRecipe(potion.Recipe);
            _context.Update(potion);
            await _context.SaveChangesAsync();

            return newPotion;
        }

        public async Task<List<ResponseRecipeWithIngredients>> GetRecipesForBrewingPotion(long potionId)
        {
            Potion potion = await Find(potionId);
            List<Recipe> recipes = await _context.Recipes.ToListAsync();
            IEnumerable<Recipe> recipesWithSameIngredients = recipes.Where(r => potion.Ingredients.All(r.Ingredients.Contains));

            return recipesWithSameIngredients.Select(r => new ResponseRecipeWithIngredients().MapTo(r)).ToList();
        }

        public async Task<ResponseBrewingPotion> GetBrewingPotionByStudentId(long id)
        {
            List<Potion> potions = await _context.Potions.ToListAsync();
            IEnumerable<ResponseBrewingPotion> brewingPotions = potions.Select(p => new ResponseBrewingPotion().MapTo(p));
            
            ResponseBrewingPotion brewingPotion = brewingPotions.FirstOrDefault(p => p.Student.Id == id && p.BrewingStatus == BrewingStatus.Brew);

            return brewingPotion;
        }

        public string GetRandomDiscoveryMessage()
        {
            Random rnd = new Random();
            return DiscoveryMsgs.ElementAt(rnd.Next(DiscoveryMsgs.Count));
        }

        public async Task<int> GetReplicaNumber(long potionId)
        {
            List<Potion> potions = await _context.Potions.ToListAsync();
            Potion potion = await Find(potionId);

            return potions.Count(p => p.Recipe == potion.Recipe);
        }
    }
}
