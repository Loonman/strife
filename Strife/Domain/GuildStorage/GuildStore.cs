using Discord_UWP.API.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Discord_UWP.SharedModels;

namespace Strife.Domain.GuildStorage
{
    public class GuildStore : IGuildStore
    {
        private readonly IUserService _userService;

        public GuildStore(IUserService userService)
        {
            _userService = userService;
        }

        public async Task<IEnumerable<UserGuild>> GetGuildsAsync()
        {
            return await _userService.GetCurrentUserGuilds();
        }
    }
}
