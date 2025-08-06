using System;

namespace CelaraChallenge
{
    public class WordFinder
    {
        private string[] rows;
        private string[] columns;
        
        public WordFinder(IEnumerable<string> matrix)
        {
            int size = matrix.Count();
            columns = new string[size];
            rows = matrix.ToArray();

            // To optimize words search, I duplicate information by saving the columns as an array of Strings
            for (int c = 0; c < size; c++)
            {
                string element = "";
                for (int r = 0; r < size; r++)
                {
                    element += rows[r][c];
                }
                columns[c] = element;
            }
        }

        public IEnumerable<string> Find(IEnumerable<string> wordstream)
        {
            Dictionary<string, int> searchSummary = new Dictionary<string, int>(StringComparer.OrdinalIgnoreCase);
            Dictionary<string, int> foundSummary = new Dictionary<string, int>(StringComparer.OrdinalIgnoreCase);

            foreach (string word in wordstream)
            {
                if (!searchSummary.ContainsKey(word))
                {
                    int countInRows = 0;
                    int countInColumns = 0;
                    foreach (string row in rows)
                    {
                        countInRows += CountOccurrences(word, row);
                    }
                    foreach (string col in columns)
                    {
                        countInColumns += CountOccurrences(word,col);
                    }
                    int total = countInRows + countInColumns;
                    searchSummary.Add(word, total);

                    if (total > 0)
                    {
                        foundSummary.Add(word, total);
                    }
                }

            }

            // If the number of words found could be very large,
            // to get the 10 most repeated ones, I would use some kind of queue or cache,
            // but given the 64x64 matrix limit, I think that's not necessary.
            var topTenMostRepeated = foundSummary
                .OrderByDescending(kv => kv.Value)
                .Take(10)
                .Select(kv => kv.Key);

            return topTenMostRepeated;
        }


        private int CountOccurrences(string word, string line)
        {
            int count = 0;
            int index = 0;
            while ((index = line.IndexOf(word, index, StringComparison.OrdinalIgnoreCase)) >= 0)
            {
                count++;
                index += word.Length;
            }
            return count;
        }

    }

}
