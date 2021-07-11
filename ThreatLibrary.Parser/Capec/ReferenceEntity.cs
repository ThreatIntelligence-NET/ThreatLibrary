using System.Linq;
using System.Xml.Linq;
using ThreatLibrary.Parser.XmlParsers;

namespace ThreatLibrary.Parser.Capec
{
    public class ReferenceEntity
    {
        public string ExternalReferenceId { get; }
        public string? Section { get; }

        public ReferenceEntity(string externalReferenceId, string? section)
        {
            ExternalReferenceId = externalReferenceId;
            Section = section;
        }

        public static ReferenceEntity Parse(XElement element)
        {
            return new(
                element.GetRequiredAttributeValue("External_Reference_ID"),
                element.GetOptionalAttributeValue("Section"));
        }

        public static ReferenceEntity[] ParseCollection(XElement element)
        {
            return element.Elements(CapecNamespaces.DefaultNamespace + "Reference")
                .Select(Parse)
                .ToArray();
        }
    }
}