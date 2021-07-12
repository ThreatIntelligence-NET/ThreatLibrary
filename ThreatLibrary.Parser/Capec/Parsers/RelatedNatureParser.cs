namespace ThreatLibrary.Parser.Capec.Parsers
{
    static class RelatedNatureParser
    {
        static readonly EnumValueParser<RelatedNature> Parser = new(
            ("ChildOf", RelatedNature.ChildOf),
            ("ParentOf", RelatedNature.ParentOf),
            ("CanFollow", RelatedNature.CanFollow),
            ("CanPrecede", RelatedNature.CanPrecede),
            ("CanAlsoBe", RelatedNature.CanAlsoBe),
            ("PeerOf", RelatedNature.PeerOf));

        public static RelatedNature Parse(string value) => Parser.Parse(value);
    }
}