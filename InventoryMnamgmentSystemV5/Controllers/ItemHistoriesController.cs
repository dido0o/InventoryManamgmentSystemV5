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
    public class ItemHistoriesController : Controller
    {
        private InventoryManagmentSystemEntities db = new InventoryManagmentSystemEntities();

        // GET: ItemHistories
        public ActionResult Index(string searchString)
        {
            var itemHistory = db.ItemHistory.Include(i => i.IStatus).Include(i => i.VUsers).Include(i => i.Items);

            if (!string.IsNullOrEmpty(searchString))
            {
                itemHistory = itemHistory.Where(i => i.Items.ISerialNum.Contains(searchString));
            }

            return View(itemHistory.ToList());
        }

        // GET: ItemHistories/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ItemHistory itemHistory = db.ItemHistory.Find(id);
            if (itemHistory == null)
            {
                return HttpNotFound();
            }
            return View(itemHistory);
        }

        // GET: ItemHistories/Create
        public ActionResult Create()
        {
            ViewBag.CurrentStatusID = new SelectList(db.IStatus, "StatusID", "StatusName");
            ViewBag.CurrentOwnerID = new SelectList(db.VUsers, "UserID", "UserName");
            ViewBag.ItemId = new SelectList(db.Items, "ItemId", "ISerialNum");
            return View();
        }

        // POST: ItemHistories/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "RecordId,ItemId,CurrentOwnerID,CurrentStatusID,CurrentDateTime,Notes")] ItemHistory itemHistory)
        {
            if (ModelState.IsValid)
            {
                itemHistory.CurrentDateTime = DateTime.Now;
                db.ItemHistory.Add(itemHistory);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CurrentStatusID = new SelectList(db.IStatus, "StatusID", "StatusName", itemHistory.CurrentStatusID);
            ViewBag.CurrentOwnerID = new SelectList(db.VUsers, "UserID", "UserName", itemHistory.CurrentOwnerID);
            ViewBag.ItemId = new SelectList(db.Items, "ItemId", "ISerialNum", itemHistory.ItemId);
            return View(itemHistory);
        }

        // GET: ItemHistories/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ItemHistory itemHistory = db.ItemHistory.Find(id);
            if (itemHistory == null)
            {
                return HttpNotFound();
            }
            ViewBag.CurrentStatusID = new SelectList(db.IStatus, "StatusID", "StatusName", itemHistory.CurrentStatusID);
            ViewBag.CurrentOwnerID = new SelectList(db.VUsers, "UserID", "UserName", itemHistory.CurrentOwnerID);
            ViewBag.ItemId = new SelectList(db.Items, "ItemId", "ISerialNum", itemHistory.ItemId);
            return View(itemHistory);
        }

        // POST: ItemHistories/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "RecordId,ItemId,CurrentOwnerID,CurrentStatusID,CurrentDateTime,Notes")] ItemHistory itemHistory)
        {
            if (ModelState.IsValid)
            {
                db.Entry(itemHistory).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CurrentStatusID = new SelectList(db.IStatus, "StatusID", "StatusName", itemHistory.CurrentStatusID);
            ViewBag.CurrentOwnerID = new SelectList(db.VUsers, "UserID", "UserName", itemHistory.CurrentOwnerID);
            ViewBag.ItemId = new SelectList(db.Items, "ItemId", "ISerialNum", itemHistory.ItemId);
            return View(itemHistory);
        }

        // GET: ItemHistories/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ItemHistory itemHistory = db.ItemHistory.Find(id);
            if (itemHistory == null)
            {
                return HttpNotFound();
            }
            return View(itemHistory);
        }

        // POST: ItemHistories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ItemHistory itemHistory = db.ItemHistory.Find(id);
            db.ItemHistory.Remove(itemHistory);
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

        //--------------------------------------------------------------------------------------------------------------
        //--------------------------------------------------------------------------------------------------------------

        public ActionResult Report(string searchString)
        {
            var history = from i in db.ItemHistory select i;

            if (!string.IsNullOrEmpty(searchString))
            {
                history = history.Where(i => i.Items.ISerialNum.Contains(searchString));
            }

            return View(history);
        }
    }
}
