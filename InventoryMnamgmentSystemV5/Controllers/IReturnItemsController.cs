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
    public class IReturnItemsController : Controller 
    {
        private InventoryManagmentSystemEntities db = new InventoryManagmentSystemEntities();

        // GET: IReturnItems
        public ActionResult Index(string searchString)
        {
            var iReturnItem = db.IReturnItem.Include(i => i.IStatus).Include(i => i.Items).Include(i => i.VUsers);

            if (!string.IsNullOrEmpty(searchString))
            {
                iReturnItem = iReturnItem.Where(i => i.Items.ISerialNum.Contains(searchString));
            }

            return View(iReturnItem.ToList());
        }

        // GET: IReturnItems/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            IReturnItem iReturnItem = db.IReturnItem.Find(id);
            if (iReturnItem == null)
            {
                return HttpNotFound();
            }
            return View(iReturnItem);
        }

        // GET: IReturnItems/Create
        public ActionResult Create()
        {
            ViewBag.IStatusID = new SelectList(db.IStatus, "StatusID", "StatusName");
            ViewBag.ItemId = new SelectList(db.Items, "ItemId", "ISerialNum");
            ViewBag.LastUserId = new SelectList(db.VUsers, "UserID", "UserName");
            return View();
        }

        // POST: IReturnItems/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "RecordId,ItemId,LastUserId,IStatusID,CurrentDateTime")] IReturnItem iReturnItem)
        {
            if (ModelState.IsValid)
            {
                iReturnItem.CurrentDateTime = DateTime.Now;
                db.IReturnItem.Add(iReturnItem);
                addToItemHistory(iReturnItem);
                removeFromUsersItems(iReturnItem.ItemId);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.IStatusID = new SelectList(db.IStatus, "StatusID", "StatusName", iReturnItem.IStatusID);
            ViewBag.ItemId = new SelectList(db.Items, "ItemId", "ISerialNum", iReturnItem.ItemId);
            ViewBag.LastUserId = new SelectList(db.VUsers, "UserID", "UserName", iReturnItem.LastUserId);
            return View(iReturnItem);
        }

        // GET: IReturnItems/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            IReturnItem iReturnItem = db.IReturnItem.Find(id);
            if (iReturnItem == null)
            {
                return HttpNotFound();
            }
            ViewBag.IStatusID = new SelectList(db.IStatus, "StatusID", "StatusName", iReturnItem.IStatusID);
            ViewBag.ItemId = new SelectList(db.Items, "ItemId", "ISerialNum", iReturnItem.ItemId);
            ViewBag.LastUserId = new SelectList(db.VUsers, "UserID", "UserName", iReturnItem.LastUserId);
            return View(iReturnItem);
        }

        // POST: IReturnItems/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "RecordId,ItemId,LastUserId,IStatusID,CurrentDateTime")] IReturnItem iReturnItem)
        {
            if (ModelState.IsValid)
            {
                db.Entry(iReturnItem).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IStatusID = new SelectList(db.IStatus, "StatusID", "StatusName", iReturnItem.IStatusID);
            ViewBag.ItemId = new SelectList(db.Items, "ItemId", "ISerialNum", iReturnItem.ItemId);
            ViewBag.LastUserId = new SelectList(db.VUsers, "UserID", "UserName", iReturnItem.LastUserId);
            return View(iReturnItem);
        }

        // GET: IReturnItems/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            IReturnItem iReturnItem = db.IReturnItem.Find(id);
            if (iReturnItem == null)
            {
                return HttpNotFound();
            }
            return View(iReturnItem);
        }

        // POST: IReturnItems/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            IReturnItem iReturnItem = db.IReturnItem.Find(id);
            db.IReturnItem.Remove(iReturnItem);
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

        public void setStoreUserForItem(IReturnItem ri)
        {

            var returnedItem = db.UsersItems.Where(x => x.ItemId == ri.ItemId).FirstOrDefault();

            if (returnedItem != null && returnedItem.UserId != 0)
            {
                UsersItems userItem = new UsersItems();
                userItem.ItemId = ri.ItemId;
                userItem.UserId = 0;
                userItem.CurrentStatusId = ri.IStatusID;
                userItem.currentDateTime = DateTime.Now;
                db.UsersItems.Remove(returnedItem);
                db.UsersItems.Add(userItem);

                db.SaveChanges();
            }
        }

        public void deleteFromUsersItems(int id)
        {
            var i = db.UsersItems.Find(id);
            if (i == null) //if null that means the item does not exist
            {
                var itemToDelete = db.UsersItems.FirstOrDefault(x => x.ItemId == id);
                db.UsersItems.Remove(itemToDelete);
            } //To be continued: To handle the kind of error that accurs when trying to remove an item and its not found
            return;
        }

        public void insertIntoUsersItems(int ItemId, int UserId)
        {
            UsersItems newUserItemAssos = new UsersItems();
            newUserItemAssos.ItemId = ItemId;
            newUserItemAssos.UserId = UserId;
            newUserItemAssos.currentDateTime = DateTime.Now;

            db.UsersItems.Add(newUserItemAssos);
        }

        public void addToItemHistory(IReturnItem item)
        {
            ItemHistory historyRecord = new ItemHistory();
            historyRecord.ItemId = item.ItemId;
            historyRecord.CurrentOwnerID = item.LastUserId;
            historyRecord.CurrentStatusID = item.IStatusID;
            historyRecord.CurrentDateTime = DateTime.Now;
            historyRecord.Notes = "إرجاع عهدة";

            db.ItemHistory.Add(historyRecord);
        }

        public void removeFromUsersItems(int id)
        {
            var itemToDelete = db.UsersItems.Where(i => i.ItemId == id).FirstOrDefault();

            if(itemToDelete != null)
            {
                db.UsersItems.Remove(itemToDelete);
                db.SaveChanges();
            }

        }
    }
}
