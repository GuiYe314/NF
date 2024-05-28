// **********************************************************************
// 文件信息
// 文件名(File Name):                DefaultLogHelper.cs
// 作者(Author):                     王云超
// 创建时间(CreateTime):             2024年5月27日 15:29:15
// Unity版本(UnityVersion)：         2021.3.38f1c1
// 脚本描述(Module description):     默认日志辅助器
// 脚本修改(Script modification):
// **********************************************************************
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using F.Librarie;

namespace NF.Librarie
{
    public class DefaultLogHelper : FrameworkLog.ILogHelper
    {
        /// <summary>
        /// 记录日志。
        /// </summary>
        /// <param name="level">日志等级。</param>
        /// <param name="message">日志内容。</param>
        public void Log(FrameworkLogLevel level, object message)
        {
            switch (level)
            {
                case FrameworkLogLevel.Debug:
                    Debug.Log(Utility.Text.Format("<color=#888888>{0}</color>", message));
                    break;

                case FrameworkLogLevel.Info:
                    Debug.Log(message.ToString());
                    break;

                case FrameworkLogLevel.Warning:
                    Debug.LogWarning(message.ToString());
                    break;

                case FrameworkLogLevel.Error:
                    Debug.LogError(message.ToString());
                    break;

                default:
                    throw new FrameworkException(message.ToString());
            }
        }
    }
}