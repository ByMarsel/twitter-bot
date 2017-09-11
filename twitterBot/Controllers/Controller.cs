
using System;
using TextStatistics;
using TweetSharp;


namespace TwitterBot
{
   public class Controller
    {
        
        public void RunTwitterBot() {
            var twitterService = new TwitterAPIService();
            var tweetsParser = new TweetsParser();
            var textAnalysis = new TextAnalysis();
            twitterService.Action += TwitterApiResponseToNotifyUser;
            string userName;
            while (true)
            {
                Console.WriteLine("Введите ник пользователя:");
                userName = Console.ReadLine();
                while (userName == "@" || string.IsNullOrWhiteSpace(userName))
                {
                    Console.WriteLine("Введен некорректный ник пользователя, попробуйте еще раз:");
                    userName = Console.ReadLine();
                }



                if (userName.IndexOf("") != -1) userName = userName.Replace("@", "");
                Program.log.Trace($"Получили ник пользователя: {userName}");
                Program.log.Trace($"Выполняем загрузку и парсинг твитов: {userName}");
                string tweets = tweetsParser.Parse(twitterService.GetRecentUserTweets(userName));
                Program.log.Trace($"Парсинг твитов завершен: {userName}");
                if (!string.IsNullOrEmpty(tweets))
                {
                    Program.log.Trace($"Конвертируем твиты, в лист JSON'оф: {userName}");
                    var list = textAnalysis.GetLettersUsageFrequencyJSONList(tweets, 140);
                    Program.log.Trace($"Конвертирование завершено: {userName}");
                    Program.log.Trace($"Выполняем отправку шапки статистики (твит): {userName}");
                    long id = twitterService.SendTweet(userName+ ", статистика для последних 5 твитов:");
                    Program.log.Trace($"Шапка отправлена. ID = {id}: {userName}");
                    Console.WriteLine(userName + ", статистика для последних 5 твитов: ");
                    for (int i = 0; i < list.Count; i++)
                    {
                        Console.WriteLine(list[i]);
                        twitterService.ReplyToStatusId(list[i], id);
                        Program.log.Trace($"JSON статистики: {list[i]}.ID родительского твита = {id}: {userName}");
                    }
                    Program.log.Trace($"Cтатистика выгружена: {userName}");
                }
                else
                {
                    Program.log.Trace($"Не удалось найти твиты пользователя: {userName}");
                    Console.WriteLine($"Не удалось найти твиты пользователя: {userName}");
                }
               
                Console.WriteLine();
            }
        }
        public void TwitterApiResponseToNotifyUser(TwitterResponse responce)
        {
            if (responce.StatusCode==0)
            {
                Console.WriteLine("Не удалось подключиться к https://api.twitter.ru, проверьте соединение интернет или доступность данного ресурса.");
                return;
            }
            if (responce.Error != null)
            {
                switch (responce.Error.Code)
                {
                    case 34:
                        Console.WriteLine("Пользователь с не найден. Проверьте корректно ли введен ник:)");
                        break;
                    case 63:
                        Console.WriteLine("Пользователь с данным ником заблокирован");
                        break;
                    case 187:
                        Console.WriteLine("Статистика по пользователю уже была выслана. Повторите процедуру после того как у пользователя будет 5 новых твитов!");
                        break;
                    default:
                        Console.WriteLine($"Не удалось выполнить отправку твитов по причине: {responce.Error}");
                        break;
                }
            }
                                    
        }
        
    }
}
