using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using InventoryMnamgmentSystemV5.Models;

namespace InventoryMnamgmentSystemV5.Controllers
{
    public class LLocationsController : Controller
    {
        private InventoryManagmentSystemEntities db = new InventoryManagmentSystemEntities();

        // GET: LLocations
        public ActionResult Index(string searchString)
        {
            var locations = from i in db.LLocation select i;

            if (!string.IsNullOrEmpty(searchString))
            {
                locations = locations.Where(i => i.LocationName.Contains(searchString));
            }

            return View(locations.ToList());
        }

        // GET: LLocations/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LLocation lLocation = db.LLocation.Find(id);
            if (lLocation == null)
            {
                return HttpNotFound();
            }
            return View(lLocation);
        }

        // GET: LLocations/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: LLocations/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "LocationId,LocationName")] LLocation lLocation)
        {
            if (ModelState.IsValid)
            {
                db.LLocation.Add(lLocation);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(lLocation);
        }

        // GET: LLocations/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LLocation lLocation = db.LLocation.Find(id);
            if (lLocation == null)
            {
                return HttpNotFound();
            }
            return View(lLocation);
        }

        // POST: LLocations/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "LocationId,LocationName")] LLocation lLocation)
        {
            if (ModelState.IsValid)
            {
                db.Entry(lLocation).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(lLocation);
        }

        // GET: LLocations/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LLocation lLocation = db.LLocation.Find(id);
            if (lLocation == null)
            {
                return HttpNotFound();
            }
            return View(lLocation);
        }

        // POST: LLocations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            LLocation lLocation = db.LLocation.Find(id);
            db.LLocation.Remove(lLocation);
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
