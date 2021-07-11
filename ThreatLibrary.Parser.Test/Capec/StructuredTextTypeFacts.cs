using System;
using System.Xml.Linq;
using ThreatLibrary.Parser.Capec.Parsers;
using Xunit;

namespace ThreatLibrary.Parser.Test.Capec
{
    public class StructuredTextTypeFacts
    {
        static string LoadAndParse(string fileName)
        {
            XElement element = XElement.Load(fileName);
            return StructuredTextParser.Parse(element);
        }
        
        [Fact]
        public void should_parse_structured_xhtml_text()
        {
            string content = LoadAndParse("capec_34_structured_xhtml.xml");
            
            Assert.Equal(
                "[Craft a malicious website]" + Environment.NewLine + 
                "The attacker crafts a malicious website to which they plan to lure the victim who is using the vulnerable target system. The malicious website does two things:" + Environment.NewLine +
                "1. Contains a hook that intercepts incoming JSON objects, reads their contents and forwards the contents to the server controlled by the attacker (via a new XMLHttpRequest)." + Environment.NewLine +
                "2. Uses the script tag with a URL in the source that requests a JSON object from the vulnerable target system. Once the JSON object is transmitted to the victim's browser, the malicious code (as described in step 1) intercepts that JSON object, steals its contents, and forwards to the attacker." + Environment.NewLine +
                "This attack step leverages the fact that the same origin policy in the browser does not protect JavaScript originating from one domain from setting up an environment to intercept and access JSON objects arriving from a completely different domain." + Environment.NewLine,
                content);
        }

        [Fact]
        public void should_parse_structured_xhtml_text_without_internal_elements()
        {
            string content = LoadAndParse("capec_34_structured_xhtml_no_element.xml");
            
            Assert.Equal("Only contains plain text.", content);
        }

        [Fact]
        public void should_ignore_deeply_nested_elements()
        {
            string content = LoadAndParse("capec_34_structured_xhtml_too_deep.xml");
            
            Assert.Equal(
                "Level 1" + Environment.NewLine +
                "Level 2" + Environment.NewLine +
                "Level 3" + Environment.NewLine +
                "Level 4" + Environment.NewLine +
                "Level 5" + Environment.NewLine +
                "Level 6" + Environment.NewLine +
                "Level 7" + Environment.NewLine +
                "Level 8Level 9Level 10Level 11" + Environment.NewLine,
                content);
        }
    }
}