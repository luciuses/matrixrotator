using System;
using System.ComponentModel;

namespace MatrixRotator.Helpers
{
    public static class EnumExtensions
    {
        public static string GetDescription(this Enum value)
        {
            var fi = value.GetType().GetField(value.ToString());
            var attributes = (DescriptionAttribute[])fi.GetCustomAttributes(typeof(DescriptionAttribute), false);
            return (attributes.Length > 0) ? attributes[0].Description : value.ToString();
        }

        public static string GetDescription<T>(T value) where T : struct
        {
            var enumValue = value as Enum;
            if (enumValue == null)
            {
                throw new Exception("Type given T must be an Enum");
            }

            return enumValue.GetDescription();
        }
    }
}
