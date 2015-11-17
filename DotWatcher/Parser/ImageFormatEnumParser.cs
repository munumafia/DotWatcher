using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using DotWatcher.Attributes;

namespace DotWatcher.Parser
{
    public class ImageFormatEnumParser
    {
        public IList<ImageFormatEnumField> Parse()
        {
            return typeof(ImageFormat)
                .GetFields(BindingFlags.Public | BindingFlags.Static)
                .Select(field => new { Field = field, Attribute = field.GetCustomAttribute<FormatInfoAttribute>() })
                .Select(fieldAttrPair => new ImageFormatEnumField
                {
                    Description = fieldAttrPair.Attribute.Description,
                    Extensions = fieldAttrPair.Attribute.Extensions.Split(',').Select(ext => ext.Trim()).ToList(),
                    Field = fieldAttrPair.Field
                }).ToList();
        }
    }

    public class ImageFormatEnumField
    {
        public string Description { get; set; }

        public IList<string> Extensions { get; set; }

        public FieldInfo Field { get; set; }
    }
}