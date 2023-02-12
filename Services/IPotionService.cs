using System.Collections.Generic;
using System.Threading.Tasks;
using HogwartsPotions.Models.Entities;

namespace ElProyecteGrande.Interfaces.Services;

public interface IPotionService
{
    Task<List<Potion>> GetAll();
}