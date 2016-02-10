using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace MyMovieDb.Models
{
	public class MovieReViewModel
	{
		public MovieReview MovieReview { get; set; }

		public List<SelectListItem> Reviews { get; set; }
	}

}