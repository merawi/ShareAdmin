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
    [Authorize]
    public class ShareholderController : Controller
    {
        private ShareholdersAdminDB db = new ShareholdersAdminDB();


        // GET: /Shareholder/
        public ActionResult Index(int? ID, string searchTerm = null)
        {
           List<Shareholder> shareholdersList = null;
           if (ID==null)
           {
               shareholdersList =
               db.Shareholders.Where(s => s.ShareholderName.StartsWith(searchTerm) || string.IsNullOrEmpty(searchTerm))
               .OrderBy(s => s.ID)
               .ToList();

               return View(shareholdersList);
           }

           else if (searchTerm == null)

               return RedirectToAction("Details", db.Shareholders.Find(ID));
           else
               return View();
        }
        
        public ActionResult Search(string searchTerm = null)
        {
            //List<Shareholder> shareholdersList = db.Shareholders.ToList();

            //List<Shareholder> shareholdersList = null;

            List<Shareholder> shareholdersList =
            db.Shareholders.Where(s => s.ShareholderName.StartsWith(searchTerm) || string.IsNullOrEmpty(searchTerm))
            .OrderBy(s => s.ID)
            .ToList();

            return RedirectToAction("Deatail", shareholdersList.First());
        }

        // GET: /Shareholder/Details/5
 //       public ActionResult Details(int? id)
public ActionResult Details(string shareholderID)
        {

            if (shareholderID == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Shareholder shareholder = db.Shareholders.Where(s => s.ShareholderID == shareholderID).First();
                //db.Shareholders.Find(shareholderID);
            if (shareholder == null)
            {
                //return HttpNotFound();
                return View();
            }
            return View(shareholder);
        }

        // GET: /Shareholder/Create
        public ActionResult Create()
        {
            return View();
        }



        // POST: /Shareholder/Create
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

        // GET: /Shareholder/Edit/5
        public ActionResult Edit(int? id)
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

        // POST: /Shareholder/Edit/5
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

        // GET: /Shareholder/Delete/5
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

        // POST: /Shareholder/Delete/5
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
