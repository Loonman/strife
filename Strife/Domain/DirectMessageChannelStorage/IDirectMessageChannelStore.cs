using Discord_UWP.SharedModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strife.Domain.DirectMessageChannelStorage
{
    public interface IDirectMessageChannelStore
    {
        Task<IEnumerable<DirectMessageChannel>> GetDirectMessageChannelsAsync();
    }
}
