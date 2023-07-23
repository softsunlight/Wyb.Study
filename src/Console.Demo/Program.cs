using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using Wyb.Study.Demo1.DbEntities;
using Wyb.Study.Demo1.Services.Interfaces;

namespace Console.Demo
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var serviceCollection = new ServiceCollection();

            //serviceCollection.AddTransient(typeof(IBaseService<>), typeof(BaseService<>));
            //serviceCollection.AddTransient(typeof(IBaseService<>), typeof(BaseService<>));
            //serviceCollection.AddTransient(Type.GetType("Console.Demo.Services.Interfaces.IBaseService`1"), Type.GetType("Console.Demo.Services.Implementations.BaseService`1"));
            //serviceCollection.AddTransient(Type.GetType("Console.Demo.Repositories.Interfaces.IBaseRepository`1"), Type.GetType("Console.Demo.Repositories.Implementations.BaseRepository`1"));

            #region 反射注入
            var assembly = Assembly.Load("Wyb.Study.Demo1");
            var types = assembly.GetTypes();
            foreach (var type in types)
            {
                if (type.FullName.StartsWith("Wyb.Study.Demo1.Services.Implementations") || type.FullName.StartsWith("Wyb.Study.Demo1.Repositories.Implementations"))
                {
                    if (type.IsGenericType)
                    {
                        //System.Console.WriteLine(type.FullName);
                        var interfaces = type.GetInterfaces();
                        foreach (var @interface in interfaces)
                        {
                            System.Console.WriteLine(@interface.Name);
                        }
                        //serviceCollection.AddTransient(interfaces[0], type);
                        serviceCollection.AddTransient(assembly.GetType($"{interfaces[0].Namespace}.{interfaces[0].Name}"), assembly.GetType(type.FullName));
                    }
                    else
                    {

                    }
                }
            }
            #endregion

            var serviceProvider = serviceCollection.BuildServiceProvider();
            var userService = serviceProvider.GetRequiredService<IBaseService<User>>();
            userService.Add(new User());

            System.Console.WriteLine(typeof(IBaseService<>).FullName);
        }
    }
}