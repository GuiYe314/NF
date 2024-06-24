// **********************************************************************
// 文件信息
// 文件名(File Name):                NProjectEntry.cs
// 作者(Author):                     王云超
// 创建时间(CreateTime):             2024年5月29日 18:46:11
// Unity版本(UnityVersion)：         2021.3.38f1c1
// 脚本描述(Module description):     项目入口
// 脚本修改(Script modification):
// **********************************************************************
using GameFramework;
using Module;
using NF;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityGameFramework.Runtime;

namespace Project
{
    public class NProjectEntry : NModuleEntry
    {



        protected override void InitProjectComponents()
        {
            base.InitProjectComponents();

            Log.Info("程序初始化完成，开启项目");
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();
        }

    }
}