using System.Collections.Generic;
using TelegramBotTask.EnglishTrainer;

namespace TelegramBotTask
{
    public class AddingVerbs
    {
        private Dictionary<long, IrregVerbsStage> ChatAdding;

        public AddingVerbs()
        {
            ChatAdding = new Dictionary<long, IrregVerbsStage>();
        }

        public void AddFirstStateVerbs(Conversation chat)
        {
            ChatAdding.Add(chat.GetId(), IrregVerbsStage.First);
        }

        public void NextStageVerbs (string message, Conversation chat)
        {
            var currentstate = ChatAdding[chat.GetId()];
            ChatAdding[chat.GetId()] = currentstate + 1;

            if (ChatAdding[chat.GetId()] == IrregVerbsStage.Second)
            {
                chat.IsAddingVerbInProcess = false;
                ChatAdding.Remove(chat.GetId());
            }
        }
        public IrregVerbsStage GetStage(Conversation chat)
        {
            return ChatAdding[chat.GetId()];
        }
    }
}