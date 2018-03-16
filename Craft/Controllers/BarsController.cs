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
    public class BarsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Bars
        public ActionResult Index()
        {
            return View(db.Bars.ToList());
        }

        public ActionResult BarList()
        {
            List<Bar> BarList = db.Bars.OrderBy(x=>x.BarName).ToList();
            List<Distributor> distributors = db.Distributors.ToList();
            var currentUserId = User.Identity.GetUserId();
            var currentDist = (from d in distributors where d.UserId == currentUserId select d.DistributorId).FirstOrDefault();
            List<Distributor_Bar> MyBarIds = db.Distributor_Bars.Where(x => x.DistributorId == currentDist).ToList();
            List<Bar> MyBarList = new List<Bar>();
            for (int i = 0; i < MyBarIds.Count; i++)
            {
                for (int j = 0; j < BarList.Count; j++)
                {
                    if (BarList[j].BarId == MyBarIds[i].BarId)
                    {
                        MyBarList.Add(BarList[j]);
                    }
                }
            }

            return View(MyBarList);
        }
        [HttpGet]
        public ActionResult ChooseBeer()
        {
            CustBarSearchViewModel Beers = new CustBarSearchViewModel();
            List<Beer> BeerList = db.Beers.ToList();
            List<Bar_Beer> Bar_Beers = db.Bar_Beers.ToList();
            for (int k = 0; k < BeerList.Count; k++)
            {
                SelectListItem item = new SelectListItem
                {
                    Text = BeerList[k].BeerName,
                    Value = BeerList[k].BeerId.ToString()
                };
                Beers.BeerNames.Add(item);

            }
            return View("CustBarSearch", Beers);
            
        }
        [HttpPost]
        public ActionResult ChooseBeer(CustBarSearchViewModel model)
        {
            return RedirectToAction("MapView", new { id = int.Parse(model.BeerId) });
        }
        
        public ActionResult MapView(int id)
        {
            CustBarSearchViewModel Model = new CustBarSearchViewModel();
            List<Bar_Beer> Bar_Beers = db.Bar_Beers.Where(x => x.BeerId == id).ToList();
            List<Bar> AllBars = db.Bars.ToList();
            var currentUserId = User.Identity.GetUserId();
            Customer CurrentCustomer = db.Customers.Where(x => x.UserId == currentUserId).FirstOrDefault();
            string customerAddress = CurrentCustomer.Street + " " + CurrentCustomer.City + " " + CurrentCustomer.State + " " + CurrentCustomer.ZipCode;
            List<string> stringAddresses = new List<string>();
            List<Bar> BeerBars = new List<Bar>();
            string labels = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            int index = 0;
            for ( int i = 0; i < Bar_Beers.Count; i++)
            {
                for (int j = 0; j < AllBars.Count; j++)
                {
                    if (Bar_Beers[i].BarId == AllBars[j].BarId)
                    {
                        AllBars[j].Legend = labels[index].ToString();
                        BeerBars.Add(AllBars[j]);
                        stringAddresses.Add(AllBars[j].Address);
                        index++;
                    }
                }
                
            }
            Model.BeerBars = BeerBars;
            Model.stringAddresses = stringAddresses;
            return View("MapView", Model);
        }

        public ActionResult AddBars()
        {
            AddBarViewModel Bars = new AddBarViewModel();
            List<SelectListItem> AvailableBarNames = new List<SelectListItem>(); 
            List<Distributor> distributors = db.Distributors.ToList();
            List<Bar> AllBars = db.Bars.ToList();
            List<Bar> AvailableBars = new List<Bar>();
            
            var currentUserId = User.Identity.GetUserId();
            var currentDist = (from d in distributors where d.UserId == currentUserId select d.DistributorId).FirstOrDefault();
            List<Distributor_Bar> MyBarIds = db.Distributor_Bars.Where(x => x.DistributorId == currentDist).ToList();
            for (int i = 0; i < MyBarIds.Count; i++)
            {
                for (int j=0; j < AllBars.Count; j++)
                {
                    if (MyBarIds[i].BarId == AllBars[j].BarId)
                    {
                        Bars.AddedBars.Add(AllBars[j]);
                    }
                }
            }
            AvailableBars = AllBars.Except(Bars.AddedBars).OrderBy(x=>x.BarName).ToList();
            for (int k = 0; k < AvailableBars.Count; k++)
            {
                SelectListItem item = new SelectListItem
                {
                    Text = AvailableBars[k].BarName,
                    Value = AvailableBars[k].BarId.ToString()
                };
                Bars.AvailableBarNames.Add(item);

            }
            
            return View("AddBars",Bars);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddBars(AddBarViewModel model)
        {
            if (ModelState.IsValid)
            {
                List<Distributor> distributors = db.Distributors.ToList();
                var currentUserId = User.Identity.GetUserId();
                var currentDist = (from d in distributors where d.UserId == currentUserId select d.DistributorId).FirstOrDefault();
                Distributor_Bar distributorBar = new Distributor_Bar();
                distributorBar.BarId = model.BarID;
                distributorBar.DistributorId = currentDist;
                db.Distributor_Bars.Add(distributorBar);
                db.SaveChanges();
                return RedirectToAction("BarList");
            }

            return RedirectToAction("BarList", "Bars");
        }
       
        
        public ActionResult RemoveBar(int? id)
        {
            Distributor_Bar distBar = db.Distributor_Bars.FirstOrDefault(i => i.BarId == id);
            db.Distributor_Bars.Remove(distBar);
            db.SaveChanges();
            return RedirectToAction("BarList", "Bars");
        }

        public ActionResult AddBeers(int? id)
        {
            AddBeerViewModel Beers = new AddBeerViewModel();
            Beers.BarId = (int)id;
            List<Beer> AvailableBeers = new List<Beer>();
            List<SelectListItem> AvailableBeerNames = new List<SelectListItem>();
            List<Beer> BeerList = db.Beers.OrderBy(x => x.BeerName).ToList();
            List<Distributor> distributors = db.Distributors.ToList();
            List<Distributor_Beer> distributor_beer = db.Distributor_Beers.ToList();
            List<Beer> DistBeerList = new List<Beer>();
            List<int> BeerIds = new List<int>();
            var currentUserId = User.Identity.GetUserId();
            var currentDist = (from d in distributors where d.UserId == currentUserId select d.DistributorId).FirstOrDefault();
            List<Bar_Beer> MyBeerIds = db.Bar_Beers.Where(x => x.BarId == Beers.BarId).ToList();
            for (int i = 0; i < distributor_beer.Count; i++)
            {
                if (distributor_beer[i].DistributorId == currentDist)
                {
                    BeerIds.Add(distributor_beer[i].BeerId);
                }
            }
            for (int h = 0; h < BeerList.Count; h++)
            {
                for (int j = 0; j < BeerIds.Count; j++)
                {
                    if (BeerIds[j] == BeerList[h].BeerId)
                    {
                        DistBeerList.Add(BeerList[h]);
                    }
                }
            }
            for (int i = 0; i < MyBeerIds.Count; i++)
            {
                for (int j = 0; j < BeerList.Count; j++)
                {
                    if (MyBeerIds[i].BeerId == BeerList[j].BeerId)
                    {
                        Beers.AddedBeers.Add(BeerList[j]);
                    }
                }
            }
            AvailableBeers = DistBeerList.Except(Beers.AddedBeers).OrderBy(x => x.BeerName).ToList();
            for (int k = 0; k < AvailableBeers.Count; k++)
            {
                SelectListItem item = new SelectListItem
                {
                    Text = AvailableBeers[k].BeerName,
                    Value = AvailableBeers[k].BeerId.ToString()
                };
                Beers.AvailableBeerNames.Add(item);

            }

            return View("AddBeers",Beers);
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddBeers(AddBeerViewModel model)
        {
            if (ModelState.IsValid)
            {
                Bar_Beer bar_beer = new Bar_Beer();
                bar_beer.BeerId = model.BeerId;
                bar_beer.BarId = model.BarId;
                db.Bar_Beers.Add(bar_beer);
                db.SaveChanges();
                return RedirectToAction("AddBeers");
            }

            return RedirectToAction("BarList", "Bars");
        }

        //public ActionResult DisplayBarBeers()
        //{

        //}
        // GET: Bars/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Bar bar = db.Bars.Find(id);
            if (bar == null)
            {
                return HttpNotFound();
            }
            return View(bar);
        }

        // GET: Bars/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Bars/Create
       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "BarId,BarName,Address")] Bar bar)
        {
            if (ModelState.IsValid)
            {
                db.Bars.Add(bar);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(bar);
        }

        // GET: Bars/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Bar bar = db.Bars.Find(id);
            if (bar == null)
            {
                return HttpNotFound();
            }
            return View(bar);
        }

        // POST: Bars/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "BarId,BarName,Address")] Bar bar)
        {
            if (ModelState.IsValid)
            {
                db.Entry(bar).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(bar);
        }

        // GET: Bars/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Bar bar = db.Bars.Find(id);
            if (bar == null)
            {
                return HttpNotFound();
            }
            return View(bar);
        }

        // POST: Bars/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Bar bar = db.Bars.Find(id);
            db.Bars.Remove(bar);
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
