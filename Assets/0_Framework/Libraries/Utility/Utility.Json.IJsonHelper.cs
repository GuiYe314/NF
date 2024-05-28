// **********************************************************************
// 文件信息
// 文件名(File Name):                Utility.Json.IJsonHelper.cs
// 作者(Author):                     王云超
// 创建时间(CreateTime):             2024年5月27日 16:59:35
// Unity版本(UnityVersion)：         2021.3.38f1c1
// 脚本描述(Module description):     Json辅助类
// 脚本修改(Script modification):
// **********************************************************************

using System;

namespace F.Librarie
{
    public static partial class Utility
    {
        public static partial class Json
        {
            /// <summary>
            /// JSON 辅助器接口。
            /// </summary>
            public interface IJsonHelper
            {
                /// <summary>
                /// 将对象序列化为 JSON 字符串。
                /// </summary>
                /// <param name="obj">要序列化的对象。</param>
                /// <returns>序列化后的 JSON 字符串。</returns>
                string ToJson(object obj);

                /// <summary>
                /// 将 JSON 字符串反序列化为对象。
                /// </summary>
                /// <typeparam name="T">对象类型。</typeparam>
                /// <param name="json">要反序列化的 JSON 字符串。</param>
                /// <returns>反序列化后的对象。</returns>
                T ToObject<T>(string json);

                /// <summary>
                /// 将 JSON 字符串反序列化为对象。
                /// </summary>
                /// <param name="objectType">对象类型。</param>
                /// <param name="json">要反序列化的 JSON 字符串。</param>
                /// <returns>反序列化后的对象。</returns>
                object ToObject(Type objectType, string json);
            }
        }
    }
}