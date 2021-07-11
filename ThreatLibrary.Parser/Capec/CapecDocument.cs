using System;
using System.Xml.Linq;

namespace ThreatLibrary.Parser.Capec
{
    public static class CapecDocument
    {
        public static AttackPatternCatalogEntity Load(string filename)
        {
            XDocument document = XDocument.Load(filename);
            XElement? rootElement = document.Root;
            if (rootElement == null)
            {
                throw new FormatException("The document contains no root element.");
            }
            
            return AttackPatternCatalogEntity.Parse(rootElement);
        }
    }
}