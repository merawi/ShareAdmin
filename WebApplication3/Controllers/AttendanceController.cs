using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebApplication3.Models;
//using Microsoft.AspNet.Identity;

namespace WebApplication3.Controllers
{

    [Authorize]
    public class AttendanceController : Controller
    {
        private ShareholdersAdminDB db = new ShareholdersAdminDB();

        // GET: /Attendance/
        public ActionResult Index(string SearchString)
        {

            return View(db.Shareholders.Where(s=>s.Attended==false).Where(s=>(s.ShareholderID.Equals(SearchString) || s.ShareholderName.StartsWith(SearchString)) || string.IsNullOrEmpty(SearchString)).ToList());
        }

        // GET: /Attendance/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Shareholder shareholder = db.Shareholders.Find(id);
            if (shareholder == null)
            {
                return HttpNotFound();
            }
            return View(shareholder);
        }

        // GET: /Attendance/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: /Attendance/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="ID,ShareholderID,ShareholderName,NSharesSubscribed,NSharesPaid,SerialNumbersList,Attended,Voted")] Shareholder shareholder)
        {
            if (ModelState.IsValid)
            {
                db.Shareholders.Add(shareholder);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(shareholder);
        }

        // GET: /Attendance/Edit/5
        public ActionResult Edit(int? id)
        {

            //ViewBag.user = User.Identity.GetUserName().ToString();

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Shareholder shareholder = db.Shareholders.Find(id);
            if (shareholder == null)
            {
                return HttpNotFound();
            }
            return View(shareholder);
        }

        // POST: /Attendance/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="ID,ShareholderID,ShareholderName,NSharesSubscribed,NSharesPaid,SerialNumbersList,Attended,Voted")] Shareholder shareholder)
        {
            if (ModelState.IsValid)
            {
                db.Entry(shareholder).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }

     
            return View(shareholder);
        }

        // GET: /Attendance/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Shareholder shareholder = db.Shareholders.Find(id);
            if (shareholder == null)
            {
                return HttpNotFound();
            }
            return View(shareholder);
        }

        // POST: /Attendance/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Shareholder shareholder = db.Shareholders.Find(id);
            db.Shareholders.Remove(shareholder);
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
