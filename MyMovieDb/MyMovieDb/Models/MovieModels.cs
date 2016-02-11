using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace MyMovieDb.Models
{
	public enum ReviewEnum
	{
		VeryBad = 1, Bad, Ehh, Good, VeryGood
	}
	public class MovieReview
	{
		public int Id { get; set; }

		[Display(Name="Movie Title")]
		[Required(AllowEmptyStrings = false, ErrorMessage = "Titles need at least one character, preferably a well-rounded one.")]
		public string MovieTitle { get; set; }

		[Display(Name ="My Rating")]
		[Range(1, 5, ErrorMessage = "You've got to rate this movie!")]
		public int RatingId { get; set; }

		[Display(Name ="Reviewed On")]
		[DisplayFormat(DataFormatString = "{0: d MMM. yyyy}")]
		public DateTime ReviewDate { get; set; }

		[ReadOnly(true)]
		public string ApplicationUserId { get; set; }
	}

	public class Review
	{
		public int Id { get; set; }
		public string Name { get; set; }

		public virtual ICollection<MovieReview> MovieReviews { get; set; }
	}
}