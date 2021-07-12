using System.Xml.Linq;

namespace ThreatLibrary.Parser.Capec.Parsers
{
    static class PrerequisitesParser
    {
        public static string[] ParseCollection(XElement element)
        {
            return StructuredTextParser
                .ParseCollection(element, CapecNamespaces.DefaultNamespace + "Prerequisite");
        }
    }
}