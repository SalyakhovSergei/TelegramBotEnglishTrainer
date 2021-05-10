using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Args;
using Telegram.Bot.Types.ReplyMarkups;

namespace TelegramBotTask.Commands
{
    public class PoemButtonCommand : AbstractCommand, IKeyBoardCommand
    {
        ITelegramBotClient botClient;

        public PoemButtonCommand(ITelegramBotClient botClient)
        {
            this.botClient = botClient;
            CommandText = "/verbs";
        }

        public void AddCallBack(Conversation chat)
        {
            this.botClient.OnCallbackQuery -= Bot_Callback;
            this.botClient.OnCallbackQuery += Bot_Callback;
        }

        private async void Bot_Callback (object sender, CallbackQueryEventArgs e)
        {
            var text = "";
            switch (e.CallbackQuery.Data)
            {
                case "do":
                    text = @"do / did / done";
                    break;
                case "be":
                    text = @"be / was|were / been";
                    break;
                case "go":
                    text = @"go / went / gone";
                    break;
                case "begin":
                    text = @"begin / began / begun";
                    break;
                default:
                    break;
            }

            await botClient.SendTextMessageAsync(e.CallbackQuery.Message.Chat.Id, text);
            await botClient.AnswerCallbackQueryAsync(e.CallbackQuery.Id);
        }
        

        public InlineKeyboardMarkup ReturnKeyBoard()
        {
            var buttonList = new List<InlineKeyboardButton>
            {
                new InlineKeyboardButton
                {
                    Text = "do",
                    CallbackData = "do"
                },

                new InlineKeyboardButton
                {
                    Text = "be",
                    CallbackData = "be"
                },

                new InlineKeyboardButton
                {
                    Text = "go",
                    CallbackData = "go"
                },

                new InlineKeyboardButton
                {
                    Text = "begin",
                    CallbackData = "begin"
                }

            };
            var keyboard = new InlineKeyboardMarkup(buttonList);
            return keyboard;
        }

        public string InformationalMessage()
        {
           return "Выберите глагол";
        }
    }
}
