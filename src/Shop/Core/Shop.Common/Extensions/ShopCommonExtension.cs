using Microsoft.Extensions.DependencyInjection;
using Shop.Common.Factories;
using Shop.Common.Repositories.Implements;
using Shop.Common.Repositories.Interfaces;
using Shop.Common.Services.Implements;
using Shop.Common.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Common.Extensions
{
    public static class ShopCommonExtension
    {
        public static IServiceCollection AddShopCommon(this IServiceCollection services)
        {
            services.AddSingleton<SqlSugarClientFactory>();

            services.AddTransient(typeof(ISugarBaseDao<>), typeof(SugarBaseDao<>));

            services.AddTransient(typeof(IBaseService<>), typeof(BaseService<>));

            return services;
        }
    }
}
