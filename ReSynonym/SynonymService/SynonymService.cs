using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace ReSynonym.SynonymService
{
    public class SynonymService
    {
        public List<string> GenerateSynonymSuggestions(string query)
        {
            try
            {
                List<string> synonymsOfQuery;

                if (SearchThesaurusForSynonyms(query, out synonymsOfQuery)) 
                    return synonymsOfQuery;
            }
            catch(Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

            return null;
        }

        private bool SearchThesaurusForSynonyms(string query, out List<string> synonymsOfQuery)
        {
            var thesaurusStream = LocateEmbeddedThesaurus();

            using (var reader = new StreamReader(thesaurusStream))
            {
                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();

                    if (!LocatedQueryAtBeginningOfLine(query, line)) continue;

                    synonymsOfQuery = SynonymsOfQuery(reader.ReadLine()); return true;
                }
            }

            synonymsOfQuery = null;
            return false;
        }

        private static Stream LocateEmbeddedThesaurus()
        {
            var assembly = Assembly.GetExecutingAssembly();
            var thesaurusStream = assembly.GetManifestResourceStream("ReSynonym.Thesaurus.th_en_US_new.dat");
            return thesaurusStream;
        }

        public bool LocatedQueryAtBeginningOfLine(string query, String line)
        {
            return !query.Where((letter, iCount) => letter != line[iCount]).Any();
        }

        public List<string> SynonymsOfQuery(string queryLine)
        {
            return queryLine.Split('|').ToList();
        }
    }
}
