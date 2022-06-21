using ECommerce.Domain.ConstantsData;
using System;
using System.ComponentModel;
using System.Reflection;

namespace ECommerce.WebUI.Helpers
{
    public static class HelperCommon
    {
        public static string GetDescription(Enum value)
        {
            FieldInfo field = value.GetType().GetField(value.ToString());

            DescriptionAttribute attribute
                    = Attribute.GetCustomAttribute(field, typeof(DescriptionAttribute))
                        as DescriptionAttribute;

            return attribute == null ? value.ToString() : attribute.Description;
        }

        public static void SendMessageToTelegram(string message)
        {
            var botClient = new NetTelebot.TelegramBotClient() { Token = SiteConstants.TelegramToken };
            botClient.SendMessage(SiteConstants.TelegramBotID, message);
        }
    }
}