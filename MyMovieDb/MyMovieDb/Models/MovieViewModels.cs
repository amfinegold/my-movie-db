using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyMovieDb.Models
{
	public class MovieReViewModel
	{
		public MovieReview MovieReview { get; set; }
		public List<Review> reviewList { get; set; }
		public IEnumerable<SelectListItem> Reviews
		{
			get { return new SelectList(reviewList, "ReviewId"); }
		}
	}
}