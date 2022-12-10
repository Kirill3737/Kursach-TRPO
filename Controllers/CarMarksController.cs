using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Курсач.Models;

namespace Курсач.Controllers
{
    public class CarMarksController : Controller
    {
        private КурсачEntities db = new КурсачEntities();

        // GET: CarMarks
        public ActionResult Index()
        {
            return View(db.CarMark.ToList());
        }

        // GET: CarMarks/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CarMark carMark = db.CarMark.Find(id);
            if (carMark == null)
            {
                return HttpNotFound();
            }
            return View(carMark);
        }

        // GET: CarMarks/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CarMarks/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. 
        // Дополнительные сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id_mark,mark_name,country_of_origin,KPP_type,supply_contract,photo")] CarMark car, HttpPostedFileBase upload)
        {
            if (ModelState.IsValid)
            {
                if (upload != null && upload.ContentLength > 0)
                {
                    using (var reader = new System.IO.BinaryReader(upload.InputStream))
                    {
                        car.photo = reader.ReadBytes(upload.ContentLength);
                    }
                }
                db.CarMark.Add(car);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.id_model = new SelectList(db.CarMark, "id_mark", "mark_name", car.id_mark);
            return View(car);
        }


        // GET: CarMarks/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CarMark carMark = db.CarMark.Find(id);
            if (carMark == null)
            {
                return HttpNotFound();
            }
            return View(carMark);
        }

        // POST: CarMarks/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. 
        // Дополнительные сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id_mark,mark_name,country_of_origin,KPP_type,supply_contract,photo")] CarMark carMark, HttpPostedFileBase upload)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.Entry(carMark).State = EntityState.Modified;
                    if (upload != null && upload.ContentLength > 0)
                    {
                        using (var reader = new System.IO.BinaryReader(upload.InputStream))
                        {
                            carMark.photo = reader.ReadBytes(upload.ContentLength);
                        }
                        db.SaveChanges();
                    }

                    else
                    {
                        db.Entry(carMark).Property(m => m.photo).IsModified = false;
                        db.SaveChanges();
                    }

                    return RedirectToAction("Index");
                }

            }
            catch (Exception e) { }
            return View(carMark);
        }

        // GET: CarMarks/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CarMark carMark = db.CarMark.Find(id);
            if (carMark == null)
            {
                return HttpNotFound();
            }
            return View(carMark);
        }

        // POST: CarMarks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            CarMark carMark = db.CarMark.Find(id);
            db.CarMark.Remove(carMark);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
