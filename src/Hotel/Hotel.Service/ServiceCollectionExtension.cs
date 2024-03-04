using Microsoft.Extensions.DependencyInjection;
using Hotel.Service.implements;
using Hotel.Service.interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hotel.Dao.interfaces;
using Hotel.Dao.implements;
using Hotel.Dao;

namespace Hotel.Service
{
    public static class ServiceCollectionExtension
    {
        public static IServiceCollection AddServicesAndDaos(this IServiceCollection services)
        {
            services.AddSingleton(typeof(ISugarBaseDao<>), typeof(SugarBaseDao<>));

            services.AddSingleton(typeof(IBaseService<>), typeof(BaseService<>));

            services.AddSingleton<SqlSugarClientFactory>();

            return services;
        }
    }
}
