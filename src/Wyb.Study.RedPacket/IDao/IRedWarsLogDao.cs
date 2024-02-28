using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wyb.Study.RedPacket.IDao
{
    public interface IRedWarsLogDao
    {
        Task<RedPacket.DnEntities.RedWarsLog> GetAsync(long id);

        Task<int> GetCountAsync(long redPakcetId, string userId);

        Task<int> AddAsync(RedPacket.DnEntities.RedWarsLog redWarsLog);

        Task<int> DeleteAsync(long id);

        Task<int> DeleteAsync(RedPacket.DnEntities.RedWarsLog redWarsLog);

        Task<int> UpdateAsync(RedPacket.DnEntities.RedWarsLog redWarsLog);
    }
}
