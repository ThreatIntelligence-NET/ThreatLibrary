using System.Linq;
using System.Xml.Linq;
using ThreatLibrary.Parser.Capec.Parsers;
using ThreatLibrary.Parser.XmlParsers;

namespace ThreatLibrary.Parser.Capec
{
    public class TaxonomyMappingEntity
    {
        TaxonomyMappingEntity(string? entryId, string? entryName, TaxonomyMappingFit? mappingFit, TaxonomyName taxonomyName)
        {
            EntryId = entryId;
            EntryName = entryName;
            MappingFit = mappingFit;
            TaxonomyName = taxonomyName;
        }

        public string? EntryId { get; }
        public string? EntryName { get; }
        public TaxonomyMappingFit? MappingFit { get; }
        public TaxonomyName TaxonomyName { get; }
        
        public static TaxonomyMappingEntity Parse(XElement element)
        {
            TaxonomyName taxonomyName = element.GetRequiredAttributeAs("Taxonomy_Name", TaxonomyNameParser.Parse);
            TaxonomyMappingFit? taxonomyMappingFit = element.GetOptionalElementValueAsStruct("Mapping_Fit", TaxonomyMappingFitParser.Parse);
            string? entryId = element.GetOptionalElementValue( CapecNamespaces.DefaultNamespace + "Entry_ID");
            string? entryName = element.GetOptionalElementValue(CapecNamespaces.DefaultNamespace + "Entry_Name");
            return new TaxonomyMappingEntity(entryId, entryName, taxonomyMappingFit, taxonomyName);
        }

        public static TaxonomyMappingEntity[] ParseCollection(XElement element)
        {
            return element.Elements( CapecNamespaces.DefaultNamespace + "Taxonomy_Mapping")
                .Select(Parse)
                .ToArray();
        }
    }
}