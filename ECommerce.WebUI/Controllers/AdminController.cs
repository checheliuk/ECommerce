using ECommerce.Domain.Data;
using ECommerce.Domain.Model;
using ECommerce.Domain.ViewModel;
using ECommerce.WebUI.Filters;
using ECommerce.WebUI.Helpers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ECommerce.WebUI.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        [GetException]
        public ActionResult Orders(int? arg1)
        {
            return View(ProductData.GetOrederAdmin(new OrderParameterViewModel()
            {
                Page = arg1 ?? 1,
            }));
        }
        [GetException]
        public ActionResult Order(int arg1)
        {
            return View(ProductData.GetOrederAdminById(arg1));
        }

        [PostException]
        [HttpPost]
        public ActionResult Order(ShippingDetails shipping)
        {
            ProductData.UpdateShippingNote(shipping);
            var model = ProductData.GetOrederAdminById(shipping.ShippingDetailsID);
            model.IsSavedSuccess = true;

            return View(model);
        }

        [GetException]
        public ActionResult Goods(int? arg1)
        {
            return View(ProductData.GetProductsAdmin(new CatalogParameterViewModel()
            {
                Page = arg1 ?? 1,
            }));
        }

        [GetException]
        public ActionResult Product(int? arg1)
        {
            return View(ProductData.GetAdminProduct(arg1));
        }

        [PostException]
        [HttpPost]
        public ActionResult Product(AdminProductViewModel model, string[] categories)
        {
            var data = model;

            if(model.Product.ProductID == 0)
            {
                model.Product.Url = TransliterHelper.Front(model.Product.Title.ToLower());
            }

            data = ProductData.UpdateProduct(model, categories);
            data.IsSavedSuccess = true;
           
            return View(data);
        }

        [GetException]
        public ActionResult Photo(int arg1)
        {
            return View(ProductData.GetAdminPhotos(arg1));
        }

        [PostException]
        [HttpPost]
        public ActionResult Photo(AdminPhotoViewModel model)
        {
            model.IsSavedSuccess = true;
            ProductData.UpdatePhotos(model.Photos);
            return View(model);
        }

        [PostException]
        [HttpPost]
        public ActionResult Upload(IEnumerable<HttpPostedFileBase> files, int ProductID)
        {
            if (files.First() != null)
            {
                var list = new List<Photo>();
                var product = ProductData.GetProductById(ProductID);
                var subPath = "~/photos/" + ProductID + "/";
                var path = "/photos/" + ProductID + "/";

                bool exists = Directory.Exists(Server.MapPath(subPath));
                if (!exists)
                    Directory.CreateDirectory(Server.MapPath(subPath));

                foreach (var file in files)
                {
                    var photo = new Photo()
                    {
                        ProductID = ProductID
                    };

                    var fileName = product.Url + "-" + DateTime.Now.ToString("yyyyMMddHHMMssfff") + Path.GetExtension(file.FileName);
                    file.SaveAs(Server.MapPath(subPath + fileName));
                    photo.Path = path + fileName;

                    var img = new System.Web.Helpers.WebImage(file.InputStream);
                    photo.Width = img.Width;
                    photo.Height = img.Height;
                    int height = photo.Width == photo.Height ? 486 : photo.Width > photo.Height ? 323 : 730;
                    img.Resize(486, height, preserveAspectRatio: true, preventEnlarge: true);
                    img.Crop(1, 1, 0, 0);
                    img.Save(subPath + "thumb-" + fileName);
                    photo.Thumbnail = path + "thumb-" + fileName;

                    list.Add(photo);
                }

                ProductData.AddPhotos(list);
            }

            return RedirectToAction("photo", new { arg1 = ProductID });
        }

        [PostException]
        [HttpPost]
        public ActionResult DeletePhoto(int photoID)
        {
            DeletePhoto(ProductData.DeletePhoto(photoID));
            return Json(null);
        }

        [PostException]
        [HttpPost]
        public ActionResult SetMainPhoto(int photoID, int productID)
        {
            ProductData.SetMainPhoto(photoID, productID);
            return Json(null);
        }

        public void DeletePhoto(List<string> list)
        {
            foreach (var item in list)
            {
                var path = Server.MapPath("~" + item);
                if (System.IO.File.Exists(path))
                {
                    System.IO.File.Delete(path);
                }
            }
        }

        [GetException]
        public ActionResult Size(int arg1)
        {
            return View(ProductData.GetAdminSize(arg1));
        }

        [PostException]
        [HttpPost]
        public ActionResult Size(AdminAssignedViewModel model, int[] sizes)
        {
            foreach (var item in model.AssignedData)
            {
                item.Assigned = sizes != null && sizes.Contains(item.ID) ? true : false;
            }

            ProductData.UpdateProductSizes(model);
            model.IsSavedSuccess = true;
            return View(model);
        }

        [GetException]
        public ActionResult Color(int arg1)
        {
            return View(ProductData.GetAdminColor(arg1));
        }

        [PostException]
        [HttpPost]
        public ActionResult Color(AdminAssignedViewModel model, int[] colors)
        {
            foreach (var item in model.AssignedData)
            {
                item.Assigned = colors != null && colors.Contains(item.ID) ? true : false;
            }

            ProductData.UpdateProductColor(model);
            model.IsSavedSuccess = true;
            return View(model);
        }

        [GetException]
        public ActionResult Settings()
        {
            return View();
        }

        [GetException]
        public ActionResult TypeProducts()
        {
            return View(ProductData.GetTypeProduct());
        }

        [PostException]
        [HttpPost]
        public ActionResult UpadateTypeProduct(int typeProductID, string seoTitle, string seoDescription, string seoText)
        {
            ProductData.UpadateTypeProduct(typeProductID, seoTitle, seoDescription, seoText);
            return Json(null);
        }

        [GetException]
        public ActionResult Categories()
        {
            return View(ProductData.GetCategories());
        }

        [PostException]
        [HttpPost]
        public ActionResult UpadateCategory(int categoryID, string seoTitle, string seoDescription, string seoText)
        {
            ProductData.UpadateCategory(categoryID, seoTitle, seoDescription, seoText);
            return Json(null);
        }

        [GetException]
        public ActionResult Colors()
        {
            return View(ProductData.GetColors());
        }

        [GetException]
        public ActionResult EditColor(int? arg1)
        {
            return View(arg1 == null ? new Color() : ProductData.GetColorById((int)arg1));
        }

        [PostException]
        [HttpPost]
        public ActionResult EditColor(Color model)
        {
            ProductData.AddOrUpdateColor(model);
            return RedirectToAction("colors");
        }

        [GetException]
        public ActionResult Sizes()
        {
            return View(ProductData.GetSizes());
        }

        [GetException]
        public ActionResult EditSize(int? arg1)
        {
            return View(arg1 == null ? new Size() : ProductData.GetSizeById((int)arg1));
        }

        [PostException]
        [HttpPost]
        public ActionResult EditSize(Size model)
        {
            ProductData.AddOrUpdateSize(model);
            return RedirectToAction("sizes");
        }
    }
}