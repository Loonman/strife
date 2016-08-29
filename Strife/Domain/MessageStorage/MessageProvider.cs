using Discord_UWP.SharedModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strife.Domain.MessageStorage
{
    public class MessageProvider
    {
        public event EventHandler<MessageCreatedEventArgs> MessageCreated;
        public event EventHandler<MessageEditedEventArgs> MessageEdited;
        public event EventHandler<MessageDeletedEventArgs> MessageDeleted;

        private readonly string _channelId;
        private readonly IMessageStore _messageStore;

        public MessageProvider(IMessageStore messageStore, string channelId)
        {
            _channelId = channelId;
            _messageStore = messageStore;

            _messageStore.MessageCreated += OnMessageCreated;
            _messageStore.MessageEdited += OnMessageEdited;
            _messageStore.MessageDeleted += OnMessageDeleted;
        }

        public async Task<Message> GetMessageAsync(string messageId)
        {
            return await _messageStore.GetMessageAsync(_channelId, messageId);
        }

        public async Task<IEnumerable<Message>> GetMessagesAsync()
        {
            return await _messageStore.GetMessagesAsync(_channelId);
        }

        public async Task EditMessageAsync(string messageId, string newContent)
        {
            await _messageStore.EditMessageAsync(_channelId, messageId, newContent);
        }

        public async Task DeleteMessageAsync(string messageId)
        {
            await _messageStore.DeleteMessageAsync(_channelId, messageId);
        }

        private void OnMessageDeleted(object sender, MessageDeletedEventArgs e)
        {
            if (_channelId == e.ChannelId)
            {
                MessageDeleted?.Invoke(this, e);
            }
        }

        private void OnMessageEdited(object sender, MessageEditedEventArgs e)
        {
            if (_channelId == e.Message.ChannelId)
            {
                MessageEdited?.Invoke(this, e);
            }
        }

        private void OnMessageCreated(object sender, MessageCreatedEventArgs e)
        {
            if (_channelId == e.Message.ChannelId)
            {
                MessageCreated?.Invoke(this, e);
            }
        }
    }
}
