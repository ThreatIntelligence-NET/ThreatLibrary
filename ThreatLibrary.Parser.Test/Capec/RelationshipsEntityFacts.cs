using System.Xml.Linq;
using ThreatLibrary.Parser.Capec;
using Xunit;

namespace ThreatLibrary.Parser.Test.Capec
{
    public class RelationshipsEntityFacts
    {
        [Fact]
        public void should_parse_relationships()
        {
            XElement element = XElement.Load("capec_34_relationships.xml");
            RelationshipsEntity relationships = RelationshipsEntity.Parse(element);
            
            Assert.Single(relationships.MemberOf);
            Assert.Equal(128, relationships.MemberOf[0].CapecId);

            Assert.Equal(2, relationships.HasMember.Length);
            Assert.Equal(116, relationships.HasMember[0].CapecId);
            Assert.Equal(403, relationships.HasMember[0].ExcludeRelateds[0].ExcludeId);
            Assert.Equal(404, relationships.HasMember[0].ExcludeRelateds[1].ExcludeId);
            Assert.Equal(117, relationships.HasMember[1].CapecId);
            Assert.Empty(relationships.HasMember[1].ExcludeRelateds);
        }

        [Fact]
        public void should_parse_relationships_without_child_element()
        {
            XElement element = XElement.Load("capec_34_relationships_without_child.xml");
            RelationshipsEntity relationships = RelationshipsEntity.Parse(element);
            
            Assert.Empty(relationships.HasMember);
            Assert.Empty(relationships.MemberOf);
        }
    }
}