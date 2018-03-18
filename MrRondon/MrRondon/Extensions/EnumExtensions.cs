using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using MrRondon.Helpers;

namespace MrRondon.Extensions
{
    //https://forums.xamarin.com/discussion/74074/enum-description-in-pcl
    public static class EnumExtensions
    {
        private static TAttribute GetAttribute<TAttribute>(this Enum value) where TAttribute : Attribute
        {
            var type = value.GetType();
            var name = Enum.GetName(type, value);
            return type.GetRuntimeField(name)
                .GetCustomAttributes(false)
                .OfType<TAttribute>()
                .FirstOrDefault();
        }

        public static IEnumerable<EnumValueDataAttribute> ConvertToList<TEnum>()
            where TEnum : struct // can't constrain to enums so closest thing
        {
            return Enum.GetValues(typeof(TEnum)).Cast<Enum>()
                .Select(val => val.GetAttribute<EnumValueDataAttribute>())
                .OrderBy(o => o.Description).ToList();
        }

        public static EnumValueDataAttribute GetAttribute(Enum value)
        {
            return value.GetAttribute<EnumValueDataAttribute>();
        }

        public static EnumValueDataAttribute GetByType<TEnum>(string value) where TEnum : struct // can't constrain to enums so closest thing
        {
            return Enum.GetValues(typeof(TEnum)).Cast<Enum>()
                .Select(val => val.GetAttribute<EnumValueDataAttribute>())
                .ToList().FirstOrDefault(v => v.KeyValue.Equals(value));
        }
    }
}