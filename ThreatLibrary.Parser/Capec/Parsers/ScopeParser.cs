namespace ThreatLibrary.Parser.Capec.Parsers
{
    static class ScopeParser
    {
        static readonly EnumValueParser<Scope> Parser = new(
            ("Confidentiality", Scope.Confidentiality),
            ("Integrity", Scope.Integrity),
            ("Availability", Scope.Availability),
            ("Access Control", Scope.AccessControl),
            ("Accountability", Scope.Accountability),
            ("Authentication", Scope.Authentication),
            ("Authorization", Scope.Authorization),
            ("Non-Repudiation", Scope.NonRepudiation),
            ("Other", Scope.Other));

        public static Scope Parse(string value) => Parser.Parse(value);
    }
}