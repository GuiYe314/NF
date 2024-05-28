// **********************************************************************
// 文件信息
// 文件名(File Name):                ResourceMode.cs
// 作者(Author):                     王云超
// 创建时间(CreateTime):             2024年5月27日 15:36:41
// Unity版本(UnityVersion)：         2021.3.38f1c1
// 脚本描述(Module description):     资源模式
// 脚本修改(Script modification):
// **********************************************************************
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NF.Librarie
{
    /// <summary>
    /// 资源模式。
    /// </summary>
    public enum ResourceMode : byte
    {
        /// <summary>
        /// 未指定。
        /// </summary>
        Unspecified = 0,

        /// <summary>
        /// 单机模式。
        /// </summary>
        Package,

        /// <summary>
        /// 预下载的可更新模式。
        /// </summary>
        Updatable,

        /// <summary>
        /// 使用时下载的可更新模式。
        /// </summary>
        UpdatableWhilePlaying
    }
}