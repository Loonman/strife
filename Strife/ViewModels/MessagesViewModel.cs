using Discord_UWP.SharedModels;
using Strife.Domain;
using Strife.Domain.MessageStorage;
using Strife.Utils;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strife.ViewModels
{
    public class MessagesViewModel : NotificationBase
    {
        private readonly MessageProvider _messageProvider;
        private string messageFieldContent = "";

        public string MessageFieldContent
        {
            get { return messageFieldContent; }
            set { SetProperty(ref messageFieldContent, value); }
        }

        public ObservableCollection<MessageGroupViewModel> MessageGroups { get; set; } = new ObservableCollection<MessageGroupViewModel>();

        public MessagesViewModel(MessageProvider messageProvider)
        {
            _messageProvider = messageProvider;
            _messageProvider.MessageCreated += OnMessageCreated;
            LoadBaseMessages();
        }

        private void OnMessageCreated(object sender, MessageCreatedEventArgs e)
        {
            ThreadingUtils.DoOnMainThread(() => AddMessage(e.Message));
        }

        public async void OnSendClick()
        {
            if (!String.IsNullOrEmpty(MessageFieldContent))
            {
                var task = _messageProvider.CreateMessageAsync(MessageFieldContent);
                MessageFieldContent = "";
                await task;
            }
        }

        private async void LoadBaseMessages()
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
