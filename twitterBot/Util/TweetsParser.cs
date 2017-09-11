using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using TweetSharp;

namespace TwitterBot
{
    public class TweetsParser
    {
        public string Parse(List<TwitterStatus> userTweets)
        {
            string tweetText = "";
            if (userTweets == null|| userTweets.Count==0) {
                return "";
            }
            var tweetsString = new StringBuilder();
            foreach (var tweet in userTweets)
            {
                tweetText = tweet.Text;
                tweetText = tweetText.Replace("RT ","");
                tweetsString.Append(tweetText);
            }
            return DeleteUserName(DeleteURL(tweetsString.ToString().ToLower()));
        }

        public string DeleteURL(string str, string uri = @"https://")
        {
            Regex regex = new Regex(@"(http|https|ftp)://([A-Za-z0-9][A-Za-z0-9_-]*(?:.[A-Za-z0-9][A-Za-z0-9_-]*)+):?(d+)?/?");
            return regex.Replace(str, "");
        }
        public string DeleteUserName(string text)
        {
            Regex regex = new Regex(@"@[A-Za-z0-9_]{1,15}");
            return regex.Replace(text, "");
        }
    }
}
