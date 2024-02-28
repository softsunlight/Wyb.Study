using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wyb.Study.RedPacket.IDao;

namespace Wyb.Study.RedPacket.Dao
{
    public class RedPacketDao : IRedPacketDao
    {
        private readonly ISqlSugarClient _sqlSugarClient;

        public RedPacketDao(ISqlSugarClient sqlSugarClient)
        {
            _sqlSugarClient = sqlSugarClient;
        }

        public async Task<RedPacket.DnEntities.RedPacket> GetAsync(long id)
        {
            return await _sqlSugarClient.CopyNew().Queryable<RedPacket.DnEntities.RedPacket>().Where(p => p.Id == id).FirstAsync();
        }

        public async Task<int> AddAsync(RedPacket.DnEntities.RedPacket redPacket)
        {
            var id = await _sqlSugarClient.CopyNew().Insertable<RedPacket.DnEntities.RedPacket>(redPacket).ExecuteReturnIdentityAsync();
            redPacket.Id = id;
            return id > 0 ? 1 : 0;
        }

        public async Task<int> DeleteAsync(long id)
        {
            return await _sqlSugarClient.CopyNew().Deleteable<RedPacket.DnEntities.RedPacket>(id).ExecuteCommandAsync();
        }

        public async Task<int> DeleteAsync(RedPacket.DnEntities.RedPacket redPacket)
        {
            return await _sqlSugarClient.CopyNew().Deleteable<RedPacket.DnEntities.RedPacket>(redPacket).ExecuteCommandAsync();
        }

        public async Task<int> UpdateAsync(RedPacket.DnEntities.RedPacket redPacket)
        {
            return await _sqlSugarClient.CopyNew().Updateable<RedPacket.DnEntities.RedPacket>(redPacket).ExecuteCommandAsync();
        }
    }
}
