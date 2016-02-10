using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyMovieDb.Controllers
{
	public class HomeController : Controller
	{
		public ActionResult Index()
		{
			return View();
		}

		public ActionResult About()
		{
			ViewBag.Message = "About Us.";

			return View();
		}

		public ActionResult Contact()
		{
			ViewBag.Message = "Here's how you can get in touch.";

			return View();
		}

		public ActionResult Help()
		{
			return View();
		}
	}
}