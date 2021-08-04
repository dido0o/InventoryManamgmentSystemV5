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
    public class VUsersController : Controller
    {
        private InventoryManagmentSystemEntities db = new InventoryManagmentSystemEntities();

        // GET: VUsers
        public ActionResult Index(string searchString)
        {
            var vUsers = db.VUsers.Include(v => v.Agency);

            if (!string.IsNullOrEmpty(searchString))
            {
                vUsers = vUsers.Where(i => i.UserName.Contains(searchString));
            }

            return View(vUsers.ToList());
        }

        // GET: VUsers/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VUsers vUsers = db.VUsers.Find(id);
            if (vUsers == null)
            {
                return HttpNotFound();
            }
            return View(vUsers);
        }

        // GET: VUsers/Create
        public ActionResult Create()
        {
            ViewBag.AgencyId = new SelectList(db.Agency, "AgencyID", "AgencyName");
            return View();
        }

        // POST: VUsers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "UserID,UserName,AgencyId")] VUsers vUsers)
        {
            if (ModelState.IsValid)
            {
                db.VUsers.Add(vUsers);
                addToAgencyUserMapping(vUsers);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.AgencyId = new SelectList(db.Agency, "AgencyID", "AgencyName", vUsers.AgencyId);
            return View(vUsers);
        }

        // GET: VUsers/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VUsers vUsers = db.VUsers.Find(id);
            if (vUsers == null)
            {
                return HttpNotFound();
            }
            ViewBag.AgencyId = new SelectList(db.Agency, "AgencyID", "AgencyName", vUsers.AgencyId);
            return View(vUsers);
        }

        // POST: VUsers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "UserID,UserName,AgencyId")] VUsers vUsers)
        {
            if (ModelState.IsValid)
            {
                db.Entry(vUsers).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.AgencyId = new SelectList(db.Agency, "AgencyID", "AgencyName", vUsers.AgencyId);
            return View(vUsers);
        }

        // GET: VUsers/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VUsers vUsers = db.VUsers.Find(id);
            if (vUsers == null)
            {
                return HttpNotFound();
            }
            return View(vUsers);
        }

        // POST: VUsers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            VUsers vUsers = db.VUsers.Find(id);
            db.VUsers.Remove(vUsers);
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

        public ActionResult Report(String searchString)
        {

            var allUsers = db.VUsers;

            if (!String.IsNullOrEmpty(searchString))
            {
                
            }
            return View(allUsers.ToList());
        }

        public void addToAgencyUserMapping(VUsers vUser)
        {
            AgencyUserMapping newRecord = new AgencyUserMapping();
            newRecord.UserId = vUser.UserID;
            newRecord.AgencyId = vUser.AgencyId;

            db.AgencyUserMapping.Add(newRecord);
        }
    }
}
