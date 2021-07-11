using System.Linq;
using System.Xml.Linq;
using ThreatLibrary.Parser.Capec.Parsers;
using ThreatLibrary.Parser.XmlParsers;

namespace ThreatLibrary.Parser.Capec
{
    public class AttackStepEntity
    {
        public int Step { get; }
        public StepPhase Phase { get; }
        public string Description { get; }
        public TechniqueEntity[] Techniques { get; }

        public AttackStepEntity(int step, StepPhase phase, string description, TechniqueEntity[] techniques)
        {
            Step = step;
            Phase = phase;
            Description = description;
            Techniques = techniques;
        }

        public static AttackStepEntity Parse(XElement element)
        {
            return new(
                step: element.GetRequiredElementValueAsInteger(CapecNamespaces.DefaultNamespace + "Step"),
                phase: element.GetRequiredElementValueAs(CapecNamespaces.DefaultNamespace + "Phase", StepPhaseParser.Parse),
                description: StructuredTextParser.Parse(element.GetRequiredElement(CapecNamespaces.DefaultNamespace + "Description")),
                techniques:TechniqueEntity.ParseCollection(element));
        }

        public static AttackStepEntity[] ParseCollection(XElement element)
        {
            return element
                .Elements(CapecNamespaces.DefaultNamespace + "Attack_Step")
                .Select(Parse)
                .ToArray();
        }
    }
}