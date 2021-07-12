namespace ThreatLibrary.Parser.Capec.Parsers
{
    static class StatusParser
    {
        static readonly EnumValueParser<Status> Parser = new(
            ("Deprecated", Status.Deprecated),
            ("Draft", Status.Draft),
            ("Incomplete", Status.Incomplete),
            ("Obsolete", Status.Obsolete),
            ("Stable", Status.Stable),
            ("Usable", Status.Usable));

        public static Status Parse(string value) => Parser.Parse(value);
    }
}