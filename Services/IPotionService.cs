using System.Collections.Generic;
using System.Threading.Tasks;
using HogwartsPotions.Dto;
using HogwartsPotions.Models.Entities;

namespace ElProyecteGrande.Interfaces.Services;

public interface IPotionService
{
    Task<List<ResponsePotion>> GetAll();
    Task<ResponsePotion> Add(NewPotion newPotion);
    Task<List<PotionWithIdAndName>> GetPotionsByStudentId(long id);
    Task<ResponseBrewingPotion> AddBrewingPotion(long studentId);
}