using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Args;
using Telegram.Bot.Types.ReplyMarkups;

namespace TelegramBotTask
{
    public class BotWorker
    {
        private ITelegramBotClient telegramBott;
        private BotMessageLogic logic;

        public void Initialize()
        {
            telegramBott = new TelegramBotClient(BotCredentials.BotToken);
            logic = new BotMessageLogic(telegramBott);
        }

        public void Start()
        {
            telegramBott.OnMessage += Bot_onMessage;
            telegramBott.StartReceiving();
        }

        public void Stop()
        {
            telegramBott.StopReceiving();
        }

        private async void Bot_onMessage(object sender, MessageEventArgs e)
        {
            if(e.Message != null)
            {
                await logic.Response(e);
            }
        }
        
    }
}
