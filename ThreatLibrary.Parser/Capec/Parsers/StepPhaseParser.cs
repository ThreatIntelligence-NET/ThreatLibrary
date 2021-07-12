namespace ThreatLibrary.Parser.Capec.Parsers
{
    static class StepPhaseParser
    {
        static readonly EnumValueParser<StepPhase> Parser = new(
            ("Explore", StepPhase.Explore),
            ("Experiment", StepPhase.Experiment),
            ("Exploit", StepPhase.Exploit));

        public static StepPhase Parse(string value) => Parser.Parse(value);
    }
}