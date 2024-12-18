using System;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using Recyclable.Models;
using Recyclable.Services;

namespace Recyclable.Controllers
{
    public class RecyclableItemsController : Controller
    {
        private readonly RecyclableItemService _recyclableItemService;

        public RecyclableItemsController()
        {
            var context = new RecyclableDbContext();
            _recyclableItemService = new RecyclableItemService(context);
        }

        public ActionResult Index()
        {
            try
            {
                var recyclableItems = _recyclableItemService.GetAllRecyclableItems();
                return View(recyclableItems);
            }
            catch
            {
                return new HttpStatusCodeResult(HttpStatusCode.InternalServerError, "Error retrieving data.");
            }
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            try
            {
                var recyclableItem = _recyclableItemService.GetRecyclableItemById(id.Value);
                if (recyclableItem == null)
                {
                    return HttpNotFound();
                }
                return View(recyclableItem);
            }
            catch
            {
                return new HttpStatusCodeResult(HttpStatusCode.InternalServerError, "Error retrieving data.");
            }
        }

        public ActionResult Create()
        {
            {
                var recyclableTypes = _recyclableItemService.GetAllRecyclableTypes();

                var selectListItems = recyclableTypes.Select(rt => new SelectListItem
                {
                    Value = rt.Id.ToString(),
                    Text = rt.Type
                }).ToList();

                var recyclableItem = new RecyclableItem
                {
                    RecyclableTypeId = 0,
                    ItemDescription = "",
                    Weight = 0,
                    ComputedRate = 0
                };

                ViewBag.RecyclableTypeId = new SelectList(selectListItems, "Value", "Text");

                return View(recyclableItem);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,RecyclableTypeId,Weight,ComputedRate,ItemDescription")] RecyclableItem recyclableItem)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _recyclableItemService.AddRecyclableItem(recyclableItem);
                    return RedirectToAction("Index");
                }
                catch
                {
                    ModelState.AddModelError("", "Error saving data.");
                }
            }

            var recyclableTypes = _recyclableItemService.GetAllRecyclableTypes();
            var selectListItems = recyclableTypes.Select(rt => new SelectListItem
            {
                Value = rt.Id.ToString(),
                Text = rt.Type
            }).ToList();

            selectListItems.Insert(0, new SelectListItem
            {
                Value = "",
                Text = "--- Select Type ---",
                Selected = recyclableItem.RecyclableTypeId == 0
            });

            ViewBag.RecyclableTypeId = new SelectList(selectListItems, "Value", "Text", recyclableItem.RecyclableTypeId);

            return View(recyclableItem);
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            try
            {
                var recyclableItem = _recyclableItemService.GetRecyclableItemById(id.Value);
                if (recyclableItem == null)
                {
                    return HttpNotFound();
                }
                var recyclableTypes = _recyclableItemService.GetAllRecyclableTypes();
                var selectListItems = recyclableTypes.Select(rt => new SelectListItem
                {
                    Value = rt.Id.ToString(),
                    Text = rt.Type
                }).ToList();

                selectListItems.Insert(0, new SelectListItem
                {
                    Value = "",
                    Text = "--- Select Type ---",
                    Selected = recyclableItem.RecyclableTypeId == 0
                });

                ViewBag.RecyclableTypeId = new SelectList(selectListItems, "Value", "Text", recyclableItem.RecyclableTypeId);

                return View(recyclableItem);
            }
            catch
            {
                return new HttpStatusCodeResult(HttpStatusCode.InternalServerError, "Error retrieving data.");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,RecyclableTypeId,Weight,ComputedRate,ItemDescription")] RecyclableItem recyclableItem)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _recyclableItemService.UpdateRecyclableItem(recyclableItem);
                    return RedirectToAction("Index");
                }
                catch
                {
                    ModelState.AddModelError("", "Error saving data.");
                }
            }

            var recyclableTypes = _recyclableItemService.GetAllRecyclableTypes();
            var selectListItems = recyclableTypes.Select(rt => new SelectListItem
            {
                Value = rt.Id.ToString(),
                Text = rt.Type
            }).ToList();

            selectListItems.Insert(0, new SelectListItem
            {
                Value = "",
                Text = "--- Select Type ---",
                Selected = recyclableItem.RecyclableTypeId == 0
            });

            ViewBag.RecyclableTypeId = new SelectList(selectListItems, "Value", "Text", recyclableItem.RecyclableTypeId);

            return View(recyclableItem);
        }

        [HttpPost]
        public JsonResult Delete(int id)
        {
            try
            {
                _recyclableItemService.DeleteRecyclableItem(id);
                return Json(new { success = true });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }

        public JsonResult GetRecyclableTypeRate(int recyclableTypeId)
        {
            var recyclableType = _recyclableItemService.GetAllRecyclableTypes()
                .Where(r => r.Id == recyclableTypeId).Select(r => new { r.Rate, r.MinKg, r.MaxKg }).FirstOrDefault();

            return Json(new
            {
                success = true,
                rate = recyclableType.Rate,
                minKg = recyclableType.MinKg,
                maxKg = recyclableType.MaxKg
            }, JsonRequestBehavior.AllowGet);
        }
    }
}
