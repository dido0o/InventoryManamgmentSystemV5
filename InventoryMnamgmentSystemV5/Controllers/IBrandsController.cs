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
    public class IBrandsController : Controller
    {
        private InventoryManagmentSystemEntities db = new InventoryManagmentSystemEntities();

        // GET: IBrands
        public ActionResult Index(string searchString)
        {
            var iBrand = from i in db.IBrand select i;

            if (!String.IsNullOrEmpty(searchString))
            {
                iBrand = iBrand.Where(s => s.BrandName.Contains(searchString));
            }

            return View(iBrand.ToList());
        }

        // GET: IBrands/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            IBrand iBrand = db.IBrand.Find(id);
            if (iBrand == null)
            {
                return HttpNotFound();
            }
            return View(iBrand);
        }

        // GET: IBrands/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: IBrands/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "BrandID,BrandName")] IBrand iBrand)
        {
            if (ModelState.IsValid)
            {
                db.IBrand.Add(iBrand);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(iBrand);
        }

        // GET: IBrands/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            IBrand iBrand = db.IBrand.Find(id);
            if (iBrand == null)
            {
                return HttpNotFound();
            }
            return View(iBrand);
        }

        // POST: IBrands/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "BrandID,BrandName")] IBrand iBrand)
        {
            if (ModelState.IsValid)
            {
                db.Entry(iBrand).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(iBrand);
        }

        // GET: IBrands/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            IBrand iBrand = db.IBrand.Find(id);
            if (iBrand == null)
            {
                return HttpNotFound();
            }
            return View(iBrand);
        }

        // POST: IBrands/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            IBrand iBrand = db.IBrand.Find(id);
            db.IBrand.Remove(iBrand);
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
