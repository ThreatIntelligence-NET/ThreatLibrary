namespace ThreatLibrary.Parser.Capec.Parsers
{
    static class ImportanceParser
    {
        static readonly EnumValueParser<Importance> Parser = new(
            ("Normal", Importance.Normal),
            ("Critical", Importance.Critical));

        public static Importance Parse(string value) => Parser.Parse(value);
    }
}