namespace ThreatLibrary.Parser.Capec.Parsers
{
    static class TaxonomyMappingFitParser
    {
        static readonly EnumValueParser<TaxonomyMappingFit> Parser = new(
            ("Exact", TaxonomyMappingFit.Exact),
            ("CAPEC More Abstract", TaxonomyMappingFit.CapecMoreAbstract),
            ("CAPEC More Specific", TaxonomyMappingFit.CapecMoreSpecific),
            ("Imprecise", TaxonomyMappingFit.Imprecise),
            ("Perspective", TaxonomyMappingFit.Perspective));

        public static TaxonomyMappingFit Parse(string value) => Parser.Parse(value);
    }
}