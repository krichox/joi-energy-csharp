using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace WebOS.JOIEnergy.Dependency.extension
{
    public static class DataServiceExtension
    {
        public static IServiceCollection AddDataService(this IServiceCollection services)
        {
            var baseType = typeof(DependencyAttribute);
            //拿到所有Type
            var path = AppDomain.CurrentDomain.RelativeSearchPath ?? AppDomain.CurrentDomain.BaseDirectory;
            var referencedAssemblies = Directory.GetFiles(path, "*.dll").Select(Assembly.LoadFrom).ToArray();
            //获取标记的注解接口
            var types = referencedAssemblies
                .SelectMany(a => a.DefinedTypes)
                .Select(type => type.AsType())
                .ToArray();
            var goalTypes = types.Where(b => b.GetCustomAttribute(baseType) != null).ToArray();
            foreach (var perGoalType in goalTypes)
            {
                var impleTypes = types.Where(a => a.GetInterfaces().Contains(perGoalType)).ToArray();
                foreach (var perImpl in impleTypes)
                {
                    if (perImpl == null)
                        continue;
                    object[] attrubuteArray = perGoalType.GetCustomAttributes(typeof(DependencyAttribute), true);
                    if (attrubuteArray != null && attrubuteArray.Length > 0)
                    {
                        var goalType = ((DependencyAttribute)attrubuteArray[0]).DependencyType;
                        switch (goalType)
                        {
                            case DependencyEnum.Scope:
                                services.AddScoped(perGoalType, perImpl);
                                break;
                            case DependencyEnum.Singleton:
                                services.AddSingleton(perGoalType, perImpl);
                                break;
                            case DependencyEnum.Trainsient:
                                services.AddTransient(perGoalType, perImpl);
                                break;
                            default:
                                break;
                        }
                    }
                }
            }
            return services;
        }
    }
}
