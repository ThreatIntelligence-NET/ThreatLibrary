using System.Linq;
using System.Xml.Linq;
using ThreatLibrary.Parser.Capec.Parsers;
using ThreatLibrary.Parser.XmlParsers;

namespace ThreatLibrary.Parser.Capec
{
    public class ConsequenceEntity
    {
        public Scope[] Scopes { get; }
        public TechnicalImpact[] Impacts { get; }
        public Likelihood? Likelihood { get; }
        public string? Note { get; }
        public string? ConsequenceId { get; }

        public ConsequenceEntity(
            Scope[] scopes,
            TechnicalImpact[] impacts,
            Likelihood? likelihood,
            string? note,
            string? consequenceId
        )
        {
            Scopes = scopes;
            Impacts = impacts;
            Likelihood = likelihood;
            Note = note;
            ConsequenceId = consequenceId;
        }

        public static ConsequenceEntity Parse(XElement element)
        {
            Scope[] scopes = element
                .Elements(CapecNamespaces.DefaultNamespace + "Scope")
                .Select(e => ScopeParser.Parse(e.Value))
                .ToArray();
            TechnicalImpact[] impacts = element.Elements(CapecNamespaces.DefaultNamespace + "Impact")
                .Select(e => TechnicalImpactParser.Parse(e.Value))
                .ToArray();
            Likelihood? likelihood = element.GetOptionalElementValueAsStruct(
                CapecNamespaces.DefaultNamespace + "Likelihood",
                LikelihoodParser.Parse
            );
            string? note = element.GetOptionalElementAsSingle(
                CapecNamespaces.DefaultNamespace + "Note",
                StructuredTextParser.Parse,
                null
            );
            string? consequenceId = element.GetOptionalAttributeValue("Consequence_ID");

            return new ConsequenceEntity(scopes, impacts, likelihood, note, consequenceId);
        }

        public static ConsequenceEntity[] ParseCollection(XElement element)
        {
            return element.Elements(CapecNamespaces.DefaultNamespace + "Consequence")
                .Select(Parse)
                .ToArray();
        }
    }
}