using System.Linq;
using System.Xml.Linq;

namespace ThreatLibrary.Parser.Capec
{
    public class RelationshipsEntity
    {
        public RelationshipEntity[] MemberOf { get; }
        public RelationshipEntity[] HasMember { get; }

        public RelationshipsEntity(RelationshipEntity[] memberOf, RelationshipEntity[] hasMember)
        {
            MemberOf = memberOf;
            HasMember = hasMember;
        }

        public static RelationshipsEntity Parse(XElement element)
        {
            RelationshipEntity[] ParseRelationship(string elementName)
            {
                return element.Elements(CapecNamespaces.DefaultNamespace + elementName)
                    .Select(RelationshipEntity.Parse)
                    .ToArray();
            }

            return new(
                ParseRelationship("Member_Of"),
                ParseRelationship("Has_Member")
            );
        }
    }
}