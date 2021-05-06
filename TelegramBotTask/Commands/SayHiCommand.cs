using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TelegramBotTask.Commands
{
    public class SayHiCommand : AbstractCommand, IChatTextCommand
    {
        public SayHiCommand()
        {
            CommandText = "/saymehi";
        }
        public string ReturnText()
        {
            return "привет";
        }
    }
}
