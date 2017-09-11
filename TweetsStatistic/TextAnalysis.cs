using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace TextStatistics
{
    public class TextAnalysis
    {

        public List<string> GetLettersUsageFrequencyJSONList(string text, int charsLimit)
        {
            var lettersUsageFrequencyList = GetLettersUsageFrequency(text);
            JSONString stringToJSON = new JSONString();
            return stringToJSON.StringToJSON(lettersUsageFrequencyList, charsLimit, new CultureInfo("en-US", true));
        }

        public Dictionary<char, float> GetLettersUsageFrequency(string text)
        {
            Dictionary<char, float> LettersUsageCount = new Dictionary<char, float>();
            var chars = text.ToLower().ToArray();
            int charsCount = 0;
            foreach (char ch in chars)
            {
                if (char.IsLetter(ch))
                {
                    if (LettersUsageCount.ContainsKey(ch))
                    {
                        LettersUsageCount[ch]++;
                    }
                    else
                    {
                        LettersUsageCount.Add(ch, 1);
                    }
                    charsCount++;
                }

            }
          
            return LettersUsageCount.ToDictionary(x => x.Key, x => (float)Math.Round(x.Value / charsCount, 3));
        }

    }
}
