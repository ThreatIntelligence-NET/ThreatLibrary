using System;
using System.Xml.Linq;
using ThreatLibrary.Parser.Capec;
using Xunit;

namespace ThreatLibrary.Parser.Test.Capec
{
    public class AttackPatternCatalogEntityFacts
    {
        [Fact]
        public void should_parse_catalog()
        {
            XElement element = XElement.Load("capec_v3.4.xml");
            AttackPatternCatalogEntity attackPatternCatalog = AttackPatternCatalogEntity.Parse(element);
            
            Assert.Equal(new DateTime(2020, 12, 17), attackPatternCatalog.Date);
            Assert.Equal("CAPEC", attackPatternCatalog.Name);
            Assert.Equal("3.4", attackPatternCatalog.Version);
            Assert.Equal(581, attackPatternCatalog.AttackPatterns.Length);
        }
    }
}