using System.Xml.Linq;
using ThreatLibrary.Parser.Capec;
using Xunit;

namespace ThreatLibrary.Parser.Test.Capec
{
    public class RelatedWeaknessEntityFacts
    {
        [Fact]
        public void should_parse_related_weakness()
        {
            XElement element = XElement.Load("capec_34_related_weakness.xml");
            RelatedWeaknessEntity relatedWeakness = RelatedWeaknessEntity.Parse(element);
            
            Assert.Equal(276, relatedWeakness.CweId);
        }

        [Fact]
        public void should_parse_related_weakness_collection()
        {
            XElement element = XElement.Load("capec_34_related_weakness_collection.xml");
            RelatedWeaknessEntity[] relatedWeaknesses = RelatedWeaknessEntity.ParseCollection(element);
            
            Assert.Equal(3, relatedWeaknesses.Length);
            Assert.Equal(276, relatedWeaknesses[0].CweId);
            Assert.Equal(277, relatedWeaknesses[1].CweId);
            Assert.Equal(278, relatedWeaknesses[2].CweId);
        }
    }
}