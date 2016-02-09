using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MyMovieDb.Models;
using Microsoft.AspNet.Identity;

namespace MyMovieDb.Controllers
{
    public class MovieReviewsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: MovieReviews
        public ActionResult Index()
        {
			var userid = User.Identity.GetUserId();
            return View(db.movieReviews.Where(x=>x.ApplicationUserId == userid).ToList());
        }

        // GET: MovieReviews/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MovieReview movieReview = db.movieReviews.Find(id);
            if (movieReview == null)
            {
                return HttpNotFound();
            }
            return View(movieReview);
        }

        // GET: MovieReviews/Create
        public ActionResult Create()
        {
			var movieModel = new MovieReViewModel
			{
				reviewList = db.reviews.ToList()
			};
            return View(movieModel);
        }

        // POST: MovieReviews/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,MovieTitle,ReviewId")] MovieReview movieReview)
        {
			movieReview.ApplicationUserId = User.Identity.GetUserId();
			movieReview.ReviewDate = DateTime.UtcNow;
            if (ModelState.IsValid)
            {
                db.movieReviews.Add(movieReview);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(movieReview);
        }

        // GET: MovieReviews/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MovieReview movieReview = db.movieReviews.Find(id);
            if (movieReview == null)
            {
                return HttpNotFound();
            }
            return View(movieReview);
        }

        // POST: MovieReviews/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,MovieTitle,ReviewId,ReviewDate,ApplicationUserId")] MovieReview movieReview)
        {
            if (ModelState.IsValid)
            {
                db.Entry(movieReview).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(movieReview);
        }

        // GET: MovieReviews/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MovieReview movieReview = db.movieReviews.Find(id);
            if (movieReview == null)
            {
                return HttpNotFound();
            }
            return View(movieReview);
        }

        // POST: MovieReviews/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            MovieReview movieReview = db.movieReviews.Find(id);
            db.movieReviews.Remove(movieReview);
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
