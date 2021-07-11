using System.Xml.Linq;
using ThreatLibrary.Parser.Capec;
using Xunit;

namespace ThreatLibrary.Parser.Test.Capec
{
    public class AttackPatternEntityFacts
    {
        [Fact]
        public void should_parse_attack_patterns()
        {
            XElement element = CapecLoader.LoadAndNormalize("capec_34_attack_pattern.xml");
            AttackPatternEntity attackPatternEntity = AttackPatternEntity.Parse(element);
            
            Assert.Equal(Abstraction.Standard, attackPatternEntity.Abstraction);
            Assert.Equal("An adversary introduces malicious hardware during an update or replacement procedure, allowing for additional compromise or site disruption at the victim location. After deployment, it is not uncommon for upgrades and replacements to occur involving hardware and various replaceable parts. These upgrades and replacements are intended to correct defects, provide additional features, and to replace broken or worn-out parts. However, by forcing or tricking the replacement of a good component with a defective or corrupted component, an adversary can leverage known defects to obtain a desired malicious impact. ", attackPatternEntity.Description);
            Assert.Equal(534, attackPatternEntity.Id);
            Assert.Single(attackPatternEntity.Mitigations);
            Assert.Equal("Malicious Hardware Update", attackPatternEntity.Name);
            Assert.Empty(attackPatternEntity.Notes);
            Assert.Equal(3, attackPatternEntity.Prerequisites.Length);
            Assert.Single(attackPatternEntity.References);
            Assert.Empty(attackPatternEntity.RelatedWeaknesses);
            Assert.Single(attackPatternEntity.ResourcesRequired);
            Assert.Single(attackPatternEntity.SkillsRequired);
            Assert.Empty(attackPatternEntity.TaxonomyMappings);
            Assert.Single(attackPatternEntity.RelatedAttackPatterns);
            Assert.Equal(Status.Stable, attackPatternEntity.Status);
            Assert.Equal(Severity.High, attackPatternEntity.TypicalSeverity);
            Assert.Equal(Likelihood.Low, attackPatternEntity.LikelihoodOfAttack);
            Assert.Equal(3, attackPatternEntity.ExecutionFlow.Length);
            Assert.Empty(attackPatternEntity.AlternateTerms);
            Assert.Empty(attackPatternEntity.Indicators);
            Assert.Single(attackPatternEntity.ExampleInstances);
            Assert.Single(attackPatternEntity.Consequences);
        }
    }
}