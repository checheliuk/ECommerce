using ECommerce.Domain.ConstantsData;
using ECommerce.Domain.Model;
using ECommerce.Domain.ViewModel;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using static ECommerce.Domain.Model.EnumModel;

namespace ECommerce.Domain.Data
{
    public static class ProductData
    {
        public static List<Product> GetProducts()
        {
            using (var db = new ApplicationDbContext())
            {
                return db.Products.ToList();
            }
         }

        public static HomeCatalogViewModels GetProducts(CatalogParameterViewModel parameters)
        {
            using (var db = new ApplicationDbContext())
            {
                string title = string.Empty;
                string seoTitle = string.Empty;
                string seoDescription = string.Empty;
                string seoText = string.Empty;

                var products = db.Products.Where(x => x.Status == ProductStatus.Available || x.Status == ProductStatus.Awaiting || x.Status == ProductStatus.NotAvailable);
                if (parameters.Type != "all" && !String.IsNullOrEmpty(parameters.Type))
                {
                    products = products.Where(x => x.TypeProduct.Url == parameters.Type);
                    var type = db.TypeProduct.FirstOrDefault(x => x.Url == parameters.Type);
                    if(type != null)
                    {
                        seoTitle = type.SeoTitle;
                        seoDescription = type.SeoDescription;
                        seoText = type.SeoText;
                        title = type.Title;
                    }
                }
               
                if (parameters.Category != "all" && !String.IsNullOrEmpty(parameters.Category))
                {
                    products = products.Where(x => x.Categories.Any(y => y.Url == parameters.Category));
                    var category = db.Categories.FirstOrDefault(x => x.Url == parameters.Category);
                    if (category != null)
                    {
                        seoTitle = category.SeoTitle;
                        seoDescription = category.SeoDescription;
                        seoText = category.SeoText;
                        title = category.Title;
                    }
                }

                if (String.IsNullOrEmpty(title))
                    title = Resources.Resource.Catalog;


                if (parameters.Sex != "all" && !String.IsNullOrEmpty(parameters.Sex))
                {
                    try
                    {
                        var sex = (Sex)Enum.Parse(typeof(Sex), parameters.Sex);
                        products = products.Where(x => x.Sex == sex);
                    }
                    catch
                    {
                        return new HomeCatalogViewModels() {
                            Products = new List<Product>(),
                            Page = 1,
                            LastPage = 1,
                            Type = parameters.Type,
                            Sex = parameters.Sex,
                            Category = parameters.Category,
                            Title = title,
                            SeoDescription = seoDescription,
                            SeoTitle = seoTitle,
                            SeoText = seoText,
                            LowPrice = 0,
                            HighPrice = 0,
                            Count = 0,
                            View = parameters.View
                        };
                    }
                }

                List<Product> data;
                switch (parameters.Order)
                {
                    case "lowtohigh":
                        data = products.OrderBy(p => p.Price).ToList();
                        break;
                    case "hightolow":
                        data = products.OrderByDescending(p => p.Price).ToList();
                        break;
                    default:
                        data = products.OrderByDescending(p => p.ProductID).ToList();
                        //data = products.OrderBy(p => p.Order ?? int.MaxValue).ThenBy(p => p.ProductID).ToList();
                        break;
                }

                return new HomeCatalogViewModels() {
                    Products = data.Skip(((int)parameters.Page - 1) * SiteConstants.PageSize).Take(SiteConstants.PageSize).ToList(),
                    Page = parameters.Page ?? 1,
                    LastPage = (int)Math.Ceiling((double)data.Count / SiteConstants.PageSize),
                    Type = parameters.Type,
                    Sex = parameters.Sex,
                    Category = parameters.Category,
                    Title = title,
                    SeoDescription = seoDescription,
                    SeoTitle = seoTitle,
                    SeoText = seoText,
                    LowPrice = data.Min(x => x.Price),
                    HighPrice = data.Max(x => x.Price),
                    Count = data.Count,
                    Order = parameters.Order,
                    View = parameters.View
                };
            }
        }
        public static HomeCatalogViewModels GetProductsAdmin(CatalogParameterViewModel parameters)
        {
            using (var db = new ApplicationDbContext())
            {
                var products = db.Products.Where(x => x.Status != ProductStatus.Delete);
                if (!String.IsNullOrEmpty(parameters.Type))
                    products = products.Where(x => x.TypeProduct.Url == parameters.Type);

                if (!String.IsNullOrEmpty(parameters.Category))
                    products = products.Where(x => x.Categories.Any(y => y.Url == parameters.Category));

                if (!String.IsNullOrEmpty(parameters.Sex))
                {
                    try
                    {
                        var sex = (Sex)Enum.Parse(typeof(Sex), parameters.Sex);
                        products = products.Where(x => x.Sex == sex);
                    }
                    catch
                    {
                        return new HomeCatalogViewModels()
                        {
                            Products = new List<Product>(),
                            Page = 1,
                            LastPage = 1,
                            Type = parameters.Type,
                            Sex = parameters.Sex,
                            Category = parameters.Category
                        };
                    }
                }

                return new HomeCatalogViewModels()
                {
                    Products = products.OrderByDescending(x => x.ProductID).Skip(((int)parameters.Page - 1) * SiteConstants.AdminPageSize).Take(SiteConstants.AdminPageSize).ToList(),
                    Page = parameters.Page ?? 1,
                    LastPage = (int)Math.Ceiling((double)products.Count() / SiteConstants.AdminPageSize),
                    Type = parameters.Type,
                    Sex = parameters.Sex,
                    Category = parameters.Category
                };
            }
        }
        public static HomeDetailsViewModels GetProduct(int id)
        {
            using (var db = new ApplicationDbContext())
            {
                var model = new HomeDetailsViewModels()
                {
                    Product = db.Products.Include(x => x.Categories)
                                         .Include(x => x.ProductSizes.Select(y => y.Size))
                                         .Include(x => x.ProductColors.Select(y => y.Color))
                                         .FirstOrDefault(x => x.ProductID == id && x.Status != ProductStatus.Delete)
                };

                if (model.Product != null)
                    model.Photos = db.Photos.Where(x => x.ProductID == id).OrderBy(p => p.Order ?? int.MaxValue).ThenBy(p => p.ProductID).ToList();

                return model;
            }
        }

        public static Product GetProductById(int id)
        {
            using (var db = new ApplicationDbContext())
            {
                return db.Products.FirstOrDefault(x => x.ProductID == id);
            }
        }
        public static AdminProductViewModel GetAdminProduct(int? id)
        {
            using (var db = new ApplicationDbContext())
            {
                var model = new AdminProductViewModel()
                {
                    TypeProducts = db.TypeProduct.ToList() 
                };

                model.Product = id == null ? new Product() {
                                                Sex = Sex.woman,
                                                TypeProductID = db.TypeProduct.First().TypeProductID,
                                                PathToTitlePhoto = SiteConstants.ImageNotFound,
                                                PathToOriginPhoto = SiteConstants.ImageNotFound,
                                                Status = ProductStatus.Create
                }
                                           : db.Products.Include(x => x.Categories)
                                                        .Include(x => x.ProductSizes)
                                                        .FirstOrDefault(x => x.ProductID == id);
                var categories = id == null ? new HashSet<int>() : new HashSet<int>(model.Product.Categories.Select(c => c.CategoryID));

                var assignedCategory = new List<AssignedCategoryData>();
                foreach (var category in db.Categories)
                {
                    assignedCategory.Add(new AssignedCategoryData
                    {
                        CategoryID = category.CategoryID,
                        Title = category.Title,
                        Assigned = categories.Contains(category.CategoryID)
                    });
                }

                model.AssignedCategoryData = assignedCategory;
                return model;
            }
        }
        public static AdminProductViewModel UpdateProduct(AdminProductViewModel model, string[] selectedCategories)
        {
            using (var db = new ApplicationDbContext())
            {
                if (String.IsNullOrEmpty(model.Product.PathToTitlePhoto))
                    model.Product.PathToTitlePhoto = SiteConstants.ImageNotFound;

                if (String.IsNullOrEmpty(model.Product.PathToOriginPhoto))
                    model.Product.PathToOriginPhoto = SiteConstants.ImageNotFound;

                if (model.Product.ProductID == 0)
                {
                    db.Products.Add(model.Product);
                }
                else
                {
                    var product = db.Products.FirstOrDefault(x => x.ProductID == model.Product.ProductID);
                    if(product != null)
                    {
                        product.Title = model.Product.Title;
                        product.Status = model.Product.Status;
                        product.Description = model.Product.Description;
                        product.Price = model.Product.Price;
                        product.FakePrice = model.Product.FakePrice;
                        product.Discount = model.Product.Discount;
                        product.Sex = model.Product.Sex;
                        product.TypeProductID = model.Product.TypeProductID;
                        product.Order = model.Product.Order;
                        db.Entry(product).State = EntityState.Modified;
                    }
                }

                db.SaveChanges();
                model.TypeProducts = db.TypeProduct.ToList();
                model.AssignedCategoryData = UpdateCategories(selectedCategories, model.Product.ProductID);

                return model;
            }
        }
        private static List<AssignedCategoryData> UpdateCategories(string[] selectedCategories, int productID)
        {
            using (var db = new ApplicationDbContext())
            {
                var product = db.Products.FirstOrDefault(x => x.ProductID == productID);

                if (selectedCategories == null)
                {
                    product.Categories = new List<Category>();
                }
                else
                {
                    var selectedCategoriesHS = new HashSet<string>(selectedCategories);
                    var instructorCategories = new HashSet<int>(product.Categories.Select(c => c.CategoryID));
                    foreach (var category in db.Categories)
                    {
                        if (selectedCategoriesHS.Contains(category.CategoryID.ToString()))
                        {
                            if (!instructorCategories.Contains(category.CategoryID))
                            {
                                product.Categories.Add(category);
                            }
                        }
                        else
                        {
                            if (instructorCategories.Contains(category.CategoryID))
                            {
                                product.Categories.Remove(category);
                            }
                        }
                    }
                }

                db.SaveChanges();

                var assignedCategory = new List<AssignedCategoryData>();
                foreach (var category in db.Categories)
                {
                    assignedCategory.Add(new AssignedCategoryData
                    {
                        CategoryID = category.CategoryID,
                        Title = category.Title,
                        Assigned = selectedCategories == null ? false : product.Categories.Select(x => x.CategoryID).Contains(category.CategoryID)
                    });
                }

                return assignedCategory;
            }
        }
        public static AdminPhotoViewModel GetAdminPhotos(int id)
        {
            using (var db = new ApplicationDbContext())
            {
                var model = new AdminPhotoViewModel()
                {
                    Photos = db.Photos.Where(x => x.ProductID == id).ToList(),
                    ProductID = id
                };
                return model;
            }
        }
        public static ShippingViewModel CreateShipping(string id)
        {
            using (var db = new ApplicationDbContext())
            {
                var shipping = new ShippingDetails()
                {
                    Status = ShippingStatus.Active,
                    UserID = id,
                    Active = DateTime.Now,
                    Create = DateTime.Now,
                    AddressAreaID = 1                   
                };

                //db.ShippingDetails.Add(shipping);
                //db.SaveChanges();

                return new ShippingViewModel()
                {
                    shipping = shipping
                };
            }
        }

        public static ShippingDetails GetShipping(ShippingDetails shipping)
        {
            using (var db = new ApplicationDbContext())
            {
                if(shipping.ShippingDetailsID == 0)
                {
                    db.ShippingDetails.Add(shipping);
                    db.SaveChanges();
                }

                return shipping;
            }
         }
        public static ShippingViewModel GetShippingByUserId(string id)
        {
            using (var db = new ApplicationDbContext())
            {
                var shipping = db.ShippingDetails.FirstOrDefault(x => x.UserID == id && x.Status == ShippingStatus.Active);
                if (shipping == null)
                    return CreateShipping(id);

                return new ShippingViewModel()
                {
                    shipping = shipping,
                    products = db.ProductShippings.Where(x => x.ShippingDetailsID == shipping.ShippingDetailsID).Include(x => x.Product).Include(x => x.Size).Include(x => x.Color).ToList()
                };
            }
        }
        public static ProductShipping AddProductShipping(int productID, int quantity, int sizeID, int shippingDetailsID, int colorID)
        {
            using (var db = new ApplicationDbContext())
            {
                var product = new ProductShipping()
                {
                     ProductID = productID,
                     Quantity = quantity,
                     SizeID = sizeID,
                     ColorID = colorID,
                     ShippingDetailsID = shippingDetailsID
                };

                db.ProductShippings.Add(product);
                db.SaveChanges();

                product.Product = db.Products.FirstOrDefault(x => x.ProductID == product.ProductID);
                product.Size = db.Sizes.FirstOrDefault(x => x.SizeID == product.SizeID);
                product.Color = db.Colors.FirstOrDefault(x => x.ColorID == product.ColorID);
                return product;
            }
        }
        public static void UpdateProductShipping(int productShippingID, int quantit)
        {
            using (var db = new ApplicationDbContext())
            {
                var item = db.ProductShippings.Find(productShippingID);
                item.Quantity = quantit;
                db.Entry(item).State = EntityState.Modified;
                db.SaveChanges();
            }
        }
        public static void DeleteProductShipping(int productShippingID)
        {
            using (var db = new ApplicationDbContext())
            {
                var item = db.ProductShippings.Find(productShippingID);
                db.Entry(item).State = EntityState.Deleted;
                db.SaveChanges();
            }
        }
        public static void DeleteAllProductShipping(int shippingDetailsID)
        {
            using (var db = new ApplicationDbContext())
            {
                var items = db.ProductShippings.Where(x => x.ShippingDetailsID == shippingDetailsID);

                foreach (var item in items)
                {
                    db.Entry(item).State = EntityState.Deleted;
                }
                
                db.SaveChanges();
            }
        }
        public static void AddPhotos(List<Photo> photos)
        {
            using (var db = new ApplicationDbContext())
            {
                db.Photos.AddRange(photos);
                db.SaveChanges();
            }
        }
        public static void UpdatePhotos(List<Photo> photos)
        {
            using (var db = new ApplicationDbContext())
            {

                foreach (var photo in photos)
                {
                    var item = db.Photos.FirstOrDefault(x => x.PhotoID == photo.PhotoID);
                    if(item != null)
                    {
                        item.Order = photo.Order;
                        db.Entry(item).State = EntityState.Modified;
                    }
                }

                db.SaveChanges();
            }
        }
        public static List<string> DeletePhoto(int photoID)
        {
            using (var db = new ApplicationDbContext())
            {
                var item = db.Photos.Find(photoID);
                if (item != null)
                {
                    var list = new List<string>()
                    {
                         item.Thumbnail,
                         item.Path,
                    };

                    db.Entry(item).State = EntityState.Deleted;
                    db.SaveChanges();
                    return list;
                }

                return null;
            }
        }
        public static void SetMainPhoto(int photoID, int productID)
        {
            using (var db = new ApplicationDbContext())
            {
                var photo = db.Photos.FirstOrDefault(x => x.PhotoID == photoID);
                var product = db.Products.FirstOrDefault(x => x.ProductID == productID);
                if (photo != null && product != null)
                {
                    product.PathToTitlePhoto = photo.Thumbnail;
                    product.PathToOriginPhoto = photo.Path;
                    db.Entry(product).State = EntityState.Modified;
                    db.SaveChanges();
                }
            }
        }
        public static AdminAssignedViewModel GetAdminColor(int id)
        {
            using (var db = new ApplicationDbContext())
            {
                var product = db.Products.Include(x => x.ProductColors).FirstOrDefault(x => x.ProductID == id);
                var assignedSize = new List<AssignedData>();

                foreach (var color in db.Colors.OrderBy(x => x.Title))
                {
                    var item = product.ProductColors.FirstOrDefault(x => x.ColorID == color.ColorID);
                    assignedSize.Add(new AssignedData
                    {
                        ID = color.ColorID,
                        Title = color.Title,
                        Assigned = item != null ? true : false,
                        Count = item?.Count
                    });
                }

                return new AdminAssignedViewModel()
                {
                    ProductID = id,
                    AssignedData = assignedSize
                };
            }
        }

        public static void UpdateProductColor(AdminAssignedViewModel model)
        {
            using (var db = new ApplicationDbContext())
            {
                var colors = db.ProductColors.Where(x => x.ProductID == model.ProductID).ToList();
                foreach (var item in model.AssignedData)
                {
                    var color = colors.FirstOrDefault(x => x.ColorID == item.ID);
                    if (item.Assigned)
                    {
                        if (color != null)
                        {
                            color.Count = item.Count;
                            db.Entry(color).State = EntityState.Modified;
                        }
                        else
                        {
                            db.ProductColors.Add(new ProductColor()
                            {
                                ProductID = model.ProductID,
                                ColorID = item.ID,
                                Count = item.Count
                            });
                        }
                    }
                    else if (color != null)
                    {
                        db.Entry(color).State = EntityState.Deleted;
                    }
                }

                db.SaveChanges();
            }
        }

        public static AdminAssignedViewModel GetAdminSize(int id)
        {
            using (var db = new ApplicationDbContext())
            {
                var product = db.Products.Include(x => x.ProductSizes).FirstOrDefault(x => x.ProductID == id);
                var assignedSize = new List<AssignedData>();

                foreach (var size in db.Sizes)
                {
                    var item = product.ProductSizes.FirstOrDefault(x => x.SizeID == size.SizeID);
                    assignedSize.Add(new AssignedData
                    {
                        ID = size.SizeID,
                        Title = size.Title,
                        Assigned = item != null ? true : false,
                        Count = item?.Count
                    });
                }

                return new AdminAssignedViewModel()
                {
                     ProductID = id,
                     AssignedData = assignedSize
                }; 
            }
        }
        public static void UpdateProductSizes(AdminAssignedViewModel model)
        {
            using (var db = new ApplicationDbContext())
            {
                var sizes = db.ProductSizes.Where(x => x.ProductID == model.ProductID).ToList();
                foreach (var item in model.AssignedData)
                {
                    var size = sizes.FirstOrDefault(x => x.SizeID == item.ID);
                    if(item.Assigned)
                    {
                        if(size != null)
                        {
                            size.Count = item.Count;
                            db.Entry(size).State = EntityState.Modified;
                        }
                        else
                        {
                            db.ProductSizes.Add(new ProductSize() {
                                  ProductID = model.ProductID,
                                  SizeID = item.ID,
                                  Count = item.Count
                            });
                        }
                    }
                    else if (size != null)
                    {
                        db.Entry(size).State = EntityState.Deleted;
                    }
                }

                db.SaveChanges();
            }
        }
        public static List<SelectListItem> GetAddressArea()
        {
            using (var db = new ApplicationDbContext())
            {
                return db.AddressAreas.Where(x => x.IsVisible == true).Select(x => new SelectListItem
                {
                    Text = x.Description,
                    Value = x.AddressAreaID.ToString()
                }).OrderBy(x => x.Text).ToList();
            }
        }

        public static AddressArea GetAddressAreaById(int id)
        {
            using (var db = new ApplicationDbContext())
            {
                return db.AddressAreas.FirstOrDefault(x => x.AddressAreaID == id);
            }
        }

        public static void UpdateShipping(ShippingDetails model)
        {
            using (var db = new ApplicationDbContext())
            {
                var item = db.ShippingDetails.Find(model.ShippingDetailsID);
                item.Status = ShippingStatus.Checkout;
                item.Create = DateTime.Now;
                item.FirstName = model.FirstName;
                item.LastName = model.LastName;
                item.Phone = model.Phone;
                item.Delivery = model.Delivery;
                item.Payment = model.Payment;
                item.AddressAreaID = model.AddressAreaID;
                item.Town = model.Town;
                item.Address = model.Address;
                item.ZipCode = model.ZipCode;
                item.Warehouses = model.Warehouses;
                item.Total = model.Total;

                db.Entry(item).State = EntityState.Modified;
                db.SaveChanges();
            }
        }

        public static void UpdateShippingNote(ShippingDetails model)
        {
            using (var db = new ApplicationDbContext())
            {
                var item = db.ShippingDetails.Find(model.ShippingDetailsID);
                item.Status = model.Status;
                item.Note = model.Note;
                item.Number = model.Number;

                db.Entry(item).State = EntityState.Modified;
                db.SaveChanges();
            }
        }


        public static AdminOrderViewModel GetOrederAdmin(OrderParameterViewModel parameters)
        {
            using (var db = new ApplicationDbContext())
            {
                var shipping = db.ShippingDetails.Where(x => x.Status != ShippingStatus.Active);

                return new AdminOrderViewModel()
                {
                    Orders = shipping.OrderByDescending(x => x.Create).Skip(((int)parameters.Page - 1) * SiteConstants.AdminPageSize).Take(SiteConstants.AdminPageSize).ToList(),
                    Page = parameters.Page ?? 1,
                    LastPage = (int)Math.Ceiling((double)shipping.Count() / SiteConstants.AdminPageSize)
                };
            }
        }

        public static AdminShippingViewModel GetOrederAdminById(int id)
        {
            using (var db = new ApplicationDbContext())
            {
                var model = new AdminShippingViewModel()
                {
                    shipping = db.ShippingDetails.Include(x => x.AddressArea).FirstOrDefault(x => x.ShippingDetailsID == id)
                };
              
                if (model.shipping == null)
                    return null;

                model.products = db.ProductShippings.Include(x => x.Product).Include(x => x.Size).Include(x => x.Color).Where(x => x.ShippingDetailsID == id).ToList();

                return model;
            }
        }

        public static void SaveErrorException(ErrorLog data)
        {
            using (var db = new ApplicationDbContext())
            {
                db.ErrorLogs.Add(data);
                db.SaveChanges();
            }
        }

        public static void AddEmail(string email)
        {
            using (var db = new ApplicationDbContext())
            {
                var item = db.Emails.FirstOrDefault(x => x.Title == email);
                if(item == null)
                {
                    db.Emails.Add(new Email() {
                         Title = email,
                         Date = DateTime.Now
                    });

                    db.SaveChanges();
                }
            }
        }

        public static List<Model.TypeProduct> GetTypeProduct()
        {
            using (var db = new ApplicationDbContext())
            {
                return db.TypeProduct.ToList();
            }
        }

        public static void UpadateTypeProduct(int typeProductID, string seoTitle, string seoDescription, string seoText)
        {
            using (var db = new ApplicationDbContext())
            {
                var typeProduct = db.TypeProduct.FirstOrDefault(x => x.TypeProductID == typeProductID);
                if (typeProduct != null)
                {
                    typeProduct.SeoTitle = seoTitle;
                    typeProduct.SeoDescription = seoDescription;
                    typeProduct.SeoText = seoText;

                    db.Entry(typeProduct).State = EntityState.Modified;
                    db.SaveChanges();
                }
            }
        }

        public static List<Category> GetCategories()
        {
            using (var db = new ApplicationDbContext())
            {
                return db.Categories.ToList();
            }
        }

        public static void UpadateCategory(int categoryID, string seoTitle, string seoDescription, string seoText)
        {
            using (var db = new ApplicationDbContext())
            {
                var category = db.Categories.FirstOrDefault(x => x.CategoryID == categoryID);
                if (category != null)
                {
                    category.SeoTitle = seoTitle;
                    category.SeoDescription = seoDescription;
                    category.SeoText = seoText;

                    db.Entry(category).State = EntityState.Modified;
                    db.SaveChanges();
                }
            }
        }

        public static List<Color> GetColors()
        {
            using (var db = new ApplicationDbContext())
            {
                return db.Colors.OrderBy(x => x.Title).ToList();
            }
        }

        public static Color GetColorById(int id)
        {
            using (var db = new ApplicationDbContext())
            {
                return db.Colors.FirstOrDefault(x => x.ColorID == id);
            }
        }

        public static void AddOrUpdateColor(Color model)
        {
            using (var db = new ApplicationDbContext())
            {
                var color = db.Colors.Find(model.ColorID);
                if (color != null)
                {
                    color.Title = model.Title;
                    db.Entry(color).State = EntityState.Modified;
                }
                else
                {
                    db.Colors.Add(new Color() { Title = model.Title });
                }

                db.SaveChanges();
            }
        }

        public static List<Size> GetSizes()
        {
            using (var db = new ApplicationDbContext())
            {
                return db.Sizes.OrderBy(x => x.Title).ToList();
            }
        }

        public static Size GetSizeById(int id)
        {
            using (var db = new ApplicationDbContext())
            {
                return db.Sizes.FirstOrDefault(x => x.SizeID == id);
            }
        }

        public static void AddOrUpdateSize(Size model)
        {
            using (var db = new ApplicationDbContext())
            {
                var size = db.Sizes.Find(model.SizeID);
                if (size != null)
                {
                    size.Title = model.Title;
                    db.Entry(size).State = EntityState.Modified;
                }
                else
                {
                    db.Sizes.Add(new Size() { Title = model.Title });
                }

                db.SaveChanges();
            }
        }
    }
}
