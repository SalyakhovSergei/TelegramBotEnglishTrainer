using System;
using System.IO;
using Telegram.Bot;
using Telegram.Bot.Args;

namespace TelegramBotTask
{
    class Program
    {
        static void Main(string[] args)
        {
            BotWorker bot = new BotWorker();

            bot.Initialize();
            bot.Start();
            string command;

            do
            {
                command = Console.ReadLine();
            }
            while (command != "stop"); 

            bot.Stop();

        }
    }
}
