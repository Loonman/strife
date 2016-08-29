using Discord_UWP.SharedModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strife.ViewModels
{
    public class MessageViewModel
    {
        public string Id { get; set; }
        public string Username { get; set; }
        public string Content { get; set; }

        public static MessageViewModel FromMessage(Message message)
        {
            return new MessageViewModel
            {
                Id = message.Id,
                Username = message.User.Username,
                Content = message.Content
            };
        }  
    }
}
