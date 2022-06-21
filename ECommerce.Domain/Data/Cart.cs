using ECommerce.Domain.Model;
using ECommerce.Domain.ViewModel;
using System.Linq;

namespace ECommerce.Domain.Data
{
    public class Cart
    {
        private ShippingViewModel shipping = new ShippingViewModel();

        public Cart()
        {
        }
        public Cart(ShippingViewModel data)
        {
            shipping = data;
        }
        public void AddItem(Product product, int quantity, int sizeID, int colorID)
        {
            shipping.shipping = ProductData.GetShipping(shipping.shipping);

            var line = shipping.products.FirstOrDefault(x => x.Product.ProductID == product.ProductID && x.SizeID == sizeID && x.ColorID == colorID);

            if (line == null)
            {
                shipping.products.Add(ProductData.AddProductShipping(product.ProductID, quantity, sizeID, shipping.shipping.ShippingDetailsID, colorID));
            }
            else
            {
                line.Quantity += quantity;
                ProductData.UpdateProductShipping(line.ProductShippingID, line.Quantity);
            }
        }

        public void RemoveLine(int productShippingID)
        {
            if(shipping.products.Any(x => x.ProductShippingID == productShippingID))
            {
                shipping.products.RemoveAll(x => x.ProductShippingID == productShippingID);
                ProductData.DeleteProductShipping(productShippingID);
            }
        }

        public void RemoveAllLine()
        {
            if (shipping.products.Any())
            {
                shipping.products.Clear();
                ProductData.DeleteAllProductShipping(shipping.shipping.ShippingDetailsID);
            }
        }

        public ShippingViewModel GetShipping()
        {
            return shipping;
        }

        public decimal ComputeTotalValue()
        {
            decimal value = 0;
            foreach (var item in shipping.products)
            {
                value += item.Quantity * item.Product.Price;
            }

            shipping.Total = value;
            return value;
        }

        public int GetQuantity()
        {
            int value = 0;
            foreach (var item in shipping.products)
            {
                value += item.Quantity;
            }

            return value;
        }

        public ShippingViewModel GetModel()
        {
            return shipping;
        }
    }
}
