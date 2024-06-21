// **********************************************************************
// 文件信息
// 文件名(File Name):                SingletonMono.cs
// 作者(Author):                     王云超
// 创建时间(CreateTime):             2024年6月21日 16:02:13
// Unity版本(UnityVersion)：         2021.3.39f1
// 脚本描述(Module description):     mono单例
// 脚本修改(Script modification):
// **********************************************************************
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NF
{
    using UnityEngine;

    public abstract class SingletonMono<T> : MonoBehaviour where T : MonoBehaviour
    {
        #region 局部变量
        private static T _Instance;
        #endregion
        #region 属性
        /// <summary>
        /// 获取单例对象
        /// </summary>
        public static T Instance
        {
            get
            {
                if (null == _Instance)
                {
                    _Instance = FindObjectOfType<T>();
                    if (null == _Instance)
                    {

                        GameObject singletonPoint = GameObject.Find("SingletonPoint");

                        GameObject go = new GameObject();
                        go.name = typeof(T).Name;
                        go.transform.SetParent(singletonPoint.transform, false);
                        _Instance = go.AddComponent<T>();
                    }
                }
                return _Instance;
            }
        }
        #endregion

        #region 方法
        protected virtual void Awake()
        {
            if (null == _Instance)
            {
                _Instance = this as T;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }
        #endregion
    }
}