using System.Collections.Generic;
using System.Threading.Tasks;
using Telegram.Bot;
using TelegramBotTask.EnglishTrainer;

namespace TelegramBotTask.Commands
{
    public class AddVerbs: AbstractCommand
    {
        private ITelegramBotClient botClient;
        private Dictionary<long, Word> Verbs;

        public AddVerbs (ITelegramBotClient botClient)
        {
            CommandText = "/addverb";
            this.botClient = botClient;
            Verbs = new Dictionary<long, Word>();
        }

        public async void StarProcessAsync(Conversation chat)
        {
            Verbs.Add(chat.GetId(), new Word());
            var text = "Введите первое значение глагола";
            await SendCommandText(text, chat.GetId());
        }

        public async void DoForStageAsync(IrregVerbsStage irregVerbsStage, Conversation chat, string message)
        {
            var word = Verbs[chat.GetId()];
            var text = "";
            switch (irregVerbsStage)
            {
                case IrregVerbsStage.First:
                    word.Russian = message;
                    text = "Введите второе значение глагола";
                    break;

                case IrregVerbsStage.Second:
                    word.English = message;
                    text = "Введите тематику";
                    break;

                case IrregVerbsStage.Third:
                    word.Theme = message;
                    text = "Успешно! Слово " + word.Russian + " добавлено в словарь. ";
                    chat.dictionary.Add(word.Russian, word);

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