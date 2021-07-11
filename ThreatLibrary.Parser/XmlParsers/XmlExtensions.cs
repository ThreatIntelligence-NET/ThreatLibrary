using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace ThreatLibrary.Parser.XmlParsers
{
    static class XmlExtensions
    {
        #region Normalizer
        
        enum XhtmlParagraphType
        {
            None,
            FullParagraph,
            HalfParagraph
        }

        public static XElement NormalizeText(this XElement element)
        {
            foreach (XText node in element.DescendantNodesAndSelf().OfType<XText>())
            {
                node.Value = ConsolidateWhitespace(node.Value);
            }

            return element;
        }
        
        static string ConsolidateWhitespace(string value)
        {
            var builder = new StringBuilder(value.Length);
            const int initial = 0;
            const int normalCharacter = 1;
            const int whitespace = 2;

            int currentStatus = initial;
            foreach (char c in value)
            {
                switch (currentStatus)
                {
                    case initial:
                        if (char.IsWhiteSpace(c))
                        {
                            currentStatus = whitespace;
                            builder.Append(' ');
                        }
                        else
                        {
                            currentStatus = normalCharacter;
                            builder.Append(c);
                        }
                        break;
                    case whitespace:
                        if (!char.IsWhiteSpace(c))
                        {
                            currentStatus = normalCharacter;
                            builder.Append(c);
                        }

                        break;
                    case normalCharacter:
                        if (char.IsWhiteSpace(c))
                        {
                            currentStatus = whitespace;
                            builder.Append(' ');
                        }
                        else
                        {
                            builder.Append(c);
                        }
                        break;
                }
            }

            return builder.ToString();
        }
        
        static string ConsolidateParagraph(IEnumerable<string?> parsed)
        {
            const int initial = 0;
            const int breaks = 1;
            const int paragraph = 2;
            int current = initial;
            var paragraphs = new StringBuilder();
            
            foreach (string? item in parsed)
            {
                switch (current)
                {
                    case initial:
                    {
                        if (item == null) continue;
                        paragraphs.Append(item);
                        current = paragraph;

                        break;
                    }
                    case paragraph:
                    {
                        if (item == null)
                        {
                            paragraphs.AppendLine();
                            current = breaks;
                        }
                        else
                        {
                            paragraphs.Append(item);
                        }

                        break;
                    }
                    case breaks:
                    {
                        if (item == null) continue;
                        paragraphs.Append(item);
                        current = paragraph;

                        break;
                    }
                }
            }
            
            return paragraphs.ToString();
        }

        static XNamespace XhtmlNamespace => "http://www.w3.org/1999/xhtml";
        
        static readonly HashSet<XName> XhtmlParagraphElements = new()
        {
            XhtmlNamespace + "p",
            XhtmlNamespace + "ul",
            XhtmlNamespace + "ol",
            XhtmlNamespace + "div"
        };

        static readonly HashSet<XName> XhtmlHalfParagraphElements = new()
        {
            XhtmlNamespace + "li"
        };
        
        static XhtmlParagraphType GetXhtmlParagraphType(this XElement element)
        {
            if (XhtmlParagraphElements.Contains(element.Name)) return XhtmlParagraphType.FullParagraph;
            if (XhtmlHalfParagraphElements.Contains(element.Name)) return XhtmlParagraphType.HalfParagraph;
            return XhtmlParagraphType.None;
        }

        static void ParseXhtml(XElement element, ICollection<string?> parsed, int currentRecursiveNumber, int maxDepth)
        {
            if (currentRecursiveNumber > maxDepth)
            {
                parsed.Add(element.Value);
                return;
            }
            
            foreach (var node in element.Nodes())
            {
                switch (node)
                {
                    case XText textNode:
                        parsed.Add(textNode.Value);
                        break;
                    case XElement elementNode:
                    {
                        XhtmlParagraphType paragraphType = elementNode.GetXhtmlParagraphType();
                        if (paragraphType == XhtmlParagraphType.FullParagraph) parsed.Add(null);
                        ParseXhtml(elementNode, parsed, currentRecursiveNumber + 1, maxDepth);
                        if (paragraphType != XhtmlParagraphType.None) parsed.Add(null);
                        break;
                    }
                }
            }
        }
        
        public static string XhtmlToText(this XElement element, int maxDepth = 8)
        {
            var parsed = new List<string?>();
            ParseXhtml(element, parsed, 1, maxDepth);
            return ConsolidateParagraph(parsed);
        }
        
        #endregion
        
        #region Required Attribute
        
        public static string GetRequiredAttributeValue(this XElement element, XName attributeName)
        {
            XAttribute? attribute = element.Attribute(attributeName);
            if (attribute == null)
            {
                throw new FormatException($"The attribute '{attributeName}' is mandatory.");
            }

            return attribute.Value;
        }
        
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
                value => int.Parse(value, NumberStyles.Integer, CultureInfo.InvariantCulture)
            );
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