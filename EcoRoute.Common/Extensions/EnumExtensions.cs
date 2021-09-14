using System;
using EcoRoute.Common.Attributes;

namespace EcoRoute.Common.Extensions
{
    public static class EnumExtensions
    {
        public static string GetStringValue(this Enum enumValue)
        {
            var type = enumValue.GetType();
            var fieldName = Enum.GetName(type, enumValue);
            if (fieldName == null) return null;
            
            var field = type.GetField(fieldName);
            if (field == null) return null;
            
            if (Attribute.GetCustomAttribute(field, typeof(StringValueAttribute)) 
                is StringValueAttribute stringValueAttribute)
            {
                return stringValueAttribute.Value;
            }

            return null;
        }
    }
}