using ECommerce.Domain.Data;
using ECommerce.Domain.Model;
using System;
using System.Collections.Generic;
using System.Web.Mvc;
using System.Xml.Linq;

namespace ECommerce.WebUI.Helpers
{
    public class SitemapGenerator
    {
        public IReadOnlyCollection<string> GetSitemapNodes(UrlHelper urlHelper)
        {
            List<string> nodes = new List<string>();

            nodes.Add(urlHelper.AbsoluteRouteUrl("Default", new { controller = "catalog", action = "grid" }));
            nodes.Add(urlHelper.AbsoluteRouteUrl("Catalog-Type-Sex-Category-Page", new { controller = "catalog", action = "grid", arg1 = "woman", arg2 = "nizhneye-bele", arg3 = "komplekty" }));
            nodes.Add(urlHelper.AbsoluteRouteUrl("Catalog-Type-Sex-Category-Page", new { controller = "catalog", action = "grid", arg1 = "woman", arg2 = "nizhneye-bele", arg3 = "byustgaltery-push-up" }));
            nodes.Add(urlHelper.AbsoluteRouteUrl("Catalog-Type-Sex-Category-Page", new { controller = "catalog", action = "grid", arg1 = "woman", arg2 = "nizhneye-bele", arg3 = "trusy" }));
            nodes.Add(urlHelper.AbsoluteRouteUrl("Catalog-Type-Sex-Category-Page", new { controller = "catalog", action = "grid", arg1 = "woman", arg2 = "topy" }));
            nodes.Add(urlHelper.AbsoluteRouteUrl("Catalog-Type-Sex-Category-Page", new { controller = "catalog", action = "grid", arg1 = "woman", arg2 = "pizhami-s-shortami" }));
            nodes.Add(urlHelper.AbsoluteRouteUrl("Catalog-Type-Sex-Category-Page", new { controller = "catalog", action = "grid", arg1 = "woman", arg2 = "kolgoty" }));
            nodes.Add(urlHelper.AbsoluteRouteUrl("Catalog-Type-Sex-Category-Page", new { controller = "catalog", action = "grid", arg1 = "woman", arg2 = "nosochki" }));
            nodes.Add(urlHelper.AbsoluteRouteUrl("Default", new { controller = "catalog", action = "cart" }));
            nodes.Add(urlHelper.AbsoluteRouteUrl("Default", new { controller = "catalog", action = "checkout" }));
            nodes.Add(urlHelper.AbsoluteRouteUrl("Default", new { controller = "catalog", action = "message" }));
            nodes.Add(urlHelper.AbsoluteRouteUrl("Default", new { controller = "catalog", action = "error" }));
            nodes.Add(urlHelper.AbsoluteRouteUrl("Default", new { controller = "info", action = "about" }));
            nodes.Add(urlHelper.AbsoluteRouteUrl("Default", new { controller = "info", action = "shipping-and-payment" }));
            nodes.Add(urlHelper.AbsoluteRouteUrl("Default", new { controller = "info", action = "reviews" }));
            nodes.Add(urlHelper.AbsoluteRouteUrl("Default", new { controller = "info", action = "contact" }));

            foreach (var product in ProductData.GetProducts())
            {
                nodes.Add(urlHelper.AbsoluteRouteUrl("Catalog-Type-Sex-Category-Page", new { controller = "catalog", action = "details", arg1 = product.Url, arg2 = product.ProductID }));
            }

            return nodes;
        }

        public string GetSitemapDocument(IEnumerable<string> sitemapNodes)
        {
            XNamespace xmlns = "http://www.sitemaps.org/schemas/sitemap/0.9";
            XElement root = new XElement(xmlns + "urlset");

            foreach (string sitemapNode in sitemapNodes)
            {
                XElement urlElement = new XElement(
                    xmlns + "url",
                    new XElement(xmlns + "loc", Uri.EscapeUriString(sitemapNode)));
                root.Add(urlElement);
            }

            XDocument document = new XDocument(root);
            return document.ToString();
        }
    }
    public static class UrlHelperExtensions
    {
        public static string AbsoluteRouteUrl(this UrlHelper urlHelper,
            string routeName, object routeValues = null)
        {
            string scheme = urlHelper.RequestContext.HttpContext.Request.Url.Scheme;
            return urlHelper.RouteUrl(routeName, routeValues, scheme);
        }
    }
}