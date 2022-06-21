using ECommerce.Domain.Data;
using ECommerce.Domain.Model;
using System;
using System.Web.Mvc;
using System.Web.Routing;

namespace ECommerce.WebUI.Filters
{
    public class GetExceptionAttribute : FilterAttribute, IExceptionFilter
    {
        public void OnException(ExceptionContext filterContext)
        {
            var error = new ErrorLog()
            {
                Message = filterContext.Exception.Message,
                StackTrace = filterContext.Exception.StackTrace,
                Time = DateTime.Now
            };

            ProductData.SaveErrorException(error);

            filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary( new { action = "catalog", controller = "error" }));
            filterContext.ExceptionHandled = true;
        }
    }
}