using System.Xml.Linq;
using ThreatLibrary.Parser.Capec;
using Xunit;

namespace ThreatLibrary.Parser.Test.Capec
{
    public class RelatedAttackPatternEntityFacts
    {
        [Fact]
        public void should_parse_related_attack_pattern()
        {
            XElement element = XElement.Load("capec_34_related_attack_pattern.xml");
            RelatedAttackPatternEntity relatedAttackPattern = RelatedAttackPatternEntity.Parse(element);
            
            Assert.Equal(RelatedNature.ChildOf, relatedAttackPattern.Nature);
            Assert.Equal(137, relatedAttackPattern.CapecId);
            Assert.Single(relatedAttackPattern.ExcludeRelateds);
            Assert.Equal(403, relatedAttackPattern.ExcludeRelateds[0].ExcludeId);
        }

        [Fact]
        public void should_parse_related_attack_pattern_without_excluded_related()
        {
            XElement element = XElement.Load("capec_34_related_attack_pattern_without_exclude_related.xml");
            RelatedAttackPatternEntity relatedAttackPattern = RelatedAttackPatternEntity.Parse(element);
            
            Assert.Equal(RelatedNature.ChildOf, relatedAttackPattern.Nature);
            Assert.Equal(137, relatedAttackPattern.CapecId);
            Assert.Empty(relatedAttackPattern.ExcludeRelateds);
        }

        [Fact]
        public void should_parse_collection()
        {
            XElement element = XElement.Load("capec_34_related_attack_patterns.xml");
            RelatedAttackPatternEntity[] relatedAttackPatterns = RelatedAttackPatternEntity.ParseCollection(element);
            
            Assert.Equal(2, relatedAttackPatterns.Length);
            
            Assert.Equal(RelatedNature.ChildOf, relatedAttackPatterns[0].Nature);
            Assert.Empty(relatedAttackPatterns[0].ExcludeRelateds);
            Assert.Equal(141, relatedAttackPatterns[0].CapecId);
            
            Assert.Equal(RelatedNature.CanPrecede, relatedAttackPatterns[1].Nature);
            Assert.Empty(relatedAttackPatterns[1].ExcludeRelateds);
            Assert.Equal(89, relatedAttackPatterns[1].CapecId);
        }
    }
}