using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wyb.Study.Application.Contracts.Responses;

namespace Wyb.Study.Application.Contracts.IServices
{
    /// <summary>
    /// kafka示例服务接口
    /// </summary>
    public interface IKafkaDemoService
    {
        /// <summary>
        /// 发送基本消息，没有指定key
        /// </summary>
        /// <returns></returns>
        Task<DataResponse<dynamic>> SendMessageAsync();

        /// <summary>
        /// 将不同Key的消息发送到不同的分区
        /// </summary>
        /// <returns></returns>
        Task<DataResponse<dynamic>> SendMessageTestDifferentKeyAsync();

        /// <summary>
        /// 将消息发送到指定的分区
        /// </summary>
        /// <returns></returns>
        Task<DataResponse<dynamic>> SendMessageToAssignPartitionAsync();

        /// <summary>
        /// kafka事务
        /// </summary>
        /// <returns></returns>
        Task<DataResponse<dynamic>> TransactionAsync();
    }
}
