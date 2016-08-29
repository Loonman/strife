using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Discord_UWP.SharedModels;
using Discord_UWP.API.User;

namespace Strife.Domain.DirectMessageChannelStorage
{
    public class DirectMessageChannelStore : IDirectMessageChannelStore
    {
        private readonly IUserService _userService;

        public async Task<IEnumerable<DirectMessageChannel>> GetDirectMessageChannelsAsync()
        {
            return await _userService.GetCurrentUserDirectMessageChannels();
        }
    }
}
