using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TelegramBotTask.Commands
{
    public class StopTrainingCommand : AbstractCommand, IChatTextCommandWithAction
    {
        public StopTrainingCommand()
        {
            CommandText = "/stop";
        }
        public bool DoAction(Conversation chat)
        {
            chat.IsTraningInProcess = false;
            return !chat.IsTraningInProcess;
        }

        public string ReturnText()
        {
            return "Тренировка остановлена!";
        }
    }
}
