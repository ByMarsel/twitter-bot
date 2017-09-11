

using NLog;
using System;

namespace TwitterBot
{
    class Program
    {
        public static Logger log = LogManager.GetCurrentClassLogger();
        static void Main(string[] args)
        {
            try
            {
                Controller controller = new Controller();
                controller.RunTwitterBot();
            }
            catch (Exception ex) {
                log.Error(ex.Message);
                Console.Write("Произошла исключительная ситуация. Вышлите файл: \"twitterBot.log\" разработчику!");

            }
        }
    }
}
