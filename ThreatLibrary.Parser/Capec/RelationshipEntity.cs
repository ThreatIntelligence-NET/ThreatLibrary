using System.Linq;
using System.Xml.Linq;
using ThreatLibrary.Parser.XmlParsers;

namespace ThreatLibrary.Parser.Capec
{
    public class RelationshipEntity
    {
        public ExcludeRelatedEntity[] ExcludeRelateds { get; }
        public int CapecId { get; }

        RelationshipEntity(int capecId, ExcludeRelatedEntity[] excludeRelateds)
        {
            ExcludeRelateds = excludeRelateds;
            CapecId = capecId;
        }

        public static RelationshipEntity Parse(XElement element)
        {
            return new(
                element.GetRequiredAttributeAsInteger("CAPEC_ID"),
                element.Elements(CapecNamespaces.DefaultNamespace + "Exclude_Related")
                    .Select(ExcludeRelatedEntity.Parse)
                    .ToArray()
            );
        }
    }
}