using Discord_UWP.SharedModels;
using Strife.Domain;
using Strife.Domain.MessageStorage;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strife.ViewModels
{
    public class MessagesViewModel
    {
        private readonly MessageProvider _messageProvider;

        public ObservableCollection<MessageGroupViewModel> MessageGroups { get; set; } = new ObservableCollection<MessageGroupViewModel>();

        public MessagesViewModel(MessageProvider messageProvider)
        {
            _messageProvider = messageProvider;

            LoadBaseMessages();
        }

        public async void LoadBaseMessages()
        {
            var messages = await _messageProvider.GetMessagesAsync();
            messages.ToList().ForEach(AddMessage);
        }

        public void AddMessage(Message message)
        {
            var lastMessageGroup = MessageGroups.LastOrDefault();

            if (lastMessageGroup != null && lastMessageGroup.UserId == message.User.Id)
            {
                AppendMessageToMessageGroup(lastMessageGroup, message);
            } else
            {
                AddNewMessageGroup(message);
            }
        }

        private void AppendMessageToMessageGroup(MessageGroupViewModel messageGroup, Message message)
        {
            messageGroup.AddMessage(message);
        }

        private void AddNewMessageGroup(Message message)
        {
            var messageGroup = new MessageGroupViewModel(message.User);
            messageGroup.AddMessage(message);

            MessageGroups.Add(messageGroup);
        }
    }
}
