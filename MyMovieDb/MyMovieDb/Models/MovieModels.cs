using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MyMovieDb.Models
{
	public class MovieReview
	{
		public int Id { get; set; }

		[Display(Name="Movie Title")]
		public string MovieTitle { get; set; }

		[Display(Name ="My Rating")]
		public int ReviewId { get; set; }
		public DateTime ReviewDate { get; set; }

		public string ApplicationUserId { get; set; }
	}

	public class Review
	{
		public int Id { get; set; }
		public string Name { get; set; }

		public virtual ICollection<MovieReview> MovieReviews { get; set; }
	}
}