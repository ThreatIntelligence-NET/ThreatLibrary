using System.Xml.Linq;
using ThreatLibrary.Parser.Capec.Parsers;
using Xunit;

namespace ThreatLibrary.Parser.Test.Capec
{
    public class PrerequisitesEntityFacts
    {
        [Fact]
        public void should_parse_collection()
        {
            XElement skillElement = XElement.Load("capec_34_prerequisites.xml");
            string[] prerequisites = PrerequisitesParser.ParseCollection(skillElement);

            Assert.Equal(2, prerequisites.Length);
            Assert.Equal("The targeted site must contain hidden fields to be modified.", prerequisites[0]);
            Assert.Equal("The targeted site must not validate the hidden fields with backend processing.", prerequisites[1]);
        }
    }
}