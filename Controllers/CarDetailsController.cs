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
    public class CarDetailsController : Controller
    {
        private КурсачEntities db = new КурсачEntities();

        // GET: CarDetails
        public ActionResult Index()
        {
            var carDetails = db.CarDetails.Include(c => c.CarMark);
            return View(carDetails.ToList());
        }

        // GET: CarDetails/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CarDetails carDetails = db.CarDetails.Find(id);
            if (carDetails == null)
            {
                return HttpNotFound();
            }
            return View(carDetails);
        }

        // GET: CarDetails/Create
        public ActionResult Create()
        {
            ViewBag.id_mark = new SelectList(db.CarMark, "id_mark", "mark_name");
            return View();
        }

        // POST: CarDetails/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. 
        // Дополнительные сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id_details,id_mark,detail_articul,year_release,cost,kuzov_type")] CarDetails carDetails)
        {
            if (ModelState.IsValid)
            {
                db.CarDetails.Add(carDetails);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.id_mark = new SelectList(db.CarMark, "id_mark", "mark_name", carDetails.id_mark);
            return View(carDetails);
        }

        // GET: CarDetails/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CarDetails carDetails = db.CarDetails.Find(id);
            if (carDetails == null)
            {
                return HttpNotFound();
            }
            ViewBag.id_mark = new SelectList(db.CarMark, "id_mark", "mark_name", carDetails.id_mark);
            return View(carDetails);
        }

        // POST: CarDetails/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. 
        // Дополнительные сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id_details,id_mark,detail_articul,year_release,cost,kuzov_type")] CarDetails carDetails)
        {
            if (ModelState.IsValid)
            {
                db.Entry(carDetails).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.id_mark = new SelectList(db.CarMark, "id_mark", "mark_name", carDetails.id_mark);
            return View(carDetails);
        }

        // GET: CarDetails/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CarDetails carDetails = db.CarDetails.Find(id);
            if (carDetails == null)
            {
                return HttpNotFound();
            }
            return View(carDetails);
        }

        // POST: CarDetails/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            CarDetails carDetails = db.CarDetails.Find(id);
            db.CarDetails.Remove(carDetails);
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
