// **********************************************************************
// 文件信息
// 文件名(File Name):                NFEntry.cs
// 作者(Author):                     王云超
// 创建时间(CreateTime):             2024年5月29日 18:26:20
// Unity版本(UnityVersion)：         2021.3.38f1c1
// 脚本描述(Module description):     框架入口
// 脚本修改(Script modification):
// **********************************************************************
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NF
{
    public partial class NFEntry : MonoBehaviour
    {
        private void Start()
        {
            InitBuiltinComponents();
            InitModuleComponents();
            InitProjectComponents();
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


    }
}