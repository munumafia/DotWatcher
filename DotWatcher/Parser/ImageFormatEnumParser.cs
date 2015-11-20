using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using DotWatcher.Attributes;

namespace DotWatcher.Parser
{
    /// <summary>
    /// Parses the metadata associated with each image format in the ImageFormat enum. This metadata includes 
    /// information like the image format name and the file extensions associated with it.
    /// </summary>
    public class ImageFormatEnumParser
    {
        /// <summary>
        /// Parses the metadata associated with each image format in the ImageFormat enum
        /// </summary>
        /// <returns>A list of the enum fields and the associated metadata</returns>
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

    /// <summary>
    /// Class representing an ImageFormat enum field and its associated metadata
    /// </summary>
    public class ImageFormatEnumField
    {
        /// <summary>
        /// A description of the image format
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// The file extensions associated with the image format
        /// </summary>
        public IList<string> Extensions { get; set; }

        /// <summary>
        /// The field that the ImageFormatEnumField instance is associated with
        /// </summary>
        public FieldInfo Field { get; set; }
    }
}