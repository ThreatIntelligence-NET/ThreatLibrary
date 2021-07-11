using System.Linq;
using System.Xml.Linq;
using ThreatLibrary.Parser.XmlParsers;

namespace ThreatLibrary.Parser.Capec
{
    public class RelatedWeaknessEntity
    {
        public int CweId { get; }

        RelatedWeaknessEntity(int cweId)
        {
            CweId = cweId;
        }

        public static RelatedWeaknessEntity Parse(XElement element)
        {
            return new(element.GetRequiredAttributeAsInteger("CWE_ID"));
        }

        public static RelatedWeaknessEntity[] ParseCollection(XElement element)
        {
            return element
                .Elements(CapecNamespaces.DefaultNamespace + "Related_Weakness")
                .Select(Parse)
                .ToArray();
        }
    }
}