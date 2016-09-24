using Discord_UWP.SharedModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strife.ViewModels
{
    public class MessageGroupViewModel
    {
        public string UserId { get; set; }
        public string UserName { get; set; }
        public string UserAvatar { get; set; }
        public ObservableCollection<MessageViewModel> Messages { get; set; }

        public MessageGroupViewModel(User user)
        {
            UserId = user.Id;
            UserName = user.Username;
            UserAvatar = $"https://cdn.discordapp.com/avatars/{user.Id}/{user.Avatar}.jpg";
            Messages = new ObservableCollection<MessageViewModel>();
        }

        public void AddMessage(Message message)
        {
            Messages.Add(new MessageViewModel(message));
        }
    }
}
