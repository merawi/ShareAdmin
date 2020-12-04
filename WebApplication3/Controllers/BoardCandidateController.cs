using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebApplication3.Models;

namespace WebApplication3.Controllers
{
    public class BoardCandidateController : Controller
    {
        private ShareholdersAdminDB db = new ShareholdersAdminDB();

        // GET: /BoardCandidate/
        public ActionResult Index()
        {
            return View(db.BoardCandidates.ToList());
        }

        // GET: /BoardCandidate/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BoardCandidate boardcandidate = db.BoardCandidates.Find(id);
            if (boardcandidate == null)
            {
                return HttpNotFound();
            }
            return View(boardcandidate);
        }
        
        [Authorize(Roles="admin")]
        // GET: /BoardCandidate/Create
        public ActionResult Create()
        {
           return View();
        }

        // POST: /BoardCandidate/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "admin")]
        public ActionResult Create([Bind(Include="ID,CandidateNo,CandidateName")] BoardCandidate boardcandidate)
        {
            if (ModelState.IsValid)
            {
                db.BoardCandidates.Add(boardcandidate);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(boardcandidate);
        }

        // GET: /BoardCandidate/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BoardCandidate boardcandidate = db.BoardCandidates.Find(id);
            if (boardcandidate == null)
            {
                return HttpNotFound();
            }
            return View(boardcandidate);
        }

        // POST: /BoardCandidate/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="ID,CandidateNo,CandidateName")] BoardCandidate boardcandidate)
        {
            if (ModelState.IsValid)
            {
                db.Entry(boardcandidate).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(boardcandidate);
        }

        // GET: /BoardCandidate/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BoardCandidate boardcandidate = db.BoardCandidates.Find(id);
            if (boardcandidate == null)
            {
                return HttpNotFound();
            }
            return View(boardcandidate);
        }

        // POST: /BoardCandidate/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            BoardCandidate boardcandidate = db.BoardCandidates.Find(id);
            db.BoardCandidates.Remove(boardcandidate);
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
