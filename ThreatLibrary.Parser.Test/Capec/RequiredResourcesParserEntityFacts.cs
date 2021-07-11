using System.Xml.Linq;
using ThreatLibrary.Parser.Capec.Parsers;
using Xunit;

namespace ThreatLibrary.Parser.Test.Capec
{
    public class RequiredResourcesParserEntityFacts
    {
        [Fact]
        public void should_parse_resources()
        {
            XElement skillElement = XElement.Load("capec_34_required_resources.xml");
            string[] resources = RequiredResourcesParser.ParseCollection(skillElement);

            Assert.Equal(2, resources.Length);
            Assert.Equal("Ability to deploy software on network.", resources[0]);
            Assert.Equal("Ability to communicate synchronously or asynchronously with server.", resources[1]);
        }
    }
}