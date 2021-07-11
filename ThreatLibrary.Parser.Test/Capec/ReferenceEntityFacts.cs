using System.Xml.Linq;
using ThreatLibrary.Parser.Capec;
using Xunit;

namespace ThreatLibrary.Parser.Test.Capec
{
    public class ReferenceEntityFacts
    {
        [Fact]
        public void should_parse_reference()
        {
            XElement element = XElement.Load("capec_34_reference.xml");
            ReferenceEntity reference = ReferenceEntity.Parse(element);
            
            Assert.Equal("REF-22", reference.ExternalReferenceId);
            Assert.Equal("DNS Cache Poisoning", reference.Section);
        }

        [Fact]
        public void should_parse_reference_without_section()
        {
            XElement element = XElement.Load("capec_34_reference_without_section.xml");
            ReferenceEntity reference = ReferenceEntity.Parse(element);
            
            Assert.Equal("REF-22", reference.ExternalReferenceId);
            Assert.Null(reference.Section);
        }

        [Fact]
        public void should_parse_collection()
        {
            XElement element = XElement.Load("capec_34_references.xml");
            ReferenceEntity[] references = ReferenceEntity.ParseCollection(element);
            
            Assert.Equal(2, references.Length);
            Assert.Equal("REF-23", references[0].ExternalReferenceId);
            Assert.Equal("DNS Threats & Weaknesses of the Domain Name System", references[0].Section);
            Assert.Equal("REF-27", references[1].ExternalReferenceId);
            Assert.Null(references[1].Section);
        }
    }
}