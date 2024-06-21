// **********************************************************************
// 文件信息
// 文件名(File Name):                NFEntry.cs
// 作者(Author):                     王云超
// 创建时间(CreateTime):             2024年5月29日 18:26:20
// Unity版本(UnityVersion)：         2021.3.38f1c1
// 脚本描述(Module description):     框架入口
// 脚本修改(Script modification):
// **********************************************************************
using GameFramework;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;
using UnityGameFramework.Runtime;

namespace NF
{
    public partial class NFEntry : MonoBehaviour
    {


        /// <summary>
        /// 获取RX组件。
        /// </summary>
        public static RXComponent RX
        {
            get;
            private set;
        }



        private void Start()
        {
            Init();

            InitBuiltinComponents();
            InitModuleComponents();
            InitProjectComponents();

        }

        protected void Init()
        {
            RX = UnityGameFramework.Runtime.GameEntry.GetComponent<RXComponent>();
        }


        /// <summary>
        //   初始化模块组件
        /// </summary>
        protected virtual void InitModuleComponents()
        {

        }


        /// <summary>
        /// 初始化项目组件
        /// </summary>
        protected virtual void InitProjectComponents()
        {

        }


        
        protected virtual void OnDestroy()
        {
            GameFrameworkLog.OnApplicationQuit();
        }

    }
}