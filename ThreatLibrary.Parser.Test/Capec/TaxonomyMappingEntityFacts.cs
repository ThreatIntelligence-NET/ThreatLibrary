using System.Xml.Linq;
using ThreatLibrary.Parser.Capec;
using Xunit;

namespace ThreatLibrary.Parser.Test.Capec
{
    public class TaxonomyMappingEntityFacts
    {
        [Fact]
        public void should_parse_taxonomy_mapping()
        {
            XElement taxonomyMappingElement = XElement.Load("./capec_34_taxonomy_mapping.xml");
            TaxonomyMappingEntity taxonomyMapping = TaxonomyMappingEntity.Parse(taxonomyMappingElement);
            
            Assert.Equal(TaxonomyName.ATTACK, taxonomyMapping.TaxonomyName);
            Assert.Equal("1566.003", taxonomyMapping.EntryId);
            Assert.Equal("Phishing:Spearfishing via Service", taxonomyMapping.EntryName);
            Assert.Null(taxonomyMapping.MappingFit);
        }

        [Fact]
        public void should_parse_taxonomy_mappings()
        {
            XElement taxonomyMappingElements = XElement.Load("./capec_34_taxonomy_mapping_collection.xml");
            TaxonomyMappingEntity[] taxonomyMappings = TaxonomyMappingEntity.ParseCollection(taxonomyMappingElements);
            
            Assert.Equal(2, taxonomyMappings.Length);
            
            Assert.Equal(TaxonomyName.WASC, taxonomyMappings[0].TaxonomyName);
            Assert.Equal("36", taxonomyMappings[0].EntryId);
            Assert.Equal("SSI Injection", taxonomyMappings[0].EntryName);
            Assert.Null(taxonomyMappings[0].MappingFit);
            
            Assert.Equal(TaxonomyName.OWASPAttacks, taxonomyMappings[1].TaxonomyName);
            Assert.Null(taxonomyMappings[1].EntryId);
            Assert.Equal("Server-Side Includes (SSI) Injection", taxonomyMappings[1].EntryName);
            Assert.Null(taxonomyMappings[1].MappingFit);
        }
    }
}