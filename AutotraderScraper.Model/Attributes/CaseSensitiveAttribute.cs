using System;

namespace AutotraderScraper.Model.Attributes
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = true)]
    public class CaseSensitiveAttribute : Attribute
    {
        public CaseSensitiveAttribute() => IsEnabled = true;

        public bool IsEnabled { get; set; }
    }
}
