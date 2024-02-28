using Microsoft.Extensions.Configuration;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Dao
{
    public class SqlSugarClientFactory
    {
        private readonly IConfiguration _configuration;

        public SqlSugarClientFactory(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public ISqlSugarClient Get(string configSectionName)
        {
            return new SqlSugarClient(new ConnectionConfig()
            {
                DbType = DbType.MySql,
                ConnectionString = _configuration.GetSection(configSectionName).Value,
                IsAutoCloseConnection = true
            });
        }
    }
}
