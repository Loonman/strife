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

        public ObservableCollection<MessageViewModel> Messages { get; set; } = new ObservableCollection<MessageViewModel>();

        public MessagesViewModel(MessageProvider messageProvider)
        {
            _messageProvider = messageProvider;

            LoadBaseMessages();
        }

        public async void LoadBaseMessages()
        {
            var messages = await _messageProvider.GetMessagesAsync();

            messages.Select(MessageViewModel.FromMessage)
                    .ToList()
                    .ForEach(m => Messages.Add(m));
        }
    }
}
