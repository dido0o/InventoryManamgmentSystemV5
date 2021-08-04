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
    public class UsersItemsController : Controller
    {
        private InventoryManagmentSystemEntities db = new InventoryManagmentSystemEntities();

        // GET: UsersItems
        public ActionResult Index(string searchString)
        {
            var usersItems = db.UsersItems.Include(u => u.IStatus).Include(u => u.Items).Include(u => u.VUsers);

            if (!string.IsNullOrEmpty(searchString))
            {
                usersItems = usersItems.Where(i => i.Items.ISerialNum.Contains(searchString));
            }

            return View(usersItems.ToList());
        }

        // GET: UsersItems/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UsersItems usersItems = db.UsersItems.Find(id);
            if (usersItems == null)
            {
                return HttpNotFound();
            }
            return View(usersItems);
        }

        // GET: UsersItems/Create
        public ActionResult Create()
        {
            ViewBag.CurrentStatusId = new SelectList(db.IStatus, "StatusID", "StatusName");
            ViewBag.ItemId = new SelectList(db.Items, "ItemId", "ISerialNum");
            ViewBag.UserId = new SelectList(db.VUsers, "UserID", "UserName");
            return View();
        }

        // POST: UsersItems/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HandleError]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "RecordId,UserId,ItemId,currentDateTime,CurrentStatusId")] UsersItems usersItems)
        {
            if (ModelState.IsValid)
            {
                usersItems.currentDateTime = DateTime.Now;
                addToItemHistory(usersItems);
                db.UsersItems.Add(usersItems);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CurrentStatusId = new SelectList(db.IStatus, "StatusID", "StatusName", usersItems.CurrentStatusId);
            ViewBag.ItemId = new SelectList(db.Items, "ItemId", "ISerialNum", usersItems.ItemId);
            ViewBag.UserId = new SelectList(db.VUsers, "UserID", "UserName", usersItems.UserId);
            return View(usersItems);
        }

        // GET: UsersItems/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UsersItems usersItems = db.UsersItems.Find(id);
            if (usersItems == null)
            {
                return HttpNotFound();
            }
            ViewBag.CurrentStatusId = new SelectList(db.IStatus, "StatusID", "StatusName", usersItems.CurrentStatusId);
            ViewBag.ItemId = new SelectList(db.Items, "ItemId", "ISerialNum", usersItems.ItemId);
            ViewBag.UserId = new SelectList(db.VUsers, "UserID", "UserName", usersItems.UserId);
            return View(usersItems);
        }

        // POST: UsersItems/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "RecordId,UserId,ItemId,currentDateTime,CurrentStatusId")] UsersItems usersItems)
        {
            if (ModelState.IsValid)
            {
                db.Entry(usersItems).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CurrentStatusId = new SelectList(db.IStatus, "StatusID", "StatusName", usersItems.CurrentStatusId);
            ViewBag.ItemId = new SelectList(db.Items, "ItemId", "ISerialNum", usersItems.ItemId);
            ViewBag.UserId = new SelectList(db.VUsers, "UserID", "UserName", usersItems.UserId);
            return View(usersItems);
        }

        // GET: UsersItems/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UsersItems usersItems = db.UsersItems.Find(id);
            if (usersItems == null)
            {
                return HttpNotFound();
            }
            return View(usersItems);
        }

        // POST: UsersItems/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            UsersItems usersItems = db.UsersItems.Find(id);
            db.UsersItems.Remove(usersItems);
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

        public void addToItemHistory(UsersItems item)
        {
            ItemHistory historyRecord = new ItemHistory();
            historyRecord.ItemId = item.ItemId;
            historyRecord.CurrentOwnerID = item.UserId;
            historyRecord.CurrentStatusID = 0;
            historyRecord.CurrentDateTime = DateTime.Now;
            historyRecord.Notes = "تسليم عهدة";

            db.ItemHistory.Add(historyRecord);
        }

        public ActionResult Report()
        {
            var usersItems = from i in db.UsersItems select i;

            return View(usersItems);
        }
    }
}
