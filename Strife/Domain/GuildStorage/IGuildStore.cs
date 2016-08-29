using Discord_UWP.SharedModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strife.Domain.GuildStorage
{
    public interface IGuildStore
    {
        Task<IEnumerable<UserGuild>> GetGuildsAsync();
    }
}
