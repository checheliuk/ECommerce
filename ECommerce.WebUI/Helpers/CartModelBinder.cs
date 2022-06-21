using ECommerce.Domain.ConstantsData;
using ECommerce.Domain.CookiesModel;
using ECommerce.Domain.Data;
using System.Web;
using System.Web.Mvc;

namespace ECommerce.WebUI.Helpers
{
    public class CartModelBinder : IModelBinder
    {
        public object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            var cart = (Cart)controllerContext.HttpContext.Session[SiteConstants.SessionCarKey];

            if (cart == null)
            {
                var helper = new UserCookiesHelper();
                var getCookies = helper.Get(SiteConstants.UserCookies);

                if (string.IsNullOrEmpty(getCookies.UserID))
                {
                    var setCookies = helper.CreateCookies(SiteConstants.UserCookies);
                    HttpContext.Current.Response.Cookies.Add(setCookies.Cookie);
                    cart = new Cart(ProductData.CreateShipping(setCookies.Model.UserID));
                }
                else
                {
                    cart = new Cart(ProductData.GetShippingByUserId(getCookies.UserID));
                }

                controllerContext.HttpContext.Session[SiteConstants.SessionCarKey] = cart;
            }

            return cart;
        }
    }
}