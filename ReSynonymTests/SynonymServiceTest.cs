using ReSynonym.SynonymService;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ReSynonymTests
{
    [TestClass]
    public class SynonymServiceTest
    {
        [TestMethod]
        public void Ensure_Can_Locate_Synonyms_From_Inside_Thesaurus()
        {
            var synonymService = new SynonymService();
            var synonyms = synonymService.GenerateSynonymSuggestions("successful");

            Assert.IsTrue(synonyms.Contains("victorious"));
        }
    }
}
