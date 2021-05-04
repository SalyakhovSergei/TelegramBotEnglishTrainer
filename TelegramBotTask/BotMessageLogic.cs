using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Args;
using Telegram.Bot.Types;

namespace TelegramBotTask
{
    public class BotMessageLogic
    {
        private Messenger messenger;
        private Dictionary<long, Conversation> chatlist;
        private ITelegramBotClient telegramBott;

        public BotMessageLogic(ITelegramBotClient botClient)
        {
            messenger = new Messenger();
            chatlist = new Dictionary<long, Conversation>();
            this.telegramBott = botClient;
        }

        public async Task Response(MessageEventArgs e)
        {
            var id = e.Message.Chat.Id;
            if (!chatlist.ContainsKey(id))
            {
                var newchat = new Conversation(e.Message.Chat);
                chatlist.Add(id, newchat);
            }

            var chat = chatlist[id];
            chat.AddMessage(e.Message);

            await SendTextMessage(chat);
        }

        private async Task SendTextMessage (Conversation chat)
        {
            var text = messenger.CreateTextMessage(chat);
            await telegramBott.SendTextMessageAsync(chatId: chat.GetId(), text: text);
        }

        

    }
}
