using System.Xml.Linq;
using ThreatLibrary.Parser.Capec;
using Xunit;

namespace ThreatLibrary.Parser.Test.Capec
{
    public class RelationshipEntityFacts
    {
        [Fact]
        public void should_parse_relationship()
        {
            XElement element = XElement.Load("capec_34_relationship.xml");
            RelationshipEntity relationship = RelationshipEntity.Parse(element);

            Assert.Equal(2, relationship.ExcludeRelateds.Length);
            Assert.Equal(403, relationship.ExcludeRelateds[0].ExcludeId);
            Assert.Equal(404, relationship.ExcludeRelateds[1].ExcludeId);
            Assert.Equal(116, relationship.CapecId);
        }

        [Fact]
        public void should_parse_relationship_without_exclude_related()
        {
            XElement element = XElement.Load("capec_34_relationship_without_exclude_relation.xml");
            RelationshipEntity relationship = RelationshipEntity.Parse(element);
            
            Assert.Empty(relationship.ExcludeRelateds);
            Assert.Equal(116, relationship.CapecId);
        }
    }
}