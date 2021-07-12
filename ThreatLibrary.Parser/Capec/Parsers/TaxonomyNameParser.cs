namespace ThreatLibrary.Parser.Capec.Parsers
{
    static class TaxonomyNameParser
    {
        static readonly EnumValueParser<TaxonomyName> Parser = new(
            ("ATTACK", TaxonomyName.ATTACK),
            ("WASC", TaxonomyName.WASC),
            ("OWASP Attacks", TaxonomyName.OWASPAttacks));

        public static TaxonomyName Parse(string value) => Parser.Parse(value);
    }
}