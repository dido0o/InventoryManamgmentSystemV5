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
    public class ITypesController : Controller
    {
        private InventoryManagmentSystemEntities db = new InventoryManagmentSystemEntities();

        // GET: ITypes
        public ActionResult Index(string searchString)
        {
            var types = from i in db.IType select i;

            if (!string.IsNullOrEmpty(searchString))
            {
                types = types.Where(i => i.ITypeName.Contains(searchString));
            }

            return View(types.ToList());
        }

        // GET: ITypes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            IType iType = db.IType.Find(id);
            if (iType == null)
            {
                return HttpNotFound();
            }
            return View(iType);
        }

        // GET: ITypes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ITypes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "TypeId,ITypeName")] IType iType)
        {
            if (ModelState.IsValid)
            {
                db.IType.Add(iType);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(iType);
        }

        // GET: ITypes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            IType iType = db.IType.Find(id);
            if (iType == null)
            {
                return HttpNotFound();
            }
            return View(iType);
        }

        // POST: ITypes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "TypeId,ITypeName")] IType iType)
        {
            if (ModelState.IsValid)
            {
                db.Entry(iType).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(iType);
        }

        // GET: ITypes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            IType iType = db.IType.Find(id);
            if (iType == null)
            {
                return HttpNotFound();
            }
            return View(iType);
        }

        // POST: ITypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            IType iType = db.IType.Find(id);
            db.IType.Remove(iType);
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
