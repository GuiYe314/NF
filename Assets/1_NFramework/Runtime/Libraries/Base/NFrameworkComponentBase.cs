// **********************************************************************
// 文件信息
// 文件名(File Name):                NFrameworkComponentBase.cs
// 作者(Author):                     王云超
// 创建时间(CreateTime):             2024年5月27日 13:58:02
// Unity版本(UnityVersion)：         2021.3.38f1c1
// 脚本描述(Module description):     框架组件抽象类
// 脚本修改(Script modification):
// **********************************************************************
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace NF.Librarie
{
    public abstract class NFrameworkComponentBase : MonoBehaviour
    {

        /// <summary>
        /// 游戏框架组件初始化。
        /// </summary>
        protected virtual void Awake()
        {
            NFrameworkEntry.RegisterComponent(this);
        }

    }
}