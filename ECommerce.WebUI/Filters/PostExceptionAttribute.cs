using ECommerce.Domain.Data;
using ECommerce.Domain.Model;
using System;
using System.Web.Mvc;

namespace ECommerce.WebUI.Filters
{
    public class PostExceptionAttribute : FilterAttribute, IExceptionFilter
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
        }
    }
}