using System.Linq;
using System.Xml.Linq;
using ThreatLibrary.Parser.Capec.Parsers;
using ThreatLibrary.Parser.XmlParsers;

namespace ThreatLibrary.Parser.Capec
{
    public class NoteEntity
    {
        public NoteType Type { get; }
        public string Value { get; }

        public NoteEntity(NoteType type, string value)
        {
            Type = type;
            Value = value;
        }

        public static NoteEntity Parse(XElement element)
        {
            return new(
                element.GetRequiredAttributeAs("Type", NoteTypeParser.Parse),
                StructuredTextParser.Parse(element));
        }

        public static NoteEntity[] ParseCollection(XElement element)
        {
            return element.Elements(CapecNamespaces.DefaultNamespace + "Note")
                .Select(Parse)
                .ToArray();
        }
    }
}