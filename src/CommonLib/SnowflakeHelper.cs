using Snowflake.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonLib
{
    public class SnowflakeHelper
    {
        public static long GetId()
        {
            return new IdWorker(Guid.NewGuid().GetHashCode(), Guid.NewGuid().GetHashCode()).NextId();
        }
    }
}
