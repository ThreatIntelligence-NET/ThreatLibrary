using System.Linq;
using System.Xml.Linq;
using ThreatLibrary.Parser.Capec.Parsers;
using ThreatLibrary.Parser.XmlParsers;

namespace ThreatLibrary.Parser.Capec
{
    public class TechniqueEntity
    {
        public int? CapecId { get; }
        public string Value { get; }

        public TechniqueEntity(int? capecId, string value)
        {
            CapecId = capecId;
            Value = value;
        }

        public static TechniqueEntity Parse(XElement element)
        {
            return new(
                element.GetOptionalAttributeAsInteger("CAPEC_ID"),
                StructuredTextParser.Parse(element)
            );
        }

        public static TechniqueEntity[] ParseCollection(XElement element)
        {
            return element.Elements(CapecNamespaces.DefaultNamespace + "Technique")
                .Select(Parse)
                .ToArray();
        }
    }
}