using System.ComponentModel;

namespace ECommerce.Domain.Model
{
    public class EnumModel
    {
        public enum Sex
        {
            man = 1,
            woman = 2,
            boy = 4,
            girl = 8
        }

        public enum ProductStatus
        {
            Create = 0,
            Available = 1,
            NotAvailable = 2,
            Awaiting = 4,
            Delete = 8
        }

        public enum TypeProduct
        {
            Shoes = 1,
            Clothing = 2,
            Bags = 3,
            Accessories = 4
        }

        public enum DetailsCategory
        {
            None = 1,
            //Shoes
            Sneakers = 1,
            Heels = 2,
            Loafers = 3,
            Sandals = 4,
            Boots = 5,
            Oxfords = 6,
            //Clothing
            Dresses = 7,
            ShirtsAndTops = 8,
            Swimwear = 9,
            Shorts = 10,
            Pants = 11,
            //Bags
            Handbags = 12,
            Backpacks = 13,
            WalletsAndAccessories = 14,
            Luggage = 15,
            //Accessories
            Sunglasses = 16,
            Hats = 17,
            Watches = 18,
            Jewelry = 19,
            Belts = 20
        }

        public enum ShippingStatus
        {
            [Description("Созданный заказ")]
            Active = 1,
            [Description("Новый заказ")]
            Checkout = 2,
            [Description("Подтвержденный заказ")]
            Confirm = 3,
            [Description("Отправленный заказ")]
            Sent = 4,
            [Description("Возвращенный заказ")]
            Return = 5,
            [Description("Отмененный заказ")]
            Cancel = 6,
            [Description("Выполненный заказ")]
            Done = 7
        }

        public enum TypePayment
        {
            [Description("Предоплата")]
            Prepayment = 1,
            [Description("Наложенный платеж с предоплатой за доставку 70 грн.")]
            CashOnDelivery = 2
        }

        public enum DeliveryService
        {
            [Description("Нова Пошта")]
            NovaPoshta = 1,
            [Description("Укрпочта")]
            UkrPoshta = 2
        }
    }
}
