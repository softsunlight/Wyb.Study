using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wyb.Study.RedPacket.IDao;

namespace Wyb.Study.RedPacket.Dao
{
    public class RedWarsLogDao : IRedWarsLogDao
    {
        private readonly ISqlSugarClient _sqlSugarClient;

        public RedWarsLogDao(ISqlSugarClient sqlSugarClient)
        {
            _sqlSugarClient = sqlSugarClient;
        }

        public async Task<RedPacket.DnEntities.RedWarsLog> GetAsync(long id)
        {
            return await _sqlSugarClient.CopyNew().Queryable<RedPacket.DnEntities.RedWarsLog>().Where(p => p.Id == id).FirstAsync();
        }

        public async Task<int> GetCountAsync(long redPakcetId, string userId)
        {
            return await _sqlSugarClient.CopyNew().Queryable<RedPacket.DnEntities.RedWarsLog>().Where(p => p.RedPacketId == redPakcetId && p.UserId == userId).CountAsync();
        }

        public async Task<int> AddAsync(RedPacket.DnEntities.RedWarsLog redWarsLog)
        {
            var id = await _sqlSugarClient.CopyNew().Insertable<RedPacket.DnEntities.RedWarsLog>(redWarsLog).ExecuteReturnIdentityAsync();
            redWarsLog.Id = id;
            return id > 0 ? 1 : 0;
        }

        public async Task<int> DeleteAsync(long id)
        {
            return await _sqlSugarClient.CopyNew().Deleteable<RedPacket.DnEntities.RedWarsLog>(id).ExecuteCommandAsync();
        }

        public async Task<int> DeleteAsync(RedPacket.DnEntities.RedWarsLog redWarsLog)
        {
            return await _sqlSugarClient.CopyNew().Deleteable<RedPacket.DnEntities.RedWarsLog>(redWarsLog).ExecuteCommandAsync();
        }

        public async Task<int> UpdateAsync(RedPacket.DnEntities.RedWarsLog redWarsLog)
        {
            return await _sqlSugarClient.CopyNew().Updateable<RedPacket.DnEntities.RedWarsLog>(redWarsLog).ExecuteCommandAsync();
        }
    }
}
