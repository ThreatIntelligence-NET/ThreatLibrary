namespace ThreatLibrary.Parser.Capec.Parsers
{
    static class ViewTypeParser
    {
        static readonly EnumValueParser<ViewType> Parser = new(
            ("Implicit", ViewType.Implicit),
            ("Explicit", ViewType.Explicit),
            ("Graph", ViewType.Graph)); 
        
        public static ViewType Parse(string value) => Parser.Parse(value);
    }
}