using ECommerce.Domain.Data;
using ECommerce.WebUI.Filters;
using ECommerce.WebUI.Helpers;
using System.Text;
using System.Web.Mvc;

namespace ECommerce.WebUI.Controllers
{
    public class PostController : Controller
    {
        [PostException]
        [HttpPost]
        public ActionResult Email(string email)
        {
            ProductData.AddEmail(email);
            return Json(null);
        }

        [GetException]
        public ActionResult Sitemap()
        {
            var sitemapGenerator = new SitemapGenerator();
            var sitemapNodes = sitemapGenerator.GetSitemapNodes(Url);
            string xml = sitemapGenerator.GetSitemapDocument(sitemapNodes);
            return Content(xml, "text/xml", Encoding.UTF8);
        }
    }
}