using System.Linq;
using System.Xml.Linq;
using ThreatLibrary.Parser.XmlParsers;

namespace ThreatLibrary.Parser.Capec.Parsers
{
    static class StructuredTextParser
    {
        public static string Parse(XElement element)
        {
            return element.XhtmlToText();
        }
        
        public static string[] ParseCollection(XElement element, XName name)
        {
            return element.Elements(name)
                .Select(Parse)
                .ToArray();
        }
    }
}