using System.Linq;
using System.Xml.Linq;
using ThreatLibrary.Parser.Capec.Parsers;
using ThreatLibrary.Parser.XmlParsers;
using static ThreatLibrary.Parser.Capec.CapecNamespaces;

namespace ThreatLibrary.Parser.Capec
{
    public class AttackPatternEntity
    {
        AttackPatternEntity(
            int id,
            string name,
            Abstraction abstraction,
            Status status,
            AlternateTermEntity[] alternateTerms,
            ConsequenceEntity[] consequences,
            string description,
            string[] exampleInstances,
            string[] indicators,
            Likelihood? likelihoodOfAttack,
            string[] mitigations,
            NoteEntity[] notes,
            string[] prerequisites,
            PreviousEntryNameEntity[] previousEntryNames,
            ReferenceEntity[] references,
            RelatedAttackPatternEntity[] relatedAttackPatterns,
            RelatedWeaknessEntity[] relatedWeaknesses,
            string[] resourcesRequired,
            SkillEntity[] skillsRequired,
            TaxonomyMappingEntity[] taxonomyMappings,
            Severity? typicalSeverity,
            AttackStepEntity[] executionFlow)
        {
            Description = description;
            LikelihoodOfAttack = likelihoodOfAttack;
            TypicalSeverity = typicalSeverity;
            ExecutionFlow = executionFlow;
            PreviousEntryNames = previousEntryNames;
            Consequences = consequences;
            ExampleInstances = exampleInstances;
            Indicators = indicators;
            AlternateTerms = alternateTerms;
            RelatedAttackPatterns = relatedAttackPatterns;
            Prerequisites = prerequisites;
            SkillsRequired = skillsRequired;
            ResourcesRequired = resourcesRequired;
            Mitigations = mitigations;
            RelatedWeaknesses = relatedWeaknesses;
            TaxonomyMappings = taxonomyMappings;
            References = references;
            Notes = notes;
            Id = id;
            Name = name;
            Abstraction = abstraction;
            Status = status;
        }

        public string[] Indicators { get; }
        public AlternateTermEntity[] AlternateTerms { get; }
        public string Description { get; }
        public Likelihood? LikelihoodOfAttack { get; }
        public Severity? TypicalSeverity { get; }
        public RelatedAttackPatternEntity[] RelatedAttackPatterns { get; }
        public string[] Prerequisites { get; }
        public SkillEntity[] SkillsRequired { get; }
        public string[] ResourcesRequired { get; }
        public string[] Mitigations { get; }
        public RelatedWeaknessEntity[] RelatedWeaknesses { get; }
        public TaxonomyMappingEntity[] TaxonomyMappings { get; }
        public ReferenceEntity[] References { get; }
        public NoteEntity[] Notes { get; }
        public int Id { get; }
        public string Name { get; }
        public Abstraction Abstraction { get; }
        public Status Status { get; }
        public AttackStepEntity[] ExecutionFlow { get; }
        public string[] ExampleInstances { get; }
        public ConsequenceEntity[] Consequences { get; }
        public PreviousEntryNameEntity[] PreviousEntryNames { get; }

        public static AttackPatternEntity[] ParseCollection(XElement element)
        {
            return element
                .Elements(DefaultNamespace + "Attack_Pattern")
                .Select(Parse)
                .ToArray();
        }

        public static AttackPatternEntity Parse(XElement element)
        {
            string description = element.GetRequiredElementAsSingle(
                DefaultNamespace + "Description",
                e => e.Value
            );
            Likelihood? likelihood = element.GetOptionalElementValueAsStruct(
                DefaultNamespace + "Likelihood_Of_Attack",
                LikelihoodParser.Parse
            );
            Severity? typicalSeverity = element.GetOptionalElementValueAsStruct(
                DefaultNamespace + "Typical_Severity",
                SeverityParser.Parse
            );
            RelatedAttackPatternEntity[] relatedAttackPatterns = element.GetOptionalElementAsCollection(
                DefaultNamespace + "Related_Attack_Patterns",
                RelatedAttackPatternEntity.ParseCollection
            );
            string[] prerequisites = element.GetOptionalElementAsCollection(
                DefaultNamespace + "Prerequisites",
                PrerequisitesParser.ParseCollection
            );
            SkillEntity[] skills = element.GetOptionalElementAsCollection(
                DefaultNamespace + "Skills_Required",
                SkillEntity.ParseCollection
            );
            string[] resourcesRequired = element.GetOptionalElementAsCollection(
                DefaultNamespace + "Resources_Required",
                RequiredResourcesParser.ParseCollection
            );
            string[] mitigations = element.GetOptionalElementAsCollection(
                DefaultNamespace + "Mitigations",
                MitigationsParser.ParseCollection
            );
            RelatedWeaknessEntity[] relatedWeaknesses = element.GetOptionalElementAsCollection(
                DefaultNamespace + "Related_Weaknesses",
                RelatedWeaknessEntity.ParseCollection
            );
            TaxonomyMappingEntity[] taxonomyMappings = element.GetOptionalElementAsCollection(
                DefaultNamespace + "Taxonomy_Mappings",
                TaxonomyMappingEntity.ParseCollection
            );
            ReferenceEntity[] references = element.GetOptionalElementAsCollection(
                DefaultNamespace + "References",
                ReferenceEntity.ParseCollection
            );
            NoteEntity[] notes = element.GetOptionalElementAsCollection(
                DefaultNamespace + "Notes",
                NoteEntity.ParseCollection
            );
            int capecId = element.GetRequiredAttributeAsInteger("ID");
            string name = element.GetRequiredAttributeValue("Name");
            Abstraction abstraction = element.GetRequiredAttributeAs("Abstraction", AbstractionParser.Parse);
            Status status = element.GetRequiredAttributeAs("Status", StatusParser.Parse);
            AttackStepEntity[] executionFlow = element.GetOptionalElementAsCollection(
                DefaultNamespace + "Execution_Flow",
                AttackStepEntity.ParseCollection
            );
            AlternateTermEntity[] alternateTerms = element.GetOptionalElementAsCollection(
                DefaultNamespace + "Alternate_Terms",
                AlternateTermEntity.ParseCollection
            );
            string[] indicators = element.GetOptionalElementAsCollection(
                DefaultNamespace + "Indicators",
                IndicatorsParser.ParseCollection);
            string[] exampleInstances = element.GetOptionalElementAsCollection(
                DefaultNamespace + "Example_Instances",
                ExampleInstancesParser.ParseCollection
            );
            ConsequenceEntity[] consequences = element.GetOptionalElementAsCollection(
                DefaultNamespace + "Consequences",
                ConsequenceEntity.ParseCollection
            );

            PreviousEntryNameEntity[] previousEntryNames = element.GetOptionalElementAsCollection(
                DefaultNamespace + "Content_History", PreviousEntryNameEntity.ParseCollection);

            return new AttackPatternEntity(
                capecId,
                name,
                abstraction,
                status,
                alternateTerms,
                consequences,
                description,
                exampleInstances,
                indicators,
                likelihood,
                mitigations,
                notes,
                prerequisites,
                previousEntryNames,
                references,
                relatedAttackPatterns,
                relatedWeaknesses,
                resourcesRequired,
                skills,
                taxonomyMappings,
                typicalSeverity,
                executionFlow
            );
        }
    }
}