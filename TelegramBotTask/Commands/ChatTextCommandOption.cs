using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TelegramBotTask.Commands
{
    public abstract class ChatTextCommandOption :AbstractCommand
    {
        public override bool CheckMessage(string message)
        {
            return message.StartsWith(CommandText);
        }
        public string ClearMessageFromCommand(string message)
        {
            return message.Substring(CommandText.Length + 1);
        }
    }
}
