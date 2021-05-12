using System.Collections.Generic;
using System.Threading.Tasks;
using Telegram.Bot;
using TelegramBotTask.EnglishTrainer;

namespace TelegramBotTask.Commands
{
    public class AddVerbs: AbstractCommand
    {
        private ITelegramBotClient botClient;
        private Dictionary<long, Verb> Verbs;

        public AddVerbs (ITelegramBotClient botClient)
        {
            CommandText = "/addverb";
            this.botClient = botClient;
            Verbs = new Dictionary<long, Verb>();
        }

        public async void StarProcessAsyncVerb(Conversation chat)
        {
            Verbs.Add(chat.GetId(), new Verb());
            var text = "Введите первое значение глагола";
            await SendCommandText(text, chat.GetId());
        }

        public async void DoForStageAsyncVerb(IrregVerbsStage irregVerbsStage, Conversation chat, string message)
        {
            var verb = Verbs[chat.GetId()];
            var text = "";
            switch (irregVerbsStage)
            {
                case IrregVerbsStage.First:
                    verb.First = message;
                    text = "Введите форму глагола в прошедшем времени";
                    break;

                case IrregVerbsStage.Second:
                    verb.Second = message;
                    text = "Введите форму глагола в завершенном времени";
                    break;

                case IrregVerbsStage.Third:
                    verb.Third = message;
                    text = "Успешно! Неправильный глагол " + verb.First + " добавлен в словарь. ";
                    chat.verbDictionary.Add(verb.First, verb);

                    Verbs.Remove(chat.GetId());
                    break;
            }
            await SendCommandText(text, chat.GetId());
        }


        private async Task SendCommandText(string text, long chat)
        {
            await botClient.SendTextMessageAsync(chatId: chat, text: text);
        }

    }
}