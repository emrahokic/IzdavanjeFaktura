using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using IzdavanjeFaktura.Models.Models;
using IzdavanjeFaktura.Repository;
using IzdavanjeFaktura.Services;

namespace IzdavanjeFaktura.Controllers
{
    public class PorezController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        private readonly IPorezService _porezService;

        public PorezController(IPorezService porezService)
        {
            _porezService = porezService;
        }
        // GET: Porez
        public ActionResult Index()
        {

            var x = _porezService.GetAll();
            return View(x);
        }

        // GET: Porez/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Porez porez = db.Porez.Find(id);
            if (porez == null)
            {
                return HttpNotFound();
            }
            return View(porez);
        }

        // GET: Porez/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Porez/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "PorezID,Naziv,Iznos")] Porez porez)
        {
            if (ModelState.IsValid)
            {
                db.Porez.Add(porez);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(porez);
        }

        // GET: Porez/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Porez porez = db.Porez.Find(id);
            if (porez == null)
            {
                return HttpNotFound();
            }
            return View(porez);
        }

        // POST: Porez/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "PorezID,Naziv,Iznos")] Porez porez)
        {
            if (ModelState.IsValid)
            {
                db.Entry(porez).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(porez);
        }

        // GET: Porez/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Porez porez = db.Porez.Find(id);
            if (porez == null)
            {
                return HttpNotFound();
            }
            return View(porez);
        }

        // POST: Porez/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Porez porez = db.Porez.Find(id);
            db.Porez.Remove(porez);
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
