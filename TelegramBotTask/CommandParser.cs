﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot.Types.ReplyMarkups;
using TelegramBotTask.Commands;

namespace TelegramBotTask
{
    public class CommandParser
    {
        private List<IChatcommand> Command;
        private AddingController addingController;

        public CommandParser()
        {
            Command = new List<IChatcommand>();
            addingController = new AddingController();
        }

        public void AddCommand(IChatcommand chatcommand)
        {
            Command.Add(chatcommand);
        }
        public bool IsMessageCommad(string message)
        {
            return Command.Exists(x => x.CheckMessage(message));
        }
        public bool IsTextCommand(string message)
        {
            var command = Command.Find(x => x.CheckMessage(message));
            return command is IKeyBoardCommand;
        }
        public bool IsButtonCommand(string message)
        {
            var command = Command.Find(x => x.CheckMessage(message));
            return command is IKeyBoardCommand;
        }

        public string GetMessageText(string message, Conversation chat)
        {
            var command = Command.Find(x => x.CheckMessage(message)) as IChatTextCommand;
            if (command is IChatTextCommandWithAction)
            {
                if (!(command as IChatTextCommandWithAction).DoAction(chat))
                { 
                    return "Ошибка выполнения команды!";
                };
            }

            return command.ReturnText();
        }

        public string GetInformationalMessage(string message)
        {
            var command = Command.Find(x => x.CheckMessage(message)) as IKeyBoardCommand;

            return command.InformationalMessage();
        }

        public InlineKeyboardMarkup GetKeyboard (string message)
        {
            var command = Command.Find(x => x.CheckMessage(message)) as IKeyBoardCommand;

            return command.ReturnKeyBoard();
        }

        public void AddCallBack(string message, Conversation chat)
        {
            var command = Command.Find(x => x.CheckMessage(message)) as IKeyBoardCommand;
            command.AddCallBack(chat);
        }

        public bool IsAddingCommand (string message)
        {
            var command = Command.Find(x => x.CheckMessage(message));
            return command is AddWordCommand;
        }

        public void StartAddingWord(string message, Conversation chat)
        {
            var command = Command.Find(x => x.CheckMessage(message)) as AddWordCommand;
            addingController.AddFirstState(chat);
            command.StarProcessAsync(chat);
        }

        public void NextStage(string message, Conversation chat)
        {
            var command = Command.Find(x => x is AddWordCommand) as AddWordCommand;
            command.DoForStageAsync(addingController.GetStage(chat), chat, message);
            addingController.NextStage(message, chat);
        }

        public void ContinueTraining(string message, Conversation chat)
        {
            var command = Command.Find(x => x is TrainingCommand) as TrainingCommand;
            command.NextStepAsync(chat, message);
        }
    }
}