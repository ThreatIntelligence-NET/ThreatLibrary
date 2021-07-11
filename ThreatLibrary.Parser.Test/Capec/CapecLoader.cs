using System.Xml.Linq;
using ThreatLibrary.Parser.XmlParsers;

namespace ThreatLibrary.Parser.Test.Capec
{
    static class CapecLoader
    {
        public static XElement LoadAndNormalize(string filename)
        {
            return XElement.Load(filename).NormalizeText();
        }
    }
}