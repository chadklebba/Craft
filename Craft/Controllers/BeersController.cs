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
using Microsoft.AspNet.Identity;

namespace Craft.Controllers
{
    public class BeersController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Beers
        public ActionResult Index()
        {
            return View(db.Beers.ToList());
        }

        
        public ActionResult BeerList()
        {
            List<Beer> BeerList = db.Beers.ToList();
            List<Distributor> distributors = db.Distributors.ToList();
            List<Distributor_Beer> distributor_beer = db.Distributor_Beers.ToList();
            List<Beer> DistBeerList = new List<Beer>();
            List<int> BeerIds = new List<int>();
            var currentUserId = User.Identity.GetUserId();
            var currentDist = (from d in distributors where d.UserId == currentUserId select d.DistributorId).FirstOrDefault();
            for (int i = 0; i < distributor_beer.Count; i++)
            {
                if(distributor_beer[i].DistributorId == currentDist)
                {
                    BeerIds.Add(distributor_beer[i].BeerId);
                }
            }
            for (int h = 0; h < BeerList.Count; h++)
            {
                for (int j = 0; j < BeerIds.Count; j++)
                {
                    if(BeerIds[j] == BeerList[h].BeerId)
                    {
                        DistBeerList.Add(BeerList[h]);
                    }
                }
            }
            
            return View(DistBeerList);
        }

        // GET: Beers/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Beer beer = db.Beers.Find(id);
            if (beer == null)
            {
                return HttpNotFound();
            }
            return View(beer);
        }

        // GET: Beers/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Beers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "BeerName,BeerId,Description,Type,ABV")] Beer beer)
        {
            List<Distributor> AllDistributors = db.Distributors.ToList();
            string currentUserId = User.Identity.GetUserId();
            Distributor currentDistributor = (from x in AllDistributors where x.UserId == currentUserId select x).FirstOrDefault();
            Distributor_Beer distributor_beer = new Distributor_Beer();
            if (ModelState.IsValid)
            {
                db.Beers.Add(beer);
                db.SaveChanges();
                distributor_beer.BeerId = beer.BeerId;
                distributor_beer.DistributorId = currentDistributor.DistributorId;
                db.Distributor_Beers.Add(distributor_beer);
                db.SaveChanges();
                return RedirectToAction("BeerList");
            }

            return View(beer);
        }

        // GET: Beers/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Beer beer = db.Beers.Find(id);
            if (beer == null)
            {
                return HttpNotFound();
            }
            return View(beer);
        }

        // POST: Beers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "BeerId,Description,Type,ABV")] Beer beer)
        {
            if (ModelState.IsValid)
            {
                db.Entry(beer).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(beer);
        }

        // GET: Beers/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Beer beer = db.Beers.Find(id);
            if (beer == null)
            {
                return HttpNotFound();
            }
            return View(beer);
        }

        // POST: Beers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Beer beer = db.Beers.Find(id);
            db.Beers.Remove(beer);
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
