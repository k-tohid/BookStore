﻿using Microsoft.AspNetCore.Mvc;

namespace BookStore.UI.Areas.Admin.Controllers
{
	[Area("Admin")]
	public class DashboardController : Controller
	{
		[Route("/Admin/")]
		[Route("/Admin/Dashboard")]
		[HttpGet]
		public IActionResult Index()
		{
			return View();
		}
	}
}
