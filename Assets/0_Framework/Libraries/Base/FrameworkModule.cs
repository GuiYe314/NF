// **********************************************************************
// 文件信息
// 文件名(File Name):                FrameworkModule.cs
// 作者(Author):                     王云超
// 创建时间(CreateTime):             2024年5月27日 15:56:36
// Unity版本(UnityVersion)：         2021.3.38f1c1
// 脚本描述(Module description):     框架模块抽象类
// 脚本修改(Script modification):
// **********************************************************************
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace F.Librarie
{
    /// <summary>
    /// 框架模块抽象类。
    /// </summary>
    internal abstract class FrameworkModule
    {
        /// <summary>
        /// 获取框架模块优先级。
        /// </summary>
        /// <remarks>优先级较高的模块会优先轮询，并且关闭操作会后进行。</remarks>
        internal virtual int Priority
        {
            get
            {
                return 0;
            }
        }

        /// <summary>
        /// 游戏模块轮询。
        /// </summary>
        /// <param name="elapseSeconds">逻辑流逝时间，以秒为单位。</param>
        /// <param name="realElapseSeconds">真实流逝时间，以秒为单位。</param>
        internal abstract void Update(float elapseSeconds, float realElapseSeconds);

        /// <summary>
        /// 关闭并清理游戏框架模块。
        /// </summary>
        internal abstract void Shutdown();
    }
}