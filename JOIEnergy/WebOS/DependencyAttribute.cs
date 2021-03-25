using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebOS.JOIEnergy.Dependency
{
    /// <summary>
    ///  DI注解
    /// </summary>
    [AttributeUsage(AttributeTargets.Interface, AllowMultiple = false)]
    public class DependencyAttribute : Attribute
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="dependencyType"></param>
        public DependencyAttribute(DependencyEnum dependencyType)
        {
            DependencyType = dependencyType;
        }

        /// <summary>
        /// 注入类型
        /// </summary>
        public Enum DependencyType { get; set; }
    }

    /// <summary>
    /// 注入类型Enum
    /// </summary>
    public enum DependencyEnum
    {
        /// <summary>
        /// 每次请求都创建
        /// </summary>
        Trainsient = 1,

        /// <summary>
        ///  单次请求同一个
        /// </summary>
        Scope = 2,

        /// <summary>
        /// 单例注入，服务启动时同一个
        /// </summary>
        Singleton = 3
    }
}
