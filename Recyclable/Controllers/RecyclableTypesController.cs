using System;
using System.Net;
using System.Web.Mvc;
using Recyclable.Models;
using Recyclable.Services;

namespace Recyclable.Controllers
{
    public class RecyclableTypesController : Controller
    {
        private readonly RecyclableTypeService _service;

        public RecyclableTypesController()
        {
            _service = new RecyclableTypeService(new RecyclableDbContext());
        }

        public ActionResult Index() =>
            View(_service.GetAllRecyclableTypes());

        public ActionResult Details(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var recyclableType = _service.GetRecyclableTypeById(id.Value);
            if (recyclableType == null)
                return HttpNotFound();

            return View(recyclableType);
        }

        public ActionResult Create() => View();

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(RecyclableType recyclableType)
        {
            if (ModelState.IsValid)
            {
                _service.AddRecyclableType(recyclableType);
                return RedirectToAction("Index");
            }
            return View(recyclableType);
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var recyclableType = _service.GetRecyclableTypeById(id.Value);
            if (recyclableType == null)
                return HttpNotFound();

            return View(recyclableType);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(RecyclableType recyclableType)
        {
            if (ModelState.IsValid)
            {
                _service.UpdateRecyclableType(recyclableType);
                return RedirectToAction("Index");
            }
            return View(recyclableType);
        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            try
            {
                _service.DeleteRecyclableType(id);
                return Json(new { success = true });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }
    }
}
