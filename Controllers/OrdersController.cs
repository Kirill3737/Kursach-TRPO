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
    public class OrdersController : Controller
    {
        private КурсачEntities db = new КурсачEntities();

        // GET: Orders
        public ActionResult Index()
        {
            var orders = db.Orders.Include(o => o.CarDetails).Include(o => o.Klients).Include(o => o.Reviews);
            return View(orders.ToList());
        }

        // GET: Orders/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Orders orders = db.Orders.Find(id);
            if (orders == null)
            {
                return HttpNotFound();
            }
            return View(orders);
        }

        // GET: Orders/Create
        public ActionResult Create()
        {
            ViewBag.id_details = new SelectList(db.CarDetails, "id_details", "year_release");
            ViewBag.id_client = new SelectList(db.Klients, "id_client", "FIO_client");
            ViewBag.id_review = new SelectList(db.Reviews, "id_review", "text_review");
            return View();
        }

        // POST: Orders/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. 
        // Дополнительные сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id_order,id_client,id_details,registration_date,id_review")] Orders orders)
        {
            if (ModelState.IsValid)
            {
                db.Orders.Add(orders);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.id_details = new SelectList(db.CarDetails, "id_details", "year_release", orders.id_details);
            ViewBag.id_client = new SelectList(db.Klients, "id_client", "FIO_client", orders.id_client);
            ViewBag.id_review = new SelectList(db.Reviews, "id_review", "text_review", orders.id_review);
            return View(orders);
        }

        // GET: Orders/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Orders orders = db.Orders.Find(id);
            if (orders == null)
            {
                return HttpNotFound();
            }
            ViewBag.id_details = new SelectList(db.CarDetails, "id_details", "year_release", orders.id_details);
            ViewBag.id_client = new SelectList(db.Klients, "id_client", "FIO_client", orders.id_client);
            ViewBag.id_review = new SelectList(db.Reviews, "id_review", "text_review", orders.id_review);
            return View(orders);
        }

        // POST: Orders/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. 
        // Дополнительные сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id_order,id_client,id_details,registration_date,id_review")] Orders orders)
        {
            if (ModelState.IsValid)
            {
                db.Entry(orders).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.id_details = new SelectList(db.CarDetails, "id_details", "year_release", orders.id_details);
            ViewBag.id_client = new SelectList(db.Klients, "id_client", "FIO_client", orders.id_client);
            ViewBag.id_review = new SelectList(db.Reviews, "id_review", "text_review", orders.id_review);
            return View(orders);
        }

        // GET: Orders/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Orders orders = db.Orders.Find(id);
            if (orders == null)
            {
                return HttpNotFound();
            }
            return View(orders);
        }

        // POST: Orders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Orders orders = db.Orders.Find(id);
            db.Orders.Remove(orders);
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
