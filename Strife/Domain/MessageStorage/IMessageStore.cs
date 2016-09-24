using Discord_UWP.SharedModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strife.Domain.MessageStorage
{
    public class MessageCreatedEventArgs : EventArgs
    {
        public Message Message { get; set; }
    }

    public class MessageEditedEventArgs : EventArgs
    {
        public Message Message { get; set; }
    }

    public class MessageDeletedEventArgs : EventArgs
    {
        public string MessageId { get; set; }
        public string ChannelId { get; set; }
    }

    public interface IMessageStore
    {
        event EventHandler<MessageCreatedEventArgs> MessageCreated;
        event EventHandler<MessageEditedEventArgs> MessageEdited;
        event EventHandler<MessageDeletedEventArgs> MessageDeleted;

        Task<Message> GetMessageAsync(string channelId, string messageId);
        Task<IEnumerable<Message>> GetMessagesAsync(string channelId);
        Task CreateMessageAsync(string channelId, string content);
        Task EditMessageAsync(string channelId, string messageId, string newContent);
        Task DeleteMessageAsync(string channelId, string messageId);
    }
}
