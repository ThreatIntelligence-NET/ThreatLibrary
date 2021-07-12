namespace ThreatLibrary.Parser.Capec.Parsers
{
    static class SeverityParser
    {
        static readonly EnumValueParser<Severity> Parser = new(
            ("Very High", Severity.VeryHigh),
            ("High", Severity.High),
            ("Medium", Severity.Medium),
            ("Low", Severity.Low),
            ("Very Low", Severity.VeryLow));

        public static Severity Parse(string value) => Parser.Parse(value);
    }
}