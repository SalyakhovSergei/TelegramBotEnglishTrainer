using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Args;

namespace TelegramBotTask
{
    public class Messenger
    {
        public string CreateTextMessage(Conversation chat)
        {
            var text = "";

            if (chat.GetLAstMessage() == "Привет")
            {
                text = "Hello";
            }
            else
            {
                var delimiter = ",";
                text = "Your messages: " + string.Join(delimiter, chat.GetTextMessages().ToArray());
            }
            
            return text;
        }
    }
}
