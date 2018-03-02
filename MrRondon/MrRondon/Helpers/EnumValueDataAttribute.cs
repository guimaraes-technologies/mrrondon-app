using System;

namespace MrRondon.Helpers
{
    //https://forums.xamarin.com/discussion/74074/enum-description-in-pcl
    public class EnumValueDataAttribute : Attribute
    {
        public string Description { get; set; }
        public string KeyValue { get; set; }
    }
}