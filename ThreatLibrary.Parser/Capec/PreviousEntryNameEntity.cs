using System;
using System.Globalization;
using System.Linq;
using System.Xml.Linq;
using ThreatLibrary.Parser.Capec.Parsers;
using ThreatLibrary.Parser.XmlParsers;

namespace ThreatLibrary.Parser.Capec
{
    public class PreviousEntryNameEntity
    {
        public string Value { get; }
        public DateTime Date { get; }

        public PreviousEntryNameEntity(string value, DateTime date)
        {
            Value = value;
            Date = date;
        }

        public static PreviousEntryNameEntity Parse(XElement element)
        {
            string value = StructuredTextParser.Parse(element);
            DateTime date = element.GetRequiredAttributeAs("Date",
                v => DateTime.ParseExact(v, "yyyy-MM-dd", CultureInfo.InvariantCulture));
            return new(value, date);
        }

        public static PreviousEntryNameEntity[] ParseCollection(XElement element)
        {
            return element.Elements(CapecNamespaces.DefaultNamespace + "Previous_Entry_Name")
                .Select(Parse)
                .ToArray();
        }
    }
}