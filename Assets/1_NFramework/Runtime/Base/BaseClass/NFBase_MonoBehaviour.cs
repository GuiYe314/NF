// **********************************************************************
// 文件信息
// 文件名(File Name):                NFBase_MonoBehaviour.cs
// 作者(Author):                     填写你的名字
// 创建时间(CreateTime):             2024年6月17日 9:02:54
// Unity版本(UnityVersion)：         2021.3.39f1
// 脚本描述(Module description):     所有框架Mono的基类
// 脚本修改(Script modification):
// **********************************************************************
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NF
{
    public class NFBase_MonoBehaviour : MonoBehaviour
    {
        /// <summary>
        /// 初始化并赋值
        /// </summary>
        /// <param name="par">根据需要进行传参</param>
        /// <param name="action">初始换完成回调</param>
        public virtual void  Initialization(object par,Action<bool,string> initEndCallback)
        {

        }
    }
}