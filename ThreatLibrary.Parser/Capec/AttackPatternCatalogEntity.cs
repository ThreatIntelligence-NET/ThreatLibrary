using System;
using System.Globalization;
using System.Xml.Linq;
using ThreatLibrary.Parser.XmlParsers;

namespace ThreatLibrary.Parser.Capec
{
    /// <summary>
    /// The <see cref="AttackPatternCatalogEntity"/> is used to hold an enumerated catalog of common attack patterns.
    /// </summary>
    public class AttackPatternCatalogEntity
    {
        // public CategoryEntity[] Categories { get; }
        // public ViewEntity[] Views { get; }
        // public ExternalReferenceEntity[] ExternalReferences { get; }
        
        /// <summary>
        /// The attack patterns contained in the catalog.
        /// </summary>
        public AttackPatternEntity[] AttackPatterns { get; }
        
        /// <summary>
        /// The name of the attack pattern catalog. (required)
        /// </summary>
        public string Name { get; }
        
        /// <summary>
        /// The version of the attack pattern catalog. (required)
        /// </summary>
        public string Version { get; }
        
        /// <summary>
        /// The date of the last update. (required).
        /// </summary>
        public DateTime Date { get; }

        AttackPatternCatalogEntity(string name, string version, DateTime date,  AttackPatternEntity[] attackPatterns)
        {
            AttackPatterns = attackPatterns;
            Name = name;
            Version = version;
            Date = date;
        }
        
        /// <summary>
        /// Parse current entity from XML element.
        /// </summary>
        /// <param name="element">The XML element represents current entity.</param>
        /// <returns>The parsed entity.</returns>
        public static AttackPatternCatalogEntity Parse(XElement element)
        {
            AttackPatternEntity[] attackPatterns = element.GetOptionalElementAsCollection(
                CapecNamespaces.DefaultNamespace + "Attack_Patterns", AttackPatternEntity.ParseCollection);
            string name = element.GetRequiredAttributeValue("Name");
            string version = element.GetRequiredAttributeValue("Version");
            DateTime date = element.GetRequiredAttributeAs("Date",
                v => DateTime.ParseExact(v, "yyyy-MM-dd", CultureInfo.InvariantCulture));
            return new AttackPatternCatalogEntity(name, version, date, attackPatterns);
        }
    }
}