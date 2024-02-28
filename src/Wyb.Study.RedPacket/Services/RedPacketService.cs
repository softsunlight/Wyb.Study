using CSRedis;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wyb.Study.RedPacket.IDao;
using Wyb.Study.RedPacket.IServices;

namespace Wyb.Study.RedPacket.Services
{
    public class RedPacketService : IRedPacketService
    {
        private readonly IRedPacketDao _redPacketDao;
        private readonly IRedWarsLogDao _redWarsLogDao;
        private readonly CSRedisClient _redisClient;
        private readonly ISqlSugarClient _sqlSugarClient;

        public RedPacketService(IRedPacketDao redPacketDao, IRedWarsLogDao redWarsLogDao, CSRedisClient redisClient, ISqlSugarClient sqlSugarClient)
        {
            _redPacketDao = redPacketDao;
            _redWarsLogDao = redWarsLogDao;
            _redisClient = redisClient;
            _sqlSugarClient = sqlSugarClient;
        }

        /// <summary>
        /// 添加红包
        /// </summary>
        /// <param name="amount">红包金额</param>
        /// <param name="number">红包个数</param>
        /// <returns></returns>
        public async Task<RedPacket.DnEntities.RedPacket> AddAsync(decimal amount, int number)
        {
            RedPacket.DnEntities.RedPacket redPacket = new DnEntities.RedPacket();
            redPacket.Amount = amount;
            redPacket.Number = number;
            redPacket.RemainAmount = amount;
            redPacket.RemainNumber = number;
            var addResult = await _redPacketDao.AddAsync(redPacket);
            await _redisClient.SetAsync($"RedPacket:{redPacket.Id}", redPacket);
            return addResult == 1 ? redPacket : null;
        }

        /// <summary>
        /// 抢红包
        /// </summary>
        /// <param name="redPacketId"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task<int> GrabRedPacketAsync(long redPacketId, string userId)
        {
            if (redPacketId <= 0)
            {
                return 0;
            }
            if (string.IsNullOrWhiteSpace(userId))
            {
                return 0;
            }
            //执行lua脚本
            string luaScript = $"" +
                $"local redPacketId = ARGV[1]\r\n" +
                $"local userId = ARGV[2]\r\n" +
                $"local redPacketKey = 'RedPacket:' .. redPacketId\r\n" +
                $"local redPacketJsonObj = cjson.decode(redis.call('get',redPacketKey))\r\n" +
                $"local redPakcetUserKey = 'RedPakcet:UserId:' .. redPacketId\r\n" +
                $"if(redPacketJsonObj.RemainNumber<=0 or redPacketJsonObj.RemainAmount<=0) then\r\n" +
                $"  return 1\r\n" +
                $"end\r\n" +
                $"if(redis.call('sismember',redPakcetUserKey,userId) == 1) then\r\n" +
                $"  return 2\r\n" +
                $"end\r\n" +
                $"redis.call('sadd',redPakcetUserKey,userId)\r\n" +
                $"return 0";
            long luaResult = (long)await _redisClient.EvalAsync(luaScript, "", redPacketId, userId);
            if (luaResult != 0)
            {
                return 0;
            }

            string guid = Guid.NewGuid().ToString();
            string lockKey = $"Lock:GrabRedPacket:{redPacketId}";
            try
            {
                while (!await _redisClient.SetAsync(lockKey, guid, 10, RedisExistence.Nx))
                {
                    await Task.Delay(10);
                }
                var redPacket = await _redisClient.GetAsync<RedPacket.DnEntities.RedPacket>($"RedPacket:{redPacketId}");
                if (redPacket == null)
                {
                    return 0;
                }
                //var redPacket = await _redPacketDao.GetAsync(redPacketId);
                //if (redPacket == null)
                //{
                //    return 0;
                //}

                //if (redPacket.RemainNumber <= 0 || redPacket.RemainAmount <= 0)
                //{
                //    return 0;
                //}
                ////同一用户不能重复抢
                //if ((await _redWarsLogDao.GetCountAsync(redPacketId, userId)) >= 1)
                //{
                //    return 0;
                //}
                await _sqlSugarClient.Ado.BeginTranAsync();
                if (redPacket.RemainNumber > 1)
                {
                    var amount = Convert.ToInt32(redPacket.RemainAmount * 100);//元转为分
                    int tempAmount = new Random().Next(1, amount - redPacket.RemainNumber + 1);

                    RedPacket.DnEntities.RedWarsLog redWarsLog = new DnEntities.RedWarsLog();
                    redWarsLog.Amount = Convert.ToDecimal(tempAmount / 100.0);
                    redWarsLog.RedPacketId = redPacketId;
                    redWarsLog.UserId = userId;
                    await _sqlSugarClient.Insertable(redWarsLog).ExecuteCommandAsync();

                    redPacket.RemainAmount -= Convert.ToDecimal(tempAmount / 100.0);
                    redPacket.RemainNumber -= 1;
                    redPacket.ModifyTime = DateTime.Now;
                    await _sqlSugarClient.Updateable(redPacket).ExecuteCommandAsync();
                }
                else//只有一个就不用随机了
                {
                    RedPacket.DnEntities.RedWarsLog redWarsLog = new DnEntities.RedWarsLog();
                    redWarsLog.Amount = redPacket.RemainAmount;
                    redWarsLog.RedPacketId = redPacketId;
                    redWarsLog.UserId = userId;
                    await _sqlSugarClient.Insertable(redWarsLog).ExecuteCommandAsync();

                    redPacket.RemainAmount = 0;
                    redPacket.RemainNumber = 0;
                    redPacket.ModifyTime = DateTime.Now;
                    await _sqlSugarClient.Updateable(redPacket).ExecuteCommandAsync();
                }
                if (!await _redisClient.SetAsync($"RedPacket:{redPacketId}", redPacket))
                {
                    throw new Exception("");
                }
                await _sqlSugarClient.Ado.CommitTranAsync();
            }
            catch (Exception ex)
            {
                await _sqlSugarClient.Ado.RollbackTranAsync();
                return 0;
            }
            finally
            {
                //if ((await _redisClient.GetAsync(lockKey)) == guid)
                //{
                //    await _redisClient.DelAsync(lockKey);
                //}
                await _redisClient.EvalAsync("if redis.call('get',ARGV[2])==ARGV[1] then return redis.call('del',ARGV[2]) else return 0 end", "", guid, lockKey);
            }
            return 1;
        }

    }
}
