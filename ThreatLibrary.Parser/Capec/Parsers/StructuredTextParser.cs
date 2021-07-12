using System.Linq;
using System.Xml;
using System.Xml.Linq;

namespace ThreatLibrary.Parser.Capec.Parsers
{
    static class StructuredTextParser
    {
        public static string Parse(XElement element)
        {
            if (element.HasElements)
            {
                using XmlReader reader = element.CreateReader(ReaderOptions.OmitDuplicateNamespaces);
                
                reader.MoveToContent();
                return reader.ReadInnerXml();
            }

            return element.Value;
        }
        
        public static string[] ParseCollection(XElement element, XName name)
        {
            return element.Elements(name)
                .Select(Parse)
                .ToArray();
        }
    }
}