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
    public class ItemsController : Controller
    {
        private InventoryManagmentSystemEntities db = new InventoryManagmentSystemEntities();

        // GET: Items
        public ActionResult Index(string searchString)
        {
            var items = db.Items.Include(i => i.IBrand).Include(i => i.IStatus).Include(i => i.IType);

            if (!string.IsNullOrEmpty(searchString))
            {
                items = items.Where(i => i.ISerialNum.Contains(searchString));
            }

            return View(items.ToList());
        }

        // GET: Items/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Items items = db.Items.Find(id);
            if (items == null)
            {
                return HttpNotFound();
            }
            return View(items);
        }

        // GET: Items/Create
        public ActionResult Create()
        {
            ViewBag.ItemBrandId = new SelectList(db.IBrand, "BrandID", "BrandName");
            ViewBag.IStatusID = new SelectList(db.IStatus, "StatusID", "StatusName");
            ViewBag.ItemTypeId = new SelectList(db.IType, "TypeId", "ITypeName");
            ViewBag.IStore = new SelectList(db.Stores, "StoreId", "StoreName");
            return View();
        }

        // POST: Items/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ItemId,ISerialNum,ItemTypeId,ItemBrandId,IStatusID,IStore")] Items items)
        {
            if (ModelState.IsValid)
            {
                db.Items.Add(items);
                addToItemHistory(items);
                addToStoreItems(items);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ItemBrandId = new SelectList(db.IBrand, "BrandID", "BrandName", items.ItemBrandId);
            ViewBag.IStatusID = new SelectList(db.IStatus, "StatusID", "StatusName", items.IStatusID);
            ViewBag.ItemTypeId = new SelectList(db.IType, "TypeId", "ITypeName", items.ItemTypeId);
            ViewBag.IStore = new SelectList(db.Stores, "StoreId", "StoreName", items.IStore);
            return View(items);
        }

        // GET: Items/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Items items = db.Items.Find(id);
            if (items == null)
            {
                return HttpNotFound();
            }
            ViewBag.ItemBrandId = new SelectList(db.IBrand, "BrandID", "BrandName", items.ItemBrandId);
            ViewBag.IStatusID = new SelectList(db.IStatus, "StatusID", "StatusName", items.IStatusID);
            ViewBag.ItemTypeId = new SelectList(db.IType, "TypeId", "ITypeName", items.ItemTypeId);
            return View(items);
        }

        // POST: Items/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ItemId,ISerialNum,ItemTypeId,ItemBrandId,IStatusID")] Items items)
        {
            if (ModelState.IsValid)
            {
                db.Entry(items).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ItemBrandId = new SelectList(db.IBrand, "BrandID", "BrandName", items.ItemBrandId);
            ViewBag.IStatusID = new SelectList(db.IStatus, "StatusID", "StatusName", items.IStatusID);
            ViewBag.ItemTypeId = new SelectList(db.IType, "TypeId", "ITypeName", items.ItemTypeId);
            return View(items);
        }

        // GET: Items/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Items items = db.Items.Find(id);
            if (items == null)
            {
                return HttpNotFound();
            }
            return View(items);
        }

        // POST: Items/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Items items = db.Items.Find(id);
            db.Items.Remove(items);
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
            var allItems = from i in db.Items select i;

            if (!string.IsNullOrEmpty(searchString))
            {
                allItems = allItems.Where(i => i.ISerialNum.Contains(searchString));
            }

            return View(allItems);
        }

        public void addToStoreItems(Items item)
        {
            var storeItemsRecord = new StoreItems();
            storeItemsRecord.StoreId = (int)item.IStore;
            storeItemsRecord.ItemId = item.ItemId;
            db.StoreItems.Add(storeItemsRecord);
            db.SaveChanges();
        }

        public void addToItemHistory(Items item)
        {
            ItemHistory historyRecord = new ItemHistory();
            historyRecord.ItemId = item.ItemId;
            historyRecord.CurrentOwnerID = 100;
            historyRecord.CurrentStatusID = item.IStatusID;
            historyRecord.CurrentDateTime = DateTime.Now;
            historyRecord.Notes = "إضافة عنصر جديد";
            db.ItemHistory.Add(historyRecord);
            db.SaveChanges();
        }
    }
}
