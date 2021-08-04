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
    public class IStatusController : Controller
    {
        private InventoryManagmentSystemEntities db = new InventoryManagmentSystemEntities();

        // GET: IStatus
        public ActionResult Index(string searchString)
        {
            var status = from i in db.IStatus select i;

            if (!string.IsNullOrEmpty(searchString))
            {
                status = status.Where(i => i.StatusName.Contains(searchString));
            }

            return View(status.ToList());
        }

        // GET: IStatus/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            IStatus iStatus = db.IStatus.Find(id);
            if (iStatus == null)
            {
                return HttpNotFound();
            }
            return View(iStatus);
        }

        // GET: IStatus/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: IStatus/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "StatusID,StatusName")] IStatus iStatus)
        {
            if (ModelState.IsValid)
            {
                db.IStatus.Add(iStatus);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(iStatus);
        }

        // GET: IStatus/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            IStatus iStatus = db.IStatus.Find(id);
            if (iStatus == null)
            {
                return HttpNotFound();
            }
            return View(iStatus);
        }

        // POST: IStatus/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "StatusID,StatusName")] IStatus iStatus)
        {
            if (ModelState.IsValid)
            {
                db.Entry(iStatus).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(iStatus);
        }

        // GET: IStatus/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            IStatus iStatus = db.IStatus.Find(id);
            if (iStatus == null)
            {
                return HttpNotFound();
            }
            return View(iStatus);
        }

        // POST: IStatus/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            IStatus iStatus = db.IStatus.Find(id);
            db.IStatus.Remove(iStatus);
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
