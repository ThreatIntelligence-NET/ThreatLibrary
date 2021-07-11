using System.Linq;
using System.Xml.Linq;
using ThreatLibrary.Parser.XmlParsers;

namespace ThreatLibrary.Parser.Capec
{
    public class ExcludeRelatedEntity
    {
        public int ExcludeId { get; }

        ExcludeRelatedEntity(int excludeId)
        {
            ExcludeId = excludeId;
        }

        public static ExcludeRelatedEntity Parse(XElement element)
        {
            int excludeId = element.GetRequiredAttributeAsInteger("Exclude_ID");
            return new ExcludeRelatedEntity(excludeId);
        }

        public static ExcludeRelatedEntity[] ParseCollection(XElement element)
        {
            return element.Elements(CapecNamespaces.DefaultNamespace + "Exclude_Related")
                .Select(Parse)
                .ToArray();
        }
    }
}