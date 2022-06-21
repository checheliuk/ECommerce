using System.Web.Mvc;

namespace ECommerce.WebUI.Controllers
{
    public class InfoController : Controller
    {
        public ActionResult Reviews()
        {
            return View();
        }

        [ActionName("shipping-and-payment")]
        public ActionResult ShippingAndPayment()
        {
            return View();
        }

        public ActionResult About()
        {
            return View();
        }

        public ActionResult Contact()
        {
            return View();
        }

        [ActionName("podarochnii-sertifikat")]
        public ActionResult Sertifikaty()
        {
            return View();
        }
    }
}