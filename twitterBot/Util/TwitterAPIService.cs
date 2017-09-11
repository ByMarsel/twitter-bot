using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using TweetSharp;


namespace TwitterBot
{
    class TwitterAPIService
    {
        private static string customerKey = Settings.Default.customer_key ;
        private static string customerKeySecret = Settings.Default.customer_key_secret;
        private static string accessToken = Settings.Default.access_token;
        private static string accessTokenSecret = Settings.Default.access_token_secret;

        public delegate void ServerResponseAction(TwitterResponse response);
        public event ServerResponseAction Action; 

        private TwitterService tweetService  = new TwitterService(customerKey, customerKeySecret, accessToken, accessTokenSecret);

        public List<TwitterStatus> GetRecentUserTweets(string userName) {
            var userTweets = tweetService.ListTweetsOnUserTimeline(new ListTweetsOnUserTimelineOptions { ScreenName = userName, Count = 5 });
            Action(tweetService.Response);
            if (userTweets != null) {
                return (List<TwitterStatus>) userTweets;
            }            
            return null;
        }
        public long SendTweet(string text) {         
                  
            tweetService.SendTweet(new SendTweetOptions {Status = text});
            Action(tweetService.Response);
            if (tweetService.Response.StatusCode == System.Net.HttpStatusCode.OK)
            {              
                return GetTweetId(tweetService.Response);               
            }
           
            return -1;
        }
        public long ReplyToStatusId(string text,long statusId)
        {   
            tweetService.SendTweet(new SendTweetOptions {InReplyToStatusId = statusId, Status =  text });
            Action(tweetService.Response);
            if (tweetService.Response.StatusCode == System.Net.HttpStatusCode.OK)
            {               
                return GetTweetId(tweetService.Response);
            }
            
            return -1;
        }
        private long GetTweetId(TwitterResponse responce) {          
            var jObject = Newtonsoft.Json.Linq.JObject.Parse(responce.Response);         
            return long.Parse(jObject["id"].ToString());
        }
       

    }
}
