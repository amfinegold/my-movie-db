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
	[Authorize]
	public class MovieReviewsController : Controller
	{
		private ApplicationDbContext db = new ApplicationDbContext();

		// GET: MovieReviews
		public ActionResult Index(string sortOrder, string searchString)
		{
			ViewBag.TitleSortParm = string.IsNullOrEmpty(sortOrder) ? "title_desc" : "";
			ViewBag.RatingSortParm = sortOrder == "rating" ? "rating_desc" : "rating";
			ViewBag.DateSortParm = sortOrder == "date" ? "date_desc" : "date";

			var userid = User.Identity.GetUserId();
			var movies = db.movieReviews.Where(x => x.ApplicationUserId == userid);

			if (!string.IsNullOrEmpty(searchString))
			{
				movies = movies.Where(m => m.MovieTitle.Contains(searchString));
			}

			switch (sortOrder)
			{
				case "title_desc":
					movies = movies.OrderByDescending(m => m.MovieTitle);
					break;
				case "rating":
					movies = movies.OrderBy(m => m.RatingId);
					break;
				case "rating_desc":
					movies = movies.OrderByDescending(m => m.RatingId);
					break;
				case "date":
					movies = movies.OrderBy(m => m.ReviewDate);
					break;
				case "date_desc":
					movies = movies.OrderByDescending(m => m.ReviewDate);
					break;
				default:
					movies = movies.OrderBy(m => m.MovieTitle);
					break;
			}

			return View(movies.ToList());
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
				ViewBag.Message = "We couldn't find what you were looking for. <br />"
					+ " Click <a href=\" / MovieReviews\">here to go back to your movie ratings</a>.";
				return View("Info");
			}
			if(movieReview.ApplicationUserId != User.Identity.GetUserId())
			{
				return RedirectToAction("Index");
			}
			
			return View(movieReview);
		}

		// GET: MovieReviews/Create
		public ActionResult Create()
		{
			var movieModel = new MovieReViewModel();
			movieModel.Reviews = GetReviews();
			return View(movieModel);
		}

		// POST: MovieReviews/Create
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Create(MovieReViewModel movieModel)
		{
			movieModel.MovieReview.ApplicationUserId = User.Identity.GetUserId();
			movieModel.MovieReview.ReviewDate = DateTime.Now;
			if (ModelState.IsValid)
			{
				var alreadyExists = db.movieReviews.Where(
					x => x.MovieTitle == movieModel.MovieReview.MovieTitle
					&& x.ApplicationUserId == movieModel.MovieReview.ApplicationUserId).ToList();
				if (alreadyExists.Count == 0 || alreadyExists == null)
				{
					db.movieReviews.Add(movieModel.MovieReview);
					db.SaveChanges();
					return RedirectToAction("Index");
				}
				else
				{
					movieModel.Errors = new Dictionary<string, string>();
					movieModel.Errors.Add("CreateMovieReview", "You've already rated this movie. Try a different title.");
					movieModel.Reviews = new List<SelectListItem>();
					movieModel.Reviews = GetReviews();
					return View(movieModel);
				}
			}
			return View(movieModel);
		}

		// GET: MovieReviews/Edit/5
		public ActionResult Edit(int? id)
		{
			if (id == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}
			var movieReview = db.movieReviews.Find(id);
			if(movieReview.ApplicationUserId != User.Identity.GetUserId())
			{
				return RedirectToAction("Index");
			}
			var movieModel = new MovieReViewModel();
			movieModel.Reviews = GetReviews();
			movieModel.MovieReview = movieReview;

			if (movieModel.MovieReview == null)
			{
				ViewBag.Message = "We couldn't find what you were looking for. <br />"
					+ " Click <a href=\" / MovieReviews\">here to go back to your movie ratings</a>.";
				return View("Info");
			}
			return View(movieModel);
		}

		// POST: MovieReviews/Edit/5
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Edit(MovieReViewModel movieModel)
		{
			if (ModelState.IsValid)
			{
				var userId = User.Identity.GetUserId();
				var alreadyExists = db.movieReviews.Where(
					x => x.MovieTitle == movieModel.MovieReview.MovieTitle
					&& x.ApplicationUserId == userId).ToList();
				if (alreadyExists.Count > 0 && alreadyExists.First().ApplicationUserId != userId)
				{
					return RedirectToAction("Index");
				}
				if ((alreadyExists.Count > 0 && alreadyExists.First().Id == movieModel.MovieReview.Id) 
					|| alreadyExists.Count ==0)
				{
					var updated = db.movieReviews.Find(movieModel.MovieReview.Id);
					updated.MovieTitle = movieModel.MovieReview.MovieTitle;
					updated.RatingId = movieModel.MovieReview.RatingId;
					updated.ReviewDate = DateTime.Now;

					db.Entry(updated).State = EntityState.Modified;
					db.SaveChanges();
				}
				else
				{
					movieModel.Errors = new Dictionary<string, string>();
					movieModel.Errors.Add("EditMovieReview", "You've already rated this movie. Try a different title.");
					movieModel.Reviews = GetReviews();
					return View(movieModel);
				}
				return RedirectToAction("Index");
			}

			return View(movieModel);
		}

		// GET: MovieReviews/Delete/5
		public ActionResult Delete(int? id)
		{
			if (id == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}

			MovieReview movieReview = db.movieReviews.Find(id);
			var userId = User.Identity.GetUserId();
			if(User.Identity.GetUserId() != movieReview.ApplicationUserId)
			{
				return RedirectToAction("Index");
			}
			if (movieReview == null)
			{
				ViewBag.Message = ViewBag.Message = "We couldn't find what you were looking for. <br />"
					+ " Click <a href=\" / MovieReviews\">here to go back to your movie ratings.</a>";
				return View("Info");
			}
			return View(movieReview);
		}

		// POST: MovieReviews/Delete/5
		[HttpPost, ActionName("Delete")]
		[ValidateAntiForgeryToken]
		public ActionResult DeleteConfirmed(int id)
		{
			MovieReview movieReview = db.movieReviews.Find(id);

			if (User.Identity.GetUserId() == movieReview.ApplicationUserId)
			{
				db.movieReviews.Remove(movieReview);
				db.SaveChanges();
			}
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

		private List<SelectListItem> GetReviews()
		{
			var selectList = new List<SelectListItem>();
			selectList.Add(new SelectListItem { Text = "Rate your movie!", Value = "0", Selected = true });
			foreach (var rev in db.reviews)
			{
				selectList.Add(new SelectListItem { Text = rev.Id + "-" + rev.Name, Selected = false, Value = rev.Id.ToString() });
			}
			return selectList;
		}
	}
}
