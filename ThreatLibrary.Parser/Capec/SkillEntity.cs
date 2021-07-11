using System.Linq;
using System.Xml.Linq;
using ThreatLibrary.Parser.Capec.Parsers;
using ThreatLibrary.Parser.XmlParsers;

namespace ThreatLibrary.Parser.Capec
{
    public class SkillEntity
    {
        public string Value { get; }
        public SkillLevel Level { get; }

        public SkillEntity(string value, SkillLevel level)
        {
            Value = value;
            Level = level;
        }

        public static SkillEntity Parse(XElement element)
        {
            var skillLevel = element.GetRequiredAttributeAs("Level", SkillLevelParser.Parse);
            return new SkillEntity
            (
                element.Value,
                skillLevel
            );
        }

        public static SkillEntity[] ParseCollection(XElement element)
        {
            return element.Elements(CapecNamespaces.DefaultNamespace + "Skill")
                .Select(Parse)
                .ToArray();
        }
    }
}