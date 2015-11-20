using System;
using System.Windows.Markup;

namespace DotWatcher.ValueConverters
{
    /// <summary>
    /// Serves as base class for all the value converters in the project
    /// </summary>
    public abstract class BaseConverter : MarkupExtension
    {
        /// <summary>
        /// Provides a value
        /// </summary>
        /// <param name="serviceProvider">The IServiceProvider instance to use</param>
        /// <returns>The value</returns>
        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            return this;
        }
    }
}
