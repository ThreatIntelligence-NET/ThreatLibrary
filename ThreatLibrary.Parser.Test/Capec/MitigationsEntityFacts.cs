using System.Xml.Linq;
using ThreatLibrary.Parser.Capec.Parsers;
using Xunit;

namespace ThreatLibrary.Parser.Test.Capec
{
    public class MitigationsEntityFacts
    {
        [Fact]
        public void should_parse_collection()
        {
            XElement skillElement = XElement.Load("capec_34_mitigations.xml");
            string[] mitigations = MitigationsParser.ParseCollection(skillElement);

            Assert.Equal(2, mitigations.Length);
            Assert.Equal("Ensure that protocols have specific limits of scale configured.", mitigations[0]);
            Assert.Equal("Specify expectations for capabilities and dictate which behaviors are acceptable when resource allocation reaches limits.", mitigations[1]);
        }
    }
}