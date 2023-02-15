using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using HogwartsPotions.Dto;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace HogwartsPotions.Controllers
{
    public class PotionViewController : Controller
    {
        // GET: Main Page
        public ActionResult Index()
        {
            return View();
        }

        // GET: Create new potion page
        [HttpGet]
        public async Task<ActionResult<List<ResponseStudent>>> BrewNewPotion()
        {
            List<ResponseStudent> students = new List<ResponseStudent>();

            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync("https://localhost:44390/api/Student"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    students = JsonConvert.DeserializeObject<List<ResponseStudent>>(apiResponse);
                }
            }

            return View(students);
        }

        //// POST: PotionViewController/Create
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Create(IFormCollection collection)
        //{
        //    try
        //    {
        //        return RedirectToAction(nameof(Index));
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}

        //// GET: PotionViewController/Edit/5
        //public ActionResult Edit(int id)
        //{
        //    return View();
        //}

        //// POST: PotionViewController/Edit/5
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Edit(int id, IFormCollection collection)
        //{
        //    try
        //    {
        //        return RedirectToAction(nameof(Index));
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}

        //// GET: PotionViewController/Delete/5
        //public ActionResult Delete(int id)
        //{
        //    return View();
        //}

        //// POST: PotionViewController/Delete/5
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Delete(int id, IFormCollection collection)
        //{
        //    try
        //    {
        //        return RedirectToAction(nameof(Index));
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}
    }
}
