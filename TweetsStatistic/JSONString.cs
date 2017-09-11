using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextStatistics
{
   public class JSONString
    {
        readonly int sizeOneBlockFrequency = 11;
      
        public List<string> StringToJSON(Dictionary<char, float> lettersUsageFrequency, int stringCharsLimit, CultureInfo cultureInfo) {
            List<string> LettersUsageFrequencyJSONList = new List<string>();
            var sortedLetters = lettersUsageFrequency.Keys.OrderBy(x=>x.ToString()).ToArray();
            var jsonBuilder = new StringBuilder();
            jsonBuilder.Append("{");
            for (int i = 0;i< sortedLetters.Length; i++)
            {
                if (jsonBuilder.Length <= stringCharsLimit - sizeOneBlockFrequency)
                {
                    if (jsonBuilder.Length != 1) jsonBuilder.Append(",");
                    jsonBuilder.AppendFormat(cultureInfo, "{0}:'{1}'", sortedLetters[i], lettersUsageFrequency[sortedLetters[i]]);
                }
                else
                {
                    jsonBuilder.Append("}");
                    LettersUsageFrequencyJSONList.Add(jsonBuilder.ToString());
                    jsonBuilder.Clear();
                    jsonBuilder.Append("{");
                    i--;
                }
                if (i == sortedLetters.Length - 1)
                {
                    jsonBuilder.Append("}");
                    LettersUsageFrequencyJSONList.Add(jsonBuilder.ToString());
                }


            }
            return LettersUsageFrequencyJSONList;
        }

    }
}
