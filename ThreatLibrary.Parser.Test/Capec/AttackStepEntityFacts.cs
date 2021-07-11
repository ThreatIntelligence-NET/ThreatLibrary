using System.Xml.Linq;
using ThreatLibrary.Parser.Capec;
using Xunit;

namespace ThreatLibrary.Parser.Test.Capec
{
    public class AttackStepEntityFacts
    {
        [Fact]
        public void should_parse_attack_step()
        {
            XElement element = XElement.Load("capec_34_attack_step.xml");
            AttackStepEntity step = AttackStepEntity.Parse(element);

            Assert.Equal(2, step.Step);
            Assert.Equal(StepPhase.Exploit, step.Phase);
            Assert.Equal("[Adversary lures victim to clickjacking page] Adversary utilizes some form of temptation, misdirection or coercion to lure the victim to loading and interacting with the clickjacking page in a way that increases the chances that the victim will click in the right areas.", step.Description);
            Assert.Equal(2, step.Techniques.Length);
            Assert.Equal("Lure the victim to the malicious site by sending the victim an e-mail with a URL to the site.", step.Techniques[0].Value);
            Assert.Null(step.Techniques[0].CapecId);
            Assert.Equal("Lure the victim to the malicious site by manipulating URLs on a site trusted by the victim.", step.Techniques[1].Value);
            Assert.Equal(123, step.Techniques[1].CapecId);
        }
        
         [Fact]
        public void should_parse_collection()
        {
            XElement element = XElement.Load("capec_34_execution_flow.xml");
            AttackStepEntity[] steps = AttackStepEntity.ParseCollection(element);

            Assert.Equal(2, steps.Length);

            Assert.Equal(1, steps[0].Step);
            Assert.Equal(StepPhase.Experiment, steps[0].Phase);
            Assert.Equal("[Craft a clickjacking page] The adversary utilizes web page layering techniques to try to craft a malicious clickjacking page", steps[0].Description);
            Assert.Equal(4, steps[0].Techniques.Length);
            Assert.Equal("The adversary leveraged iframe overlay capabilities to craft a malicious clickjacking page", steps[0].Techniques[0].Value);
            Assert.Null(steps[0].Techniques[0].CapecId);
            Assert.Equal("The adversary leveraged Flash file overlay capabilities to craft a malicious clickjacking page", steps[0].Techniques[1].Value);
            Assert.Null(steps[0].Techniques[1].CapecId);
            Assert.Equal("The adversary leveraged Silverlight overlay capabilities to craft a malicious clickjacking page", steps[0].Techniques[2].Value);
            Assert.Null(steps[0].Techniques[2].CapecId);
            Assert.Equal("The adversary leveraged cross-frame scripting to craft a malicious clickjacking page", steps[0].Techniques[3].Value);
            Assert.Null(steps[0].Techniques[3].CapecId);

            Assert.Equal(2, steps[1].Step);
            Assert.Equal(StepPhase.Exploit, steps[1].Phase);
            Assert.Equal("[Adversary lures victim to clickjacking page] Adversary utilizes some form of temptation, misdirection or coercion to lure the victim to loading and interacting with the clickjacking page in a way that increases the chances that the victim will click in the right areas.", steps[1].Description);
            Assert.Equal(3, steps[1].Techniques.Length);
            Assert.Equal("Lure the victim to the malicious site by sending the victim an e-mail with a URL to the site.", steps[1].Techniques[0].Value);
            Assert.Null(steps[1].Techniques[0].CapecId);
            Assert.Equal("Lure the victim to the malicious site by manipulating URLs on a site trusted by the victim.", steps[1].Techniques[1].Value);
            Assert.Null(steps[1].Techniques[1].CapecId);
            Assert.Equal("Lure the victim to the malicious site through a cross-site scripting attack.", steps[1].Techniques[2].Value);
            Assert.Null(steps[1].Techniques[2].CapecId);
        }
    }
}