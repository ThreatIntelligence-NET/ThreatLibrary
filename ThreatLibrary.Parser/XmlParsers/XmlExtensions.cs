using System;
using System.Globalization;
using System.Xml.Linq;

namespace ThreatLibrary.Parser.XmlParsers
{
    /// <summary>
    /// This class contains common method for retrieving XML content.
    /// </summary>
    public static class XmlExtensions
    {
        #region Required Attribute
        
        /// <summary>
        /// Get attribute value from specified <paramref name="element"/>. This attribute is mandatory.
        /// </summary>
        /// <param name="element">The element which contains the attribute.</param>
        /// <param name="attributeName">The name of the attribute.</param>
        /// <returns>The string value of the attribute.</returns>
        /// <exception cref="FormatException">The attribute does not exist.</exception>
        public static string GetRequiredAttributeValue(this XElement element, XName attributeName)
        {
            XAttribute? attribute = element.Attribute(attributeName);
            if (attribute == null)
            {
                throw new FormatException($"The attribute '{attributeName}' is mandatory.");
            }

            return attribute.Value;
        }
        
        /// <summary>
        /// Get attribute value from specified <paramref name="element"/> and convert the content of the value into
        /// <typeparamref name="T"/>. This attribute is mandatory.
        /// </summary>
        /// <param name="element">The element which contains the attribute.</param>
        /// <param name="attributeName">The name of the attribute.</param>
        /// <param name="parser">The parser which convert the string value into <typeparamref name="T"/>.</param>
        /// <typeparam name="T">The destination type.</typeparam>
        /// <returns>The converted attribute value</returns>
        /// <exception cref="FormatException">The attribute does not exist.</exception>
        public static T GetRequiredAttributeAs<T>(
            this XElement element,
            XName attributeName,
            Func<string, T> parser)
        {
            if (parser == null) { throw new ArgumentNullException(nameof(parser)); }
            
            string value = GetRequiredAttributeValue(element, attributeName);
            T taxonomyName = parser(value);
            return taxonomyName;
        }

        public static int GetRequiredAttributeAsInteger(
            this XElement element,
            XName attributeName)
        {
            return GetRequiredAttributeAs(
                element,
                attributeName,
                value => int.Parse(value, NumberStyles.Integer, CultureInfo.InvariantCulture));
        }
        
        #endregion
        
        #region Optional Attribute

        public static string? GetOptionalAttributeValue(
            this XElement element,
            XName attributeName,
            string? defaultValue = null)
        {
            XAttribute? attribute = element.Attribute(attributeName);
            return attribute == null ? defaultValue : attribute.Value;
        }

        static T? GetOptionalAttributeAsStruct<T>(
            this XElement element,
            XName attributeName,
            Func<string, T> parser,
            T? defaultValue = null) where T : struct
        {
            string? value = GetOptionalAttributeValue(element, attributeName);
            return value == null ? defaultValue : parser(value);
        }

        public static int? GetOptionalAttributeAsInteger(
            this XElement element,
            XName attributeName)
        {
            return GetOptionalAttributeAsStruct(
                element,
                attributeName,
                value => int.Parse(value, NumberStyles.Integer, CultureInfo.InvariantCulture));
        }
        
        #endregion
        
        #region Optional Element
        
        public static T? GetOptionalElementAsSingle<T>(
            this XElement element,
            XName elementName,
            Func<XElement, T> parser,
            T? defaultValue) where T : class
        {
            XElement? child = element.Element(elementName);
            return child == null ? defaultValue : parser(child);
        }
        
        public static T[] GetOptionalElementAsCollection<T>(
            this XElement element,
            XName elementName,
            Func<XElement, T[]> parser)
        {
            XElement? child = element.Element(elementName);
            return child == null ? Array.Empty<T>() : parser(child);
        }
        
        public static string? GetOptionalElementValue(
            this XElement element,
            XName elementName,
            string? defaultValue = null)
        {
            XElement? child = element.Element(elementName);
            return child == null ? defaultValue : child.Value;
        }
        
        public static T? GetOptionalElementValueAsStruct<T>(
            this XElement element,
            XName elementName,
            Func<string, T> parser) where T : struct 
        {
            XElement? child = element.Element(elementName);
            return child == null ? null : parser(child.Value);
        }
        
        #endregion
        
        #region Required Element
        
        public static XElement GetRequiredElement(this XElement element, XName elementName)
        {
            XElement? child = element.Element(elementName);
            if (child == null)
            {
                throw new FormatException($"The element '{elementName}' is mandatory");
            }

            return child;
        }

        public static T GetRequiredElementAsSingle<T>(
            this XElement element, 
            XName elementName,
            Func<XElement, T> parser)
        {
            XElement child = GetRequiredElement(element, elementName);
            return parser(child);
        }
        
        public static string GetRequiredElementValue(
            this XElement element,
            XName elementName)
        {
            return GetRequiredElement(element, elementName).Value;
        }
        
        public static T GetRequiredElementValueAs<T>(
            this XElement element,
            XName elementName,
            Func<string, T> parser)
        {
            return parser(GetRequiredElementValue(element, elementName));
        }

        public static int GetRequiredElementValueAsInteger(
            this XElement element,
            XName elementName)
        {
            return GetRequiredElementValueAs(
                element,
                elementName,
                value => int.Parse(value, NumberStyles.Integer, CultureInfo.InvariantCulture));
        }

        #endregion
    }
}