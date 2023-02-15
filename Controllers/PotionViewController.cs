using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HogwartsPotions.Controllers
{
    public class PotionViewController : Controller
    {
        // GET: Main Page
        public ActionResult Index()
        {
            return View();
        }

        //// GET: PotionViewController/Details/5
        //public ActionResult Details(int id)
        //{
        //    return View();
        //}

        //// GET: PotionViewController/Create
        //public ActionResult Create()
        //{
        //    return View();
        //}

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
