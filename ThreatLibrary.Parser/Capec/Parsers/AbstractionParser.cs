namespace ThreatLibrary.Parser.Capec.Parsers
{
    static class AbstractionParser
    {
        static readonly EnumValueParser<Abstraction> Parser = new(
            ("Meta", Abstraction.Meta),
            ("Standard", Abstraction.Standard),
            ("Detailed", Abstraction.Detailed));

        public static Abstraction Parse(string value) => Parser.Parse(value);
    }
}