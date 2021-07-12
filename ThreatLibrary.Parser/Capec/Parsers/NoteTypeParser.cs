namespace ThreatLibrary.Parser.Capec.Parsers
{
    static class NoteTypeParser
    {
        static readonly EnumValueParser<NoteType> Parser = new(
            ("Maintenance", NoteType.Maintenance),
            ("Relationship", NoteType.Relationship),
            ("Research Gap", NoteType.ResearchGap),
            ("Terminology", NoteType.Terminology),
            ("Other", NoteType.Other));

        public static NoteType Parse(string value) => Parser.Parse(value);
    }
}