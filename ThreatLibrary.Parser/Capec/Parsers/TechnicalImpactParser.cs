namespace ThreatLibrary.Parser.Capec.Parsers
{
    static class TechnicalImpactParser
    {
        static readonly EnumValueParser<TechnicalImpact> Parser = new(
            ("Modify Data", TechnicalImpact.ModifyData),
            ("Read Data", TechnicalImpact.ReadData),
            ("Unreliable Execution", TechnicalImpact.UnreliableExecution),
            ("Resource Consumption", TechnicalImpact.ResourceConsumption),
            ("Execute Unauthorized Commands", TechnicalImpact.ExecuteUnauthorizedCommands),
            ("Gain Privileges", TechnicalImpact.GainPrivileges),
            ("Bypass Protection Mechanism", TechnicalImpact.BypassProtectionMechanism),
            ("Hide Activities", TechnicalImpact.HideActivities),
            ("Alter Execution Logic", TechnicalImpact.AlterExecutionLogic),
            ("Other", TechnicalImpact.Other));

        public static TechnicalImpact Parse(string value) => Parser.Parse(value);
    }
}