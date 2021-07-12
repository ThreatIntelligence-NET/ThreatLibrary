namespace ThreatLibrary.Parser.Capec.Parsers
{
    static class SkillLevelParser
    {
        static readonly EnumValueParser<SkillLevel> Parser = new(
            ("High", SkillLevel.High),
            ("Medium", SkillLevel.Medium),
            ("Low", SkillLevel.Low),
            ("Unknown", SkillLevel.Unknown));

        public static SkillLevel Parse(string value) => Parser.Parse(value);
    }
}