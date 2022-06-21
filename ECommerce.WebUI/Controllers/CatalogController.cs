using ECommerce.Domain.ConstantsData;
using ECommerce.Domain.CookiesModel;
using ECommerce.Domain.Data;
using ECommerce.Domain.Model;
using ECommerce.Domain.ViewModel;
using ECommerce.WebUI.Filters;
using ECommerce.WebUI.Helpers;
using System.Web.Mvc;

namespace ECommerce.WebUI.Controllers
{
    public class CatalogController : Controller
    {
        private Cart GetCart()
        {
            var cart = (Cart)Session[SiteConstants.SessionCarKey];
            if (cart == null)
            {
                var helper = new UserCookiesHelper();
                var getCookies = helper.Get(SiteConstants.UserCookies);

                if (string.IsNullOrEmpty(getCookies.UserID))
                {
                    var setCookies = helper.CreateCookies(SiteConstants.UserCookies);
                    Response.Cookies.Add(setCookies.Cookie);
                    cart = new Cart(ProductData.CreateShipping(setCookies.Model.UserID));
                }
                else
                {
                    cart = new Cart(ProductData.GetShippingByUserId(getCookies.UserID));
                }

                Session[SiteConstants.SessionCarKey] = cart;
            }

            return cart;
        }

        [GetException]
        public ActionResult Grid(string arg1, string arg2, string arg3, string arg4, int? arg5)
        {
            return View(ProductData.GetProducts(new CatalogParameterViewModel()
            {
                Sex = arg1?.ToLower() ?? "all",
                Type = arg2?.ToLower() ?? "all",
                Category = arg3?.ToLower() ?? "all",
                Order = arg4?.ToLower() ?? "popularity",
                Page = arg5 ?? 1,
                View = "grid"
            }));
        }

        [GetException]
        public ActionResult List(string arg1, string arg2, string arg3, string arg4, int? arg5)
        {
            return View(ProductData.GetProducts(new CatalogParameterViewModel()
            {
                Sex = arg1?.ToLower() ?? "all",
                Type = arg2?.ToLower() ?? "all",
                Category = arg3?.ToLower() ?? "all",
                Order = arg4?.ToLower() ?? "popularity",
                Page = arg5 ?? 1,
                View = "list"
            }));
        }

        [GetException]
        public ActionResult Details(string arg1, int arg2)
        {
            var model = ProductData.GetProduct(arg2);
            if (model.Photos == null)
                return RedirectToAction("error");

            return View(model);
        }

        public ViewResult Summary(Cart cart)
        {
            return View(cart);
        }

        [GetException]
        public ActionResult Cart(Cart cart)
        {
            return View(cart);
        }

        [GetException]
        public ActionResult Checkout(Cart cart)
        {
            cart.ComputeTotalValue();
            var model = cart.GetModel();
            if (model.Total == 0)
                return RedirectToAction("grid");

            ViewBag.Area = ProductData.GetAddressArea();
            return View(model);
        }

        [PostException] 
        [HttpPost]
        public ActionResult Checkout(ShippingDetails shipping, Cart cart)
        {
            cart.ComputeTotalValue();
            var data = cart.GetModel();
            if (!string.IsNullOrEmpty(shipping.FirstName) && !string.IsNullOrEmpty(shipping.LastName) && !string.IsNullOrEmpty(shipping.Phone) && !string.IsNullOrEmpty(shipping.Town) 
                && data.shipping.ShippingDetailsID != 0 && shipping.AddressAreaID != 0 && shipping.Delivery != 0 && shipping.Payment != 0)
            {
                if ((shipping.Delivery == EnumModel.DeliveryService.NovaPoshta && !string.IsNullOrEmpty(shipping.Warehouses)) 
                    || (shipping.Delivery == EnumModel.DeliveryService.UkrPoshta && !string.IsNullOrEmpty(shipping.ZipCode) && !string.IsNullOrEmpty(shipping.Address)))
                {
                    shipping.ShippingDetailsID = data.shipping.ShippingDetailsID;
                    shipping.Total = data.Total;
                    ProductData.UpdateShipping(shipping);
                    Session.Remove(SiteConstants.SessionCarKey);

                    var products = " Заказ: ";
                    foreach (var item in data.products)
                    {
                        products += "[ " + item.Product.Title + " " + item.Quantity + "шт. Размер: " + item.Size.Title + " Цвет: " + item.Color.Title + " ] "; 
                    }

                    HelperCommon.SendMessageToTelegram("Имя: [ " + shipping.FirstName + " " + shipping.LastName +
                                                       " ] Телефон: [ " + shipping.Phone +
                                                       " ] Cлужба доставки: [ " + HelperCommon.GetDescription(shipping.Delivery) +
                                                       " ] Метод оплаты: [ " + HelperCommon.GetDescription(shipping.Payment) +
                                                       " ] Область: [ " + ProductData.GetAddressAreaById(shipping.AddressAreaID)?.Description +
                                                       " ] Город: [ " + shipping.Town +
                                                       (shipping.Delivery == EnumModel.DeliveryService.UkrPoshta ? " ] Почтовый индекс: [ " + shipping.ZipCode + " ] Адрес: [ " + shipping.Address : "") +
                                                       (shipping.Delivery == EnumModel.DeliveryService.NovaPoshta ? " ] Отделения новой почты: [ " + shipping.Warehouses : "") +
                                                       products +
                                                       " ] Сумма заказа: [ " + shipping.Total + " грн. ]");
                    return View("message");
                }
            }

            data.shipping.FirstName = shipping.FirstName;
            data.shipping.LastName = shipping.LastName;
            data.shipping.Phone = shipping.Phone;
            data.shipping.Delivery = shipping.Delivery;
            data.shipping.Payment =  shipping.Payment;
            data.shipping.AddressAreaID = shipping.AddressAreaID;
            data.shipping.Town = shipping.Town;
            data.shipping.Address = shipping.Address;
            data.shipping.ZipCode = shipping.ZipCode;
            data.shipping.Warehouses = shipping.Warehouses;

            ViewBag.Area = ProductData.GetAddressArea();
            return View(data);
        }

        [PostException]
        [HttpPost]
        public ActionResult AddToCart(int productID, int count = 1, int sizeID = 1, int colorID = 1)
        {
            var product = ProductData.GetProductById(productID);

            if (product != null)
            {
                GetCart().AddItem(product, count, sizeID, colorID);
            }

            return Json(null);
        }

        [PostException]
        [HttpPost]
        public ActionResult RemoveFromCart(int productShippingID)
        {
            GetCart().RemoveLine(productShippingID);
            return Json(null);
        }

        [PostException]
        [HttpPost]
        public ActionResult RemoveAllFromCart()
        {
            GetCart().RemoveAllLine();
            return Json(null);
        }

        public ActionResult Error()
        {
            return View();
        }

        [GetException]
        public ActionResult Message()
        {
            return View();
        }
    }
}