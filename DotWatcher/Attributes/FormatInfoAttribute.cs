using System;

namespace DotWatcher.Attributes
{
    [AttributeUsage(AttributeTargets.Field)]
    public class FormatInfoAttribute : Attribute
    {
        public string Description { get; set; }
        public string Extensions { get; set; }
    }
}
