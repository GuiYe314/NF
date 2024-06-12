// **********************************************************************
// 文件信息
// 文件名(File Name):                FileLogHelper.cs
// 作者(Author):                     王云超
// 创建时间(CreateTime):             2024年5月30日 9:17:23
// Unity版本(UnityVersion)：         2021.3.38f1c1
// 脚本描述(Module description):     
// 脚本修改(Script modification):
// **********************************************************************
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityGameFramework.Runtime;
using GameFramework;
using System.IO;
using Cysharp.Threading.Tasks;

namespace NF
{
    public class FileLogHelper : DefaultLogHelper
    {
        //设置日志文件保存路径-你可以自定义，也可以使用系统的
        private readonly string LogSavePathDirectory = UtilityNF.Path.GetParentDirectory(Application.dataPath, 1) + "/NFLog";
        private readonly string LogSavePath = string.Empty;

        //存储大小 单位M
        private readonly int LogSaveSize = 50;
        //存储时间 单位天
        private readonly int LogSaveTime = 30; 

        //消息队列
        private Queue<string> logMsgQueue = new Queue<string>();

        // 指定要筛选的文件类型
        string fileExtension = ".log"; // 例如 ".txt" 或 ".png"


        public FileLogHelper()
        {
            Application.logMessageReceived += OnLogMessageReceived;
            try
            {
             
                //查看路径是否存在不存在创建路径
                if (!Directory.Exists(LogSavePathDirectory)) {

                    Directory.CreateDirectory(LogSavePathDirectory);
                }
                //检查路径中的Log文件

                Open(LogSavePathDirectory);
            }
            catch
            {
            }
        }



        protected async UniTask Open(string logPath)
        {
            Debug.LogError("s0");

            string uniTask = await CheckDocument(logPath);
            Debug.LogError(uniTask);
            Debug.LogError("s1");
        }


        
        protected async UniTask<string> CheckDocument(string logPath)
        {
            Debug.LogError("s");
            string nLogPath =string.Empty;

            // 获取文件夹下的所有指定类型的文件
            string[] files = Directory.GetFiles(logPath, "*" + fileExtension);
            // 检查文件数组是否为空
            if (files.Length == 0)
            {
                //没有文件创建文件
                string logName = UtilityNF.Time.Get_Timestamp() + ".log";
                nLogPath = logPath + "/" + logName;
                File.Create(nLogPath);
            }



            return nLogPath;

        }


        private void OnLogMessageReceived(string logMessage, string stackTrace, LogType logType)
        {
            string log = Utility.Text.Format("[{0}][{1}] {2}{4}{3}{4}", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"), logType.ToString(), logMessage ?? "<Empty Message>", stackTrace ?? "<Empty StackTrace>", Environment.NewLine);

            logMsgQueue.Enqueue(logMessage);
            try
            {

                File.AppendAllText(LogSavePath, log, Encoding.UTF8);
            }
            catch
            {

               
            }
        }

    }
}