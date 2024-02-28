using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wyb.Study.RedPacket.IServices
{
    public interface IRedPacketService
    {
        /// <summary>
        /// 添加红包
        /// </summary>
        /// <param name="amount">红包金额</param>
        /// <param name="number">红包个数</param>
        /// <returns></returns>
        Task<RedPacket.DnEntities.RedPacket> AddAsync(decimal amount, int number);

        /// <summary>
        /// 抢红包
        /// </summary>
        /// <param name="redPacketId"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        Task<int> GrabRedPacketAsync(long redPacketId, string userId);
    }
}
