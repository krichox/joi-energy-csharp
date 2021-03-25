using Newtonsoft.Json;
using System.Collections;

namespace WebOS.JOIEnergy.APIReturn
{

    /// <summary>
    /// API响应数据
    /// </summary>
    [JsonObject(MemberSerialization.OptIn)]
    public class APIReturn
    {
        /// <summary>
        /// 返回码
        /// </summary>
        [JsonProperty("code")]
        public int Code { get; protected set; }

        /// <summary>
        /// 返回信息                                                                                                                                                                                                                                                                         
        /// </summary>
        [JsonProperty("message")]
        public string Message { get; protected set; }
        /// <summary>
        /// 返回数据
        /// </summary>
        [JsonProperty("data")]
        public Hashtable Data { get; protected set; } = new Hashtable();
        /// <summary>
        /// 是否成功
        /// </summary>
        [JsonProperty("success")]
        public bool Success { get { return Code == 0; } }
        /// <summary>
        /// 
        /// </summary>
        public APIReturn()
        {

        }
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="code"></param>
        public APIReturn(int code)
        {
            SetCode(code);
        }
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="message"></param>
        public APIReturn(string message)
        {
            SetMessage(message);
        }
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="code"></param>
        /// <param name="message"></param>
        /// <param name="data"></param>
        public APIReturn(int code, string message, params object[] data)
        {
            SetCode(code).SetMessage(message).SetData(data);
        }

        /// <summary>
        /// 设置返回码
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public APIReturn SetCode(int value)
        {
            Code = value;
            return this;
        }
        /// <summary>
        /// 设置返回信息
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public APIReturn SetMessage(string value)
        {
            Message = value;
            return this;
        }
        /// <summary>
        /// 设置返回数据
        /// </summary>
        /// <param name="values"></param>
        /// <returns></returns>
        public APIReturn SetData(params object[] values)
        {
            Data.Clear();
            return AppendData(values);
        }
        /// <summary>
        /// 添加数据
        /// </summary>
        /// <param name="values"></param>
        /// <returns></returns>
        private APIReturn AppendData(params object[] values)
        {
            if (values == null || values.Length < 2 || values[0] == null) return this;
            for (int i = 0; i < values.Length; i += 2)
            {
                if (values[i] == null) continue;
                Data[values[i]] = i + 1 < values.Length ? values[i + 1] : null;
            }
            return this;
        }

        /// <summary>
        /// 返回实例-成功
        /// </summary>
        public static APIReturn Succeed { get { return new APIReturn(RetCode.Succeed, "操作成功"); } }
        /// <summary>
        /// 返回实例-失败
        /// </summary>
        public static APIReturn Failed { get { return new APIReturn(RetCode.Failed, "操作失败"); } }
        /// <summary>
        /// 返回实例-无权限
        /// </summary>
        public static APIReturn NoPermission { get { return new APIReturn(RetCode.NoPermission, "无权限"); } }
        /// <summary>
        /// 返回实例-记录不存在或无权限访问
        /// </summary>
        public static APIReturn RecordNoExistedOrNoAccess { get { return new APIReturn(RetCode.RecordNoExistedOrNoAccess, "记录不存在或无权限访问"); } }
        /// <summary>
        /// 返回实例-参数错误
        /// </summary>
        public static APIReturn ParameterError { get { return new APIReturn(RetCode.ParameterError, "参数错误"); } }
        /// <summary>
        /// 返回实例-未登录
        /// </summary>
        public static APIReturn NoLogined { get { return new APIReturn(RetCode.NoLogined, "未登录状态"); } }
        /// <summary>
        /// 返回实例-需要二次操作
        /// </summary>
        public static APIReturn NeedAgain { get { return new APIReturn(RetCode.NeedAgain, "需要后续操作"); } }

        /// <summary>
        /// 错误码-通用
        /// </summary>
        public static partial class RetCode
        {
            /// <summary>
            /// 成功
            /// </summary>
            public static int Succeed = 0;
            /// <summary>
            /// 失败
            /// </summary>
            public static int Failed = 99;
            /// <summary>
            /// 无权限
            /// </summary>
            public static int NoPermission = 98;
            /// <summary>
            /// 记录不存在或无权访问
            /// </summary>
            public static int RecordNoExistedOrNoAccess = 98;
            /// <summary>
            /// 参数错误
            /// </summary>
            public static int ParameterError = 97;
            /// <summary>
            /// 未登录状态
            /// </summary>
            public static int NoLogined = 96;
            /// <summary>
            /// 需要二次操作
            /// </summary>
            public static int NeedAgain = 95;
        }
    }
}


