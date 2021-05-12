using System.Collections.Generic;
using TelegramBotTask.EnglishTrainer;

namespace TelegramBotTask
{
    public class AddingVerbs
    {
        private Dictionary<long, IrregVerbsStage> ChatAddingVerb;

        public AddingVerbs()
        {
            ChatAddingVerb = new Dictionary<long, IrregVerbsStage>();
        }

        public void AddFirstStateVerbs(Conversation chat)
        {
            ChatAddingVerb.Add(chat.GetId(), IrregVerbsStage.First);
        }

        public void NextStageVerbs (string message, Conversation chat)
        {
            var currentstate = ChatAddingVerb[chat.GetId()];
            ChatAddingVerb[chat.GetId()] = currentstate + 1;

            if (ChatAddingVerb[chat.GetId()] == IrregVerbsStage.Third)
            {
                chat.IsAddingVerbInProcess = false;
                ChatAddingVerb.Remove(chat.GetId());
            }
        }
        public IrregVerbsStage GetStageVerb(Conversation chat)
        {
            return ChatAddingVerb[chat.GetId()];
        }
    }
}