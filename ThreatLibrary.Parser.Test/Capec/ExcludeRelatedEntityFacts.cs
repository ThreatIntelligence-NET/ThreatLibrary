using System.Xml.Linq;
using ThreatLibrary.Parser.Capec;
using Xunit;

namespace ThreatLibrary.Parser.Test.Capec
{
    public class ExcludeRelatedEntityFacts
    {
        [Fact]
        public void should_parser_exclude_related()
        {
            XElement element = XElement.Load("capec_34_exclude_related.xml");
            ExcludeRelatedEntity excludeRelatedEntity = ExcludeRelatedEntity.Parse(element);
            
            Assert.Equal(403, excludeRelatedEntity.ExcludeId);
        }
    }
}