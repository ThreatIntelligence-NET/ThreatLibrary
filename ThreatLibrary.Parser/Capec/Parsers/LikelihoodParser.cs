namespace ThreatLibrary.Parser.Capec.Parsers
{
    static class LikelihoodParser
    {
        static readonly EnumValueParser<Likelihood> Parser = new(
            ("High", Likelihood.High),
            ("Medium", Likelihood.Medium),
            ("Low", Likelihood.Low),
            ("Unknown", Likelihood.Unknown));

        public static Likelihood Parse(string value) => Parser.Parse(value);
    }
}