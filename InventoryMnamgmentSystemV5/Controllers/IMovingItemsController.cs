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
    public class IMovingItemsController : Controller 
    {
        private InventoryManagmentSystemEntities db = new InventoryManagmentSystemEntities();

        // GET: IMovingItems
        public ActionResult Index(string searchString)
        {
            var iMovingItem = db.IMovingItem.Include(i => i.IStatus).Include(i => i.VUsers).Include(i => i.Items).Include(i => i.VUsers1);

            if (!string.IsNullOrEmpty(searchString))
            {
                iMovingItem = iMovingItem.Where(i => i.Items.ISerialNum.Contains(searchString));
            }

            return View(iMovingItem.ToList());
        }

        // GET: IMovingItems/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            IMovingItem iMovingItem = db.IMovingItem.Find(id);
            if (iMovingItem == null)
            {
                return HttpNotFound();
            }
            return View(iMovingItem);
        }

        // GET: IMovingItems/Create
        public ActionResult Create()
        {
            ViewBag.currentStatusId = new SelectList(db.IStatus, "StatusID", "StatusName");
            ViewBag.currentUserId = new SelectList(db.VUsers, "UserID", "UserName");
            ViewBag.ItemId = new SelectList(db.Items, "ItemId", "ISerialNum");
            ViewBag.previousUserId = new SelectList(db.VUsers, "UserID", "UserName");
            return View();
        }

        // POST: IMovingItems/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "RecordId,ItemId,previousUserId,currentUserId,currentStatusId,curretnDateTime,notes")] IMovingItem iMovingItem)
        {
            if (ModelState.IsValid)
            {
                deleteFromUsersItems(iMovingItem.ItemId);
                db.SaveChanges();
                insertIntoUsersItems(iMovingItem.ItemId, iMovingItem.currentUserId);
                addToItemHistory(iMovingItem);
                iMovingItem.curretnDateTime = DateTime.Now;
                db.IMovingItem.Add(iMovingItem);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.currentStatusId = new SelectList(db.IStatus, "StatusID", "StatusName", iMovingItem.currentStatusId);
            ViewBag.currentUserId = new SelectList(db.VUsers, "UserID", "UserName", iMovingItem.currentUserId);
            ViewBag.ItemId = new SelectList(db.Items, "ItemId", "ISerialNum", iMovingItem.ItemId);
            ViewBag.previousUserId = new SelectList(db.VUsers, "UserID", "UserName", iMovingItem.previousUserId);
            return View(iMovingItem);
        }

        // GET: IMovingItems/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            IMovingItem iMovingItem = db.IMovingItem.Find(id);
            if (iMovingItem == null)
            {
                return HttpNotFound();
            }
            ViewBag.currentStatusId = new SelectList(db.IStatus, "StatusID", "StatusName", iMovingItem.currentStatusId);
            ViewBag.currentUserId = new SelectList(db.VUsers, "UserID", "UserName", iMovingItem.currentUserId);
            ViewBag.ItemId = new SelectList(db.Items, "ItemId", "ISerialNum", iMovingItem.ItemId);
            ViewBag.previousUserId = new SelectList(db.VUsers, "UserID", "UserName", iMovingItem.previousUserId);
            return View(iMovingItem);
        }

        // POST: IMovingItems/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "RecordId,ItemId,previousUserId,currentUserId,currentStatusId,curretnDateTime,notes")] IMovingItem iMovingItem)
        {
            if (ModelState.IsValid)
            {
                db.Entry(iMovingItem).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.currentStatusId = new SelectList(db.IStatus, "StatusID", "StatusName", iMovingItem.currentStatusId);
            ViewBag.currentUserId = new SelectList(db.VUsers, "UserID", "UserName", iMovingItem.currentUserId);
            ViewBag.ItemId = new SelectList(db.Items, "ItemId", "ISerialNum", iMovingItem.ItemId);
            ViewBag.previousUserId = new SelectList(db.VUsers, "UserID", "UserName", iMovingItem.previousUserId);
            return View(iMovingItem);
        }

        // GET: IMovingItems/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            IMovingItem iMovingItem = db.IMovingItem.Find(id);
            if (iMovingItem == null)
            {
                return HttpNotFound();
            }
            return View(iMovingItem);
        }

        // POST: IMovingItems/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            IMovingItem iMovingItem = db.IMovingItem.Find(id);
            db.IMovingItem.Remove(iMovingItem);
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

        public void deleteFromUsersItems(int id)
        {

            var userItem = db.UsersItems.Where(x => x.ItemId == id).FirstOrDefault();

            if (userItem != null) //if null that means the item does not exist
            {
                db.UsersItems.Remove(userItem);
            } //To be continued: To handle the kind of error that accurs when trying to remove an item and its not found
            return;
        }

        public void insertIntoUsersItems(int ItemId, int UserId)
        {
            if (!checkItem(ItemId))
            {
                UsersItems newUserItemAssos = new UsersItems();
                newUserItemAssos.ItemId = ItemId;
                newUserItemAssos.UserId = UserId;
                newUserItemAssos.currentDateTime = DateTime.Now;

                db.UsersItems.Add(newUserItemAssos);
            }
        }

        public bool checkItem(int id)
        {
            var UsersItems = db.UsersItems.Where(s => s.ItemId == id).FirstOrDefault();
            bool IExist;

            if (UsersItems != null)
            {
                IExist = true; // Item does exist in database
            }
            else IExist = false;
            return IExist;
        }

        public void addToItemHistory(IMovingItem item)
        {
            ItemHistory historyRecord = new ItemHistory();
            historyRecord.ItemId = item.ItemId;
            historyRecord.CurrentOwnerID = item.currentUserId;
            historyRecord.CurrentStatusID = item.currentStatusId;
            historyRecord.CurrentDateTime = DateTime.Now;
            historyRecord.Notes = "نقل عهدة " + item.notes;

            db.ItemHistory.Add(historyRecord);
        }

        public ActionResult Report()
        {
            var movedItems = db.IMovingItem;
            return View(movedItems.ToList());
        }
    }
}
