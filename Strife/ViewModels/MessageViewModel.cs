using Discord_UWP.SharedModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strife.ViewModels
{
    public class MessageViewModel : NotificationBase
    {
        private readonly Message _message;

        public string Id => _message.Id;
        public string Content => _message.Content;

        public MessageViewModel(Message message)
        {
            _message = message;
        }
    }
}
