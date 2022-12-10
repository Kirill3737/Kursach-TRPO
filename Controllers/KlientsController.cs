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
    public class KlientsController : Controller
    {
        private КурсачEntities db = new КурсачEntities();

        // GET: Klients
        public ViewResult Index(string sortOrder, string searchString)
        {
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "FIO_client_desc" : "";
            ViewBag.DateSortParm = sortOrder == "licience_number" ? "licience_number_desc" : "licience_number";
            ViewBag.IndividualTypeSortParam = sortOrder == "type_of_individual" ? "type_of_individual_desc" : "type_of_individual";
            var klients = from s in db.Klients
                          select s;

            if (!String.IsNullOrEmpty(searchString))
            {
                klients = klients.Where(s => s.FIO_client.Contains(searchString)); /*|| s.FirstMidName.Contains(searchString));*/
            }

            switch (sortOrder)
            {
                case "FIO_client_desc":
                    klients = klients.OrderByDescending(s => s.FIO_client);
                    break;
                case "licience_number_desc":
                    klients = klients.OrderBy(s => s.licience_number);
                    break;
                case "licience_number":
                    klients = klients.OrderByDescending(s => s.licience_number);
                    break;
                case "type_of_individual":
                    klients = klients.OrderByDescending(s => s.type_of_individual);
                    break;
                case "type_of_individual_desc":
                    klients = klients.OrderByDescending(s => s.type_of_individual);
                    break;
                default:
                    klients = klients.OrderBy(s => s.FIO_client);
                    break;
            }
            return View(klients.ToList());
        }

        // GET: Klients/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Klients klients = db.Klients.Find(id);
            if (klients == null)
            {
                return HttpNotFound();
            }
            return View(klients);
        }

        // GET: Klients/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Klients/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. 
        // Дополнительные сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id_client,FIO_client,passport_number,licience_number,type_of_individual,priority_payment_method")] Klients klients)
        {
            if (ModelState.IsValid)
            {
                db.Klients.Add(klients);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(klients);
        }

        // GET: Klients/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Klients klients = db.Klients.Find(id);
            if (klients == null)
            {
                return HttpNotFound();
            }
            return View(klients);
        }

        // POST: Klients/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. 
        // Дополнительные сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id_client,FIO_client,passport_number,licience_number,type_of_individual,priority_payment_method")] Klients klients)
        {
            if (ModelState.IsValid)
            {
                db.Entry(klients).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(klients);
        }

        // GET: Klients/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Klients klients = db.Klients.Find(id);
            if (klients == null)
            {
                return HttpNotFound();
            }
            return View(klients);
        }

        // POST: Klients/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Klients klients = db.Klients.Find(id);
            db.Klients.Remove(klients);
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
