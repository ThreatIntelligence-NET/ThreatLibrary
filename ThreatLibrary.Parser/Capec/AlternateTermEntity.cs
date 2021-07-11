using System.Linq;
using System.Xml.Linq;
using ThreatLibrary.Parser.Capec.Parsers;
using ThreatLibrary.Parser.XmlParsers;

namespace ThreatLibrary.Parser.Capec
{
    /// <summary>
    /// The <see cref="AlternateTermEntity"/> type indicates one or more other names used to describe this attack
    /// pattern. The required Term element contains the actual alternate term. The required Description element
    /// provides context for each alternate term by which this attack pattern may be known.
    /// </summary>
    public class AlternateTermEntity
    {
        /// <summary>
        /// The term used to describe the attack pattern. (required)
        /// </summary>
        public string Term { get; }
        
        /// <summary>
        /// The description of the term. (optional)
        /// </summary>
        public string? Description { get; }

        AlternateTermEntity(string term, string? description)
        {
            Term = term;
            Description = description;
        }

        /// <summary>
        /// Parse current entity from XML element.
        /// </summary>
        /// <param name="element">The XML element represents current entity.</param>
        /// <returns>The parsed entity.</returns>
        public static AlternateTermEntity Parse(XElement element)
        {
            string term = element.GetRequiredElementValue(CapecNamespaces.DefaultNamespace + "Term");
            string? description = element.GetOptionalElementAsSingle(
                CapecNamespaces.DefaultNamespace + "Description",
                StructuredTextParser.Parse,
                null
            );

            return new AlternateTermEntity(term, description);
        }

        /// <summary>
        /// Parse a collection of current entity from XML element.
        /// </summary>
        /// <param name="element">The XML element containing a collection of current entity.</param>
        /// <returns>The parsed entity collection.</returns>
        public static AlternateTermEntity[] ParseCollection(XElement element)
        {
            return element.Elements(CapecNamespaces.DefaultNamespace + "Alternate_Term")
                .Select(Parse)
                .ToArray();
        }
    }
}