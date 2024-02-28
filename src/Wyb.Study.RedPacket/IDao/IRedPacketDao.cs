using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wyb.Study.RedPacket.IDao
{
    public interface IRedPacketDao
    {
        Task<RedPacket.DnEntities.RedPacket> GetAsync(long id);

        Task<int> AddAsync(RedPacket.DnEntities.RedPacket redPacket);

        Task<int> DeleteAsync(long id);

        Task<int> DeleteAsync(RedPacket.DnEntities.RedPacket redPacket);

        Task<int> UpdateAsync(RedPacket.DnEntities.RedPacket redPacket);
    }
}
