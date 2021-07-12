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
                "[Craft a malicious website]<xhtml:p xmlns:xhtml=\"http://www.w3.org/1999/xhtml\">The attacker crafts a malicious website to which they plan to lure the victim who is using the vulnerable target system. The malicious website does two things:</xhtml:p><xhtml:div style=\"margin-left:10px;\" xmlns:xhtml=\"http://www.w3.org/1999/xhtml\"><xhtml:ul><xhtml:li>1. Contains a hook that intercepts incoming JSON objects, reads their contents and forwards the contents to the server controlled by the attacker (via a new XMLHttpRequest).</xhtml:li><xhtml:li>2. Uses the script tag with a URL in the source that requests a JSON object from the vulnerable target system. Once the JSON object is transmitted to the victim's browser, the malicious code (as described in step 1) intercepts that JSON object, steals its contents, and forwards to the attacker.</xhtml:li></xhtml:ul></xhtml:div><xhtml:p xmlns:xhtml=\"http://www.w3.org/1999/xhtml\">This attack step leverages the fact that the same origin policy in the browser does not protect JavaScript originating from one domain from setting up an environment to intercept and access JSON objects arriving from a completely different domain.</xhtml:p>",
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
                "<xhtml:div xmlns:xhtml=\"http://www.w3.org/1999/xhtml\"><xhtml:span>Level 1</xhtml:span><xhtml:div><xhtml:span>Level 2</xhtml:span><xhtml:div><xhtml:span>Level 3</xhtml:span><xhtml:div><xhtml:span>Level 4</xhtml:span><xhtml:div><xhtml:span>Level 5</xhtml:span><xhtml:div><xhtml:span>Level 6</xhtml:span><xhtml:div><xhtml:span>Level 7</xhtml:span><xhtml:div><xhtml:span>Level 8</xhtml:span><xhtml:div><xhtml:span>Level 9</xhtml:span><xhtml:div><xhtml:span>Level 10</xhtml:span><xhtml:div><xhtml:span>Level 11</xhtml:span></xhtml:div></xhtml:div></xhtml:div></xhtml:div></xhtml:div></xhtml:div></xhtml:div></xhtml:div></xhtml:div></xhtml:div></xhtml:div>",
                content);
        }
    }
}