using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Craft.Models;
using Craft.Models.Craft;

namespace Craft.Controllers
{
    public class Distributor_BeerController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Distributor_Beer
        public ActionResult Index()
        {
            var distributor_Beers = db.Distributor_Beers.Include(d => d.beer).Include(d => d.distributor);
            return View(distributor_Beers.ToList());
        }

        // GET: Distributor_Beer/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Distributor_Beer distributor_Beer = db.Distributor_Beers.Find(id);
            if (distributor_Beer == null)
            {
                return HttpNotFound();
            }
            return View(distributor_Beer);
        }

        // GET: Distributor_Beer/Create
        public ActionResult Create()
        {
            ViewBag.BeerId = new SelectList(db.Beers, "BeerId", "BeerName");
            ViewBag.DistributorId = new SelectList(db.Distributors, "DistributorId", "Name");
            return View();
        }

        // POST: Distributor_Beer/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "DistributorId,BeerId")] Distributor_Beer distributor_Beer)
        {
            if (ModelState.IsValid)
            {
                db.Distributor_Beers.Add(distributor_Beer);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.BeerId = new SelectList(db.Beers, "BeerId", "BeerName", distributor_Beer.BeerId);
            ViewBag.DistributorId = new SelectList(db.Distributors, "DistributorId", "Name", distributor_Beer.DistributorId);
            return View(distributor_Beer);
        }

        // GET: Distributor_Beer/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Distributor_Beer distributor_Beer = db.Distributor_Beers.Find(id);
            if (distributor_Beer == null)
            {
                return HttpNotFound();
            }
            ViewBag.BeerId = new SelectList(db.Beers, "BeerId", "BeerName", distributor_Beer.BeerId);
            ViewBag.DistributorId = new SelectList(db.Distributors, "DistributorId", "Name", distributor_Beer.DistributorId);
            return View(distributor_Beer);
        }

        // POST: Distributor_Beer/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "DistributorId,BeerId")] Distributor_Beer distributor_Beer)
        {
            if (ModelState.IsValid)
            {
                db.Entry(distributor_Beer).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.BeerId = new SelectList(db.Beers, "BeerId", "BeerName", distributor_Beer.BeerId);
            ViewBag.DistributorId = new SelectList(db.Distributors, "DistributorId", "Name", distributor_Beer.DistributorId);
            return View(distributor_Beer);
        }

        // GET: Distributor_Beer/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Distributor_Beer distributor_Beer = db.Distributor_Beers.Find(id);
            if (distributor_Beer == null)
            {
                return HttpNotFound();
            }
            return View(distributor_Beer);
        }

        // POST: Distributor_Beer/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Distributor_Beer distributor_Beer = db.Distributor_Beers.Find(id);
            db.Distributor_Beers.Remove(distributor_Beer);
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
