using System.Xml.Linq;
using ThreatLibrary.Parser.Capec;
using Xunit;

namespace ThreatLibrary.Parser.Test.Capec
{
    public class SkillEntityFacts
    {
        [Fact]
        public void should_parse_skill()
        {
            XElement skillElement = XElement.Load("capec_34_skill.xml");
            SkillEntity skill = SkillEntity.Parse(skillElement);
            
            Assert.Equal(SkillLevel.High, skill.Level);
            Assert.Equal("If the attacker has to perform SQL injection blindly", skill.Value);
        }
        
        [Fact]
        public void should_parse_skill_collection()
        {
            XElement skillElements = XElement.Load("capec_34_skill_collection.xml");
            SkillEntity[] skills = SkillEntity.ParseCollection(skillElements);
            
            Assert.Equal(2, skills.Length);
            
            Assert.Equal(SkillLevel.Low, skills[0].Level);
            Assert.Equal("To modify file name or file extension", skills[0].Value);
            
            Assert.Equal(SkillLevel.Medium, skills[1].Level);
            Assert.Equal("To use misclassification to force the Web server to disclose configuration information, source, or binary data", skills[1].Value);
        }
    }
}