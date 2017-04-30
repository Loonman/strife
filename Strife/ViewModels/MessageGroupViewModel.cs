using Discord_UWP.SharedModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strife.ViewModels
{
    public class MessageGroupViewModel : NotificationBase
    {
        public string UserId { get; set; }
        public string UserName { get; set; }
        public string UserAvatar { get; set; }
        public ObservableCollection<MessageViewModel> Messages { get; set; } = new ObservableCollection<MessageViewModel>();
        private Collection<MessageViewModel> _Messages { get; set; } = new Collection<MessageViewModel>();

        public MessageGroupViewModel(User user)
        {
            UserId = user.Id;
            UserName = user.Username;
            UserAvatar = $"https://cdn.discordapp.com/avatars/{user.Id}/{user.Avatar}.jpg";
        }

        public void AddMessage(Message message)
        {
            _Messages.Add(new MessageViewModel(message));

            Messages.Add(new MessageViewModel(message));
        }
    }
}
