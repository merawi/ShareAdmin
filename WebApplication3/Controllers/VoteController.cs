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
    [Authorize(Roles="user,dmin")]
    public class VoteController : Controller
    {
        private ShareholdersAdminDB db = new ShareholdersAdminDB();

        // GET: /Vote/
        public ActionResult Index(string SearchString)
        {
            return View(db.Shareholders.Where(s=>s.Attended && !s.Voted).Where(s=>(s.ShareholderID.Equals(SearchString) || s.ShareholderName.StartsWith(SearchString)) || string.IsNullOrEmpty(SearchString)).ToList());
        }

        // GET: /Vote/Details/5
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

        // GET: /Vote/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: /Vote/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="ID,ShareholderID,ShareholderName,ShareholderNameAmh,NSharesSubscribed,NSharesPaid,SerialNumbersList,Attended,Voted")] Shareholder shareholder)
        {
            if (ModelState.IsValid)
            {
                db.Shareholders.Add(shareholder);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(shareholder);
        }

        // GET: /Vote/Edit/5
        public ActionResult Edit(int? id)
        {

            List<BoardCandidate> candidates = new List<BoardCandidate>();

            foreach(BoardCandidate item in db.BoardCandidates.ToList())
            {
                item.selected = false;
            }

            ViewBag.candidates = candidates;

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

        [HttpGet]
        public ActionResult VoteEdit(int? id)
        {
            Shareholder shareholder = db.Shareholders.Find(id);

            ViewBag.ID = id;
            ViewBag.ShareholderID = shareholder.ShareholderID;
            ViewBag.ShareholderName  = shareholder.ShareholderName;
            ViewBag.ShareholderNameAmh = shareholder.ShareholderNameAmh;

            List<BoardCandidate> candidates = db.BoardCandidates.ToList();
            foreach (BoardCandidate b in candidates)
            {
                b.selected = false;
            }

            return View(db.BoardCandidates.ToList());
        }


        [HttpPost]
        public ActionResult VoteEditSave(IEnumerable<BoardCandidate> Candidates)
        {

            Shareholder shareholder = db.Shareholders.Find(int.Parse(Request.Form["ShareholderId"]));

            foreach(BoardCandidate b in Candidates)
            {
                if (b.selected) shareholder.ElectedCandidates.Add(b);
            }

            shareholder.VoteRegisterer = User.Identity.Name;

            db.Entry(shareholder).State = EntityState.Modified;
            db.SaveChanges();


            //return Candidates.Count(x => x.selected).ToString() + "--" + shareholder.ShareholderNameAmh; 

            return RedirectToAction("Index");
        }


        

        // POST: /Vote/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="ID,ShareholderID,ShareholderName,ShareholderNameAmh,NSharesSubscribed,NSharesPaid,SerialNumbersList,Attended,Voted")] Shareholder shareholder)
        {
            if (ModelState.IsValid)
            {
                db.Entry(shareholder).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(shareholder);
        }

        // GET: /Vote/Delete/5
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

        // POST: /Vote/Delete/5
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
