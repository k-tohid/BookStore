using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace BookStore.UI.Filters.ActionFilters
{
	public class RedirectAuthenticatedFilter : ActionFilterAttribute
	{
		public override void OnActionExecuting(ActionExecutingContext context)
		{

			if (context.HttpContext.User?.Identity?.IsAuthenticated == true)
			{
				context.Result = new RedirectToActionResult("Index", "Home", null);
			}

			base.OnActionExecuting(context);
		}
	}
}
