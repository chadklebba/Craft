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
    public class FavoritesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: FavoriteBeerIds
        public ActionResult Index()
        {
            var favorites = db.Favorites.Include(f => f.Beer).Include(f => f.Customer);
            return View(favorites.ToList());
        }

        public ActionResult AddToFavorites()
        {
            FavoritesViewModel Favs = new FavoritesViewModel();
            var currentUserId = User.Identity.GetUserId();
            Customer customer = db.Customers.Where(x => x.UserId == currentUserId).FirstOrDefault();
            List<Beer> AllBeers = db.Beers.ToList();
            List<Beer> AvailableBeers = new List<Beer>();
            List<Favorite> FavoriteBeerIds = db.Favorites.Where(x => x.CustomerId == customer.CustomerId).ToList();
            List<Beer> FavoriteBeer = new List<Beer>();
            for (int i = 0; i < FavoriteBeerIds.Count; i++)
            {
                for(int j = 0; j < AllBeers.Count; j++)
                {
                    if (FavoriteBeerIds[i].BeerId == AllBeers[j].BeerId)
                    {
                        FavoriteBeer.Add(AllBeers[j]);
                    }
                }
            }
            Favs.FavoriteBeer = FavoriteBeer;
            AvailableBeers = AllBeers.Except(FavoriteBeer).ToList();
            for (int k = 0; k < AvailableBeers.Count; k++)
            {
                SelectListItem item = new SelectListItem
                {
                    Text = AvailableBeers[k].BeerName,
                    Value = AvailableBeers[k].BeerId.ToString()
                };
                Favs.AvailableBeerNames.Add(item);

            }
            for (int l = 0; l < Favs.FavoriteBeer.Count; l++)
            {
                for (int m = 0; m < AllBeers.Count; m++)
                {
                   if (Favs.FavoriteBeer[l].Type == AllBeers[m].Type)
                    {
                        if (AllBeers[m] != null) { Favs.RecommendedBeers.Add(AllBeers[m]); }
                    }

                }

            }
            Favs.RecommendedBeers = Favs.RecommendedBeers.Except(Favs.FavoriteBeer).ToList();
            return View("AddToFavorites", Favs);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddToFavorites(FavoritesViewModel model)
        {
            var currentUserId = User.Identity.GetUserId();
            Customer customer = db.Customers.Where(x => x.UserId == currentUserId).FirstOrDefault();
            Favorite favorite = new Favorite();
            favorite.BeerId = model.BeerId;
            favorite.CustomerId = customer.CustomerId;
            db.Favorites.Add(favorite);
            db.SaveChanges();
            return RedirectToAction("AddToFavorites");
        }

        public ActionResult RemoveBeer(int? id)
        {
            Favorite favorite = db.Favorites.FirstOrDefault(i => i.BeerId == id);
            db.Favorites.Remove(favorite);
            db.SaveChanges();
            return RedirectToAction("AddToFavorites", "Favorites");
        }
        // GET: FavoriteBeerIds/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Favorite favorite = db.Favorites.Find(id);
            if (favorite == null)
            {
                return HttpNotFound();
            }
            return View(favorite);
        }

        // GET: FavoriteBeerIds/Create
        public ActionResult Create()
        {
            ViewBag.BeerId = new SelectList(db.Beers, "BeerId", "BeerName");
            ViewBag.CustomerId = new SelectList(db.Customers, "CustomerId", "Name");
            return View();
        }

        // POST: FavoriteBeerIds/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "FavoriteId,CustomerId,BeerId")] Favorite favorite)
        {
            if (ModelState.IsValid)
            {
                db.Favorites.Add(favorite);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.BeerId = new SelectList(db.Beers, "BeerId", "BeerName", favorite.BeerId);
            ViewBag.CustomerId = new SelectList(db.Customers, "CustomerId", "Name", favorite.CustomerId);
            return View(favorite);
        }

        // GET: FavoriteBeerIds/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Favorite favorite = db.Favorites.Find(id);
            if (favorite == null)
            {
                return HttpNotFound();
            }
            ViewBag.BeerId = new SelectList(db.Beers, "BeerId", "BeerName", favorite.BeerId);
            ViewBag.CustomerId = new SelectList(db.Customers, "CustomerId", "Name", favorite.CustomerId);
            return View(favorite);
        }

        // POST: FavoriteBeerIds/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "FavoriteId,CustomerId,BeerId")] Favorite favorite)
        {
            if (ModelState.IsValid)
            {
                db.Entry(favorite).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.BeerId = new SelectList(db.Beers, "BeerId", "BeerName", favorite.BeerId);
            ViewBag.CustomerId = new SelectList(db.Customers, "CustomerId", "Name", favorite.CustomerId);
            return View(favorite);
        }

        // GET: FavoriteBeerIds/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Favorite favorite = db.Favorites.Find(id);
            if (favorite == null)
            {
                return HttpNotFound();
            }
            return View(favorite);
        }

        // POST: FavoriteBeerIds/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Favorite favorite = db.Favorites.Find(id);
            db.Favorites.Remove(favorite);
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
