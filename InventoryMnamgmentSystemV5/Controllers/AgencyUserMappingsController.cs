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
    public class AgencyUserMappingsController : Controller
    {
        private InventoryManagmentSystemEntities db = new InventoryManagmentSystemEntities();

        // GET: AgencyUserMappings
        public ActionResult Index(string searchString)
        {
            var agencyUserMapping = db.AgencyUserMapping.Include(a => a.Agency).Include(a => a.VUsers);
            if (!String.IsNullOrEmpty(searchString))
            {
                agencyUserMapping = agencyUserMapping.Where(s => s.VUsers.UserName.Contains(searchString));
            }
            return View(agencyUserMapping.ToList());
        }

        // GET: AgencyUserMappings/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AgencyUserMapping agencyUserMapping = db.AgencyUserMapping.Find(id);
            if (agencyUserMapping == null)
            {
                return HttpNotFound();
            }
            return View(agencyUserMapping);
        }

        // GET: AgencyUserMappings/Create
        public ActionResult Create()
        {
            ViewBag.AgencyId = new SelectList(db.Agency, "AgencyID", "AgencyName");
            ViewBag.UserId = new SelectList(db.VUsers, "UserID", "UserName");
            return View();
        }

        // POST: AgencyUserMappings/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "RecordId,AgencyId,UserId")] AgencyUserMapping agencyUserMapping)
        {
            if (ModelState.IsValid)
            {
                db.AgencyUserMapping.Add(agencyUserMapping);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.AgencyId = new SelectList(db.Agency, "AgencyID", "AgencyName", agencyUserMapping.AgencyId);
            ViewBag.UserId = new SelectList(db.VUsers, "UserID", "UserName", agencyUserMapping.UserId);
            return View(agencyUserMapping);
        }

        // GET: AgencyUserMappings/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AgencyUserMapping agencyUserMapping = db.AgencyUserMapping.Find(id);
            if (agencyUserMapping == null)
            {
                return HttpNotFound();
            }
            ViewBag.AgencyId = new SelectList(db.Agency, "AgencyID", "AgencyName", agencyUserMapping.AgencyId);
            ViewBag.UserId = new SelectList(db.VUsers, "UserID", "UserName", agencyUserMapping.UserId);
            return View(agencyUserMapping);
        }

        // POST: AgencyUserMappings/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "RecordId,AgencyId,UserId")] AgencyUserMapping agencyUserMapping)
        {
            if (ModelState.IsValid)
            {
                db.Entry(agencyUserMapping).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.AgencyId = new SelectList(db.Agency, "AgencyID", "AgencyName", agencyUserMapping.AgencyId);
            ViewBag.UserId = new SelectList(db.VUsers, "UserID", "UserName", agencyUserMapping.UserId);
            return View(agencyUserMapping);
        }

        // GET: AgencyUserMappings/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AgencyUserMapping agencyUserMapping = db.AgencyUserMapping.Find(id);
            if (agencyUserMapping == null)
            {
                return HttpNotFound();
            }
            return View(agencyUserMapping);
        }

        // POST: AgencyUserMappings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            AgencyUserMapping agencyUserMapping = db.AgencyUserMapping.Find(id);
            db.AgencyUserMapping.Remove(agencyUserMapping);
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

        //--------------------------------------------------------------------------------------------------------------------------------------------
        //--------------------------------------------------------------------------------------------------------------------------------------------
        public ActionResult Report()
        {
            var userAgency = db.AgencyUserMapping;

            return View(userAgency.ToList());
        }
    }
}
