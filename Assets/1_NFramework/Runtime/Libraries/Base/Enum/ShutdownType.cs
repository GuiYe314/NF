// **********************************************************************
// 文件信息
// 文件名(File Name):                ShutdownType.cs
// 作者(Author):                     王云超
// 创建时间(CreateTime):             2024年5月27日 14:29:01
// Unity版本(UnityVersion)：         2021.3.38f1c1
// 脚本描述(Module description):     枚举
// 脚本修改(Script modification):
// **********************************************************************
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NF.Librarie
{/// <summary>
 /// 关闭程序框架类型。
 /// </summary>
    public enum ShutdownType : byte
    {
        /// <summary>
        /// 仅关闭程序框架。
        /// </summary>
        None = 0,

        /// <summary>
        /// 关闭程序框架并重启程序。
        /// </summary>
        Restart,

        /// <summary>
        /// 关闭程序框架并退出程序。
        /// </summary>
        Quit,
    }
}