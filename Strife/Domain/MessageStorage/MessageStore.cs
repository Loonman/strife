using Discord_UWP.API.Channel;
using Discord_UWP.API.Channel.Models;
using Discord_UWP.Gateway;
using Discord_UWP.Gateway.DownstreamEvents;
using Discord_UWP.SharedModels;
using Strife.Utils;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strife.Domain.MessageStorage
{ 
    public class MessageStore : IMessageStore
    {
        private readonly IChannelService _channelService;
        private readonly IGateway _gateway;

        public event EventHandler<MessageCreatedEventArgs> MessageCreated;
        public event EventHandler<MessageEditedEventArgs> MessageEdited;
        public event EventHandler<MessageDeletedEventArgs> MessageDeleted;

        private ConcurrentDictionary<string, Message> messages = new ConcurrentDictionary<string, Message>();

        public MessageStore(IChannelService channelService, IGateway gateway)
        {
            _channelService = channelService;
            _gateway = gateway;
            messages = new ConcurrentDictionary<string, Message>();

            _gateway.MessageCreated += OnMessageCreated;
            _gateway.MessageUpdated += OnMessageUpdated;
            _gateway.MessageDeleted += OnMessageDeleted;
        }

        public async Task<IEnumerable<Message>> GetMessagesAsync(string channelId)
        {
            if (AreMessagesCached(channelId))
            {
                return GetLocalMessages(channelId);
            }
            return await GetServerMessages(channelId);
        }

        public async Task<Message> GetMessageAsync(string channelId, string messageId)
        {
            return await _channelService.GetChannelMessage(channelId, messageId);
        }

        private bool AreMessagesCached(string channelId)
        {
            return messages.Select(p => p.Value)
                           .Where(m => m.ChannelId == channelId)
                           .Any();
        }

        private IEnumerable<Message> GetLocalMessages(string channelId)
        {
            return messages.Select(p => p.Value)
                           .Where(m => m.ChannelId == channelId)
                           .OrderBy(m => m.Timestamp);
        }

        private async Task<IEnumerable<Message>> GetServerMessages(string channelId)
        {
            var serverMessages = await _channelService.GetChannelMessages(channelId);

            return serverMessages.OrderBy(m => m.Timestamp);
        }

        public async Task EditMessageAsync(string channelId, string messageId, string newContent)
        {
            var editMessage = new EditMessage
            {
                Content = newContent
            };

            await _channelService.EditMessage(channelId, messageId, editMessage);
        }

        public async Task DeleteMessageAsync(string channelId, string messageId)
        {
            await DeleteServerMessage(messageId);
            DeleteLocalMessage(messageId);
        }

        private void DeleteLocalMessage(string messageId)
        {
            Message outMessage;
            messages.TryRemove(messageId, out outMessage);
        }

        private async Task DeleteServerMessage(string messageId)
        {
            var message = messages[messageId];

            if (message.Id == messageId)
            {
                await _channelService.DeleteMessage(message.ChannelId, message.Id);
            }
        }

        private void OnMessageCreated(object sender, GatewayEventArgs<Message> e)
        {
           
        }

        private void OnMessageUpdated(object sender, GatewayEventArgs<Message> e)
        {
            
        }

        private void OnMessageDeleted(object sender, GatewayEventArgs<MessageDelete> e)
        {
            DeleteLocalMessage(e.EventData.MessageId);
        }
    }

    public static class MessageStoreExtensions
    {
        public static IObservable<Message> MessagesObservable(this MessageStore messageStore, string channelId)
        {
            //return Observable.FromEvent<EventHandler<Message>, Message>(e => messageStore.MessageChanges += e, e => messageStore.MessageChanges -= e)
            //                 .Where(m => m.ChannelId == channelId);

            return null;
        }
    }
}
