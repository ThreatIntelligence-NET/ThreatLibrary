using System;
using System.Xml.Linq;
using ThreatLibrary.Parser.XmlParsers;
using Xunit;

namespace ThreatLibrary.Parser.Test.XmlParsers
{
    public class XmlExtensionsFacts
    {
        [Fact]
        public void should_get_required_attribute_value()
        {
            const string expectedValue = "attribute value";
            
            var elementWithAttribute = new XElement(
                "element",
                new XAttribute("attribute", expectedValue)
            );

            Assert.Equal(expectedValue, elementWithAttribute.GetRequiredAttributeValue("attribute"));
        }

        [Fact]
        public void should_throw_if_attribute_does_not_exist()
        {
            var elementWithoutAttribute = new XElement("element");

            Assert.Throws<FormatException>(() => elementWithoutAttribute.GetRequiredAttributeValue("attribute"));
        }
    }
}