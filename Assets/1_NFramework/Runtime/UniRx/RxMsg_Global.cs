// **********************************************************************
// 文件信息
// 文件名(File Name):                RxMsg_Global.cs
// 作者(Author):                     王云超
// 创建时间(CreateTime):             2024年6月21日 15:51:05
// Unity版本(UnityVersion)：         2021.3.39f1
// 脚本描述(Module description):     全局Rx
// 脚本修改(Script modification):
// **********************************************************************
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

namespace NF
{
    public  class RxMsg_Global : UniRxBase
    {

        /// <summary>
        /// 程序关闭回调
        /// </summary>
        public Subject<bool> OnApplicationQuit { get; set; }

        public override void Initialize()
        {
            base.Initialize();
            OnApplicationQuit = new Subject<bool>();


        }


        public override void On_Destroy()
        {
            base.On_Destroy();
            OnApplicationQuit.Dispose();
        }


    }
}