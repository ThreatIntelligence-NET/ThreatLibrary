namespace ThreatLibrary.Parser.Capec.Parsers
{
    static class StakeholderTypeParser
    {
        static readonly EnumValueParser<StakeholderType> Parser = new(
            ("Academic Researchers", StakeholderType.AcademicResearchers),
            ("Applied Researchers", StakeholderType.AppliedResearchers),
            ("Assessment Customers", StakeholderType.AssessmentCustomers),
            ("Assessment Vendors", StakeholderType.AssessmentVendors),
            ("CAPEC Team", StakeholderType.CapecTeam),
            ("Educators", StakeholderType.Educators),
            ("Information Providers", StakeholderType.InformationProviders),
            ("Software Customers", StakeholderType.SoftwareCustomers),
            ("Software Designers", StakeholderType.SoftwareDesigners),
            ("Software Developers", StakeholderType.SoftwareDevelopers),
            ("Software Vendors", StakeholderType.SoftwareVendors),
            ("Other", StakeholderType.Other));

        public static StakeholderType Parse(string value) => Parser.Parse(value);
    }
}