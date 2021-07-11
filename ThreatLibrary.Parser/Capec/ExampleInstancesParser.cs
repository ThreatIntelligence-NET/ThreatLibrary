using System.Xml.Linq;
using ThreatLibrary.Parser.Capec.Parsers;

namespace ThreatLibrary.Parser.Capec
{
    static class ExampleInstancesParser
    {
        public static string[] ParseCollection(XElement element)
        {
            return StructuredTextParser.ParseCollection(element, CapecNamespaces.DefaultNamespace + "Example");
        }
    }
}