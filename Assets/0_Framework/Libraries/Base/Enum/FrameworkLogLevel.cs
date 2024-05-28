// **********************************************************************
// 文件信息
// 文件名(File Name):                FrameworkLogLevel.cs
// 作者(Author):                     王云超
// 创建时间(CreateTime):             2024年5月27日 15:06:49
// Unity版本(UnityVersion)：         2021.3.38f1c1
// 脚本描述(Module description):     框架日志级别枚举
// 脚本修改(Script modification):
// **********************************************************************
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace F.Librarie
{
    /// <summary>
    /// 框架日志等级。
    /// </summary>
    public enum FrameworkLogLevel : byte
    {
        /// <summary>
        /// 调试。
        /// </summary>
        Debug = 0,

        /// <summary>
        /// 信息。
        /// </summary>
        Info,

        /// <summary>
        /// 警告。
        /// </summary>
        Warning,

        /// <summary>
        /// 错误。
        /// </summary>
        Error,

        /// <summary>
        /// 严重错误。
        /// </summary>
        Fatal
    }
}