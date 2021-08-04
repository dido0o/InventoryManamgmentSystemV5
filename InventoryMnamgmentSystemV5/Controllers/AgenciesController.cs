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
    public class AgenciesController : Controller
    {
        private InventoryManagmentSystemEntities db = new InventoryManagmentSystemEntities();

        // GET: Agencies
        public ActionResult Index(string searchString)
        {

            var agencies = from i in db.Agency select i;

            if (!String.IsNullOrEmpty(searchString))
            {
                agencies = agencies.Where(s => s.AgencyName.Contains(searchString));
            }

            return View(agencies.ToList());
        }

        // GET: Agencies/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Agency agency = db.Agency.Find(id);
            if (agency == null)
            {
                return HttpNotFound();
            }
            return View(agency);
        }

        // GET: Agencies/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Agencies/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "AgencyID,AgencyName")] Agency agency)
        {
            if (ModelState.IsValid)
            {
                db.Agency.Add(agency);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(agency);
        }

        // GET: Agencies/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Agency agency = db.Agency.Find(id);
            if (agency == null)
            {
                return HttpNotFound();
            }
            return View(agency);
        }

        // POST: Agencies/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "AgencyID,AgencyName")] Agency agency)
        {
            if (ModelState.IsValid)
            {
                db.Entry(agency).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(agency);
        }

        // GET: Agencies/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Agency agency = db.Agency.Find(id);
            if (agency == null)
            {
                return HttpNotFound();
            }
            return View(agency);
        }

        // POST: Agencies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Agency agency = db.Agency.Find(id);
            db.Agency.Remove(agency);
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
