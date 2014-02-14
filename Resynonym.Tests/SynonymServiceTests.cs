using NUnit.Framework;
using ReSynonym.SynonymService;

namespace Resynonym.Tests
{
    public class SynonymServiceTests
    {
        [Test]
        public void Ensure_Can_Locate_Synonyms_From_Inside_Thesaurus()
        {
            var synonymService = new SynonymService();
            var synonyms = synonymService.GenerateSynonymSuggestions("successful");

            Assert.IsTrue(synonyms.Contains("victorious"));
        }
    }
}
