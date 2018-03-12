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
            AvailableBars = AllBars.Except(Bars.AddedBars).ToList();
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
