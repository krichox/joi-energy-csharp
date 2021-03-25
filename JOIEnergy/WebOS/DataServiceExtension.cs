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
        /// <summary>
        /// DIService
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddDataService(this IServiceCollection services)
        {
            Type[] types, goalTypes;
            GetDependencyGoalTypes(out types, out goalTypes);
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
                        DIService(services, perGoalType, perImpl, goalType);
                    }
                }
            }
            return services;
        }


        /// <summary>
        /// DIService
        /// DIService
        /// </summary>
        /// <param name="services"></param>
        /// <param name="perGoalType"></param>
        /// <param name="perImpl"></param>
        /// <param name="goalType"></param>
        private static void DIService(IServiceCollection services, Type perGoalType, Type perImpl, Enum goalType)
        {
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

        /// <summary>
        /// 获取需要注入Types
        /// </summary>
        /// <param name="types"></param>
        /// <param name="goalTypes"></param>
        private static void GetDependencyGoalTypes(out Type[] types, out Type[] goalTypes)
        {
            var baseType = typeof(DependencyAttribute);
            //拿到所有Type
            var path = AppDomain.CurrentDomain.RelativeSearchPath ?? AppDomain.CurrentDomain.BaseDirectory;
            var referencedAssemblies = Directory.GetFiles(path, "*.dll").Select(Assembly.LoadFrom).ToArray();
            //获取标记的注解接口
            types = referencedAssemblies
                .SelectMany(a => a.DefinedTypes)
                .Select(type => type.AsType())
                .ToArray();
            goalTypes = types.Where(b => b.GetCustomAttribute(baseType) != null).ToArray();
        }
    }
}
