using System.Linq;
using System.Xml.Linq;
using ThreatLibrary.Parser.Capec.Parsers;
using ThreatLibrary.Parser.XmlParsers;

namespace ThreatLibrary.Parser.Capec
{
    public class RelatedAttackPatternEntity
    {
        public ExcludeRelatedEntity[] ExcludeRelateds { get; }
        public RelatedNature Nature { get; }
        public int CapecId { get; }

        public RelatedAttackPatternEntity(RelatedNature nature, int capecId, ExcludeRelatedEntity[] excludeRelateds)
        {
            ExcludeRelateds = excludeRelateds;
            Nature = nature;
            CapecId = capecId;
        }

        public static RelatedAttackPatternEntity Parse(XElement element)
        {
            return new
            (
                element.GetRequiredAttributeAs<RelatedNature>("Nature", RelatedNatureParser.Parse),
                element.GetRequiredAttributeAsInteger("CAPEC_ID"),
                ExcludeRelatedEntity.ParseCollection(element)
            );
        }

        public static RelatedAttackPatternEntity[] ParseCollection(XElement element)
        {
            return element.Elements(CapecNamespaces.DefaultNamespace + "Related_Attack_Pattern")
                .Select(Parse)
                .ToArray();
        }
    }
}