using Microsoft.Extensions.DependencyInjection;
using Shop.Dao;
using Shop.Dao.implements;
using Shop.Dao.interfaces;
using Shop.Service.implements;
using Shop.Service.interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Service
{
    public static class ServiceCollectionExtension
    {
        public static IServiceCollection AddServicesAndDaos(this IServiceCollection services)
        {
            services.AddSingleton(typeof(ISugarBaseDao<>), typeof(SugarBaseDao<>));

            services.AddSingleton(typeof(IShopDao), typeof(ShopDao));

            services.AddSingleton(typeof(IBaseService<>), typeof(BaseService<>));

            services.AddSingleton(typeof(IShopService), typeof(ShopService));

            services.AddSingleton<SqlSugarClientFactory>();

            return services;
        }
    }
}
