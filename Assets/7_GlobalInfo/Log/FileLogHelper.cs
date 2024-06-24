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
using System.Linq;
using System.Threading;
using Unity.VisualScripting;
using UniRx;

namespace NF
{
    public class FileLogHelper : GameFrameworkLog.ILogHelper
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


        //程序关闭
        private bool applicationQuit = false;

        protected string OpenRecord = $"--------------------------------开启记录：{System.DateTime.Now}--------------------------------";
        protected string EndRecord = $"--------------------------------结束记录：{System.DateTime.Now}--------------------------------";


        public FileLogHelper()
        {

  

            Application.logMessageReceived += OnLogMessageReceived;

        
            try
            {
               
                //查看路径是否存在不存在创建路径
                if (!Directory.Exists(LogSavePathDirectory)) {

                    Directory.CreateDirectory(LogSavePathDirectory);
                }

                logMsgQueue.Enqueue(OpenRecord);

                //开启记录
                Open(LogSavePathDirectory);

                
            }
            catch
            {

            }
        }



        public  void Log(GameFrameworkLogLevel level, object message)
        {
            switch (level)
            {
                case GameFrameworkLogLevel.Debug:
#if !NF_UNITY_NOLOG
                    Debug.Log(Utility.Text.Format("<color=#888888>{0}</color>", message));
#endif
                    break;

                case GameFrameworkLogLevel.Info:
#if !NF_UNITY_NOLOG
                    Debug.Log(message.ToString());
#endif
                    break;
                case GameFrameworkLogLevel.Warning:

#if !NF_UNITY_NOLOG
                    Debug.LogWarning(message.ToString());
#endif
                    break;

                case GameFrameworkLogLevel.Error:

#if !NF_UNITY_NOLOG
                    Debug.LogError(message.ToString());
#endif
                    break;
                default:
                    throw new GameFrameworkException(message.ToString());
            }


            //进行日志保存拼接
            System.Diagnostics.StackTrace st = new System.Diagnostics.StackTrace(true);
            ConcatenationMsg(message.ToString(),level,st);
        }


        protected virtual void ConcatenationMsg(string logMsg, GameFrameworkLogLevel level, System.Diagnostics.StackTrace st)
        {

            string log = string.Format("【{0}】---ProgramRunTime:【{1}】---ScriptLocation:【{2}-{3}-{4}】"+ "---Messageblob:【{5}】", level.ToString(), Time.time.ToString(), st.GetFrame(3).GetMethod().ReflectedType.Name, st.GetFrame(3).GetMethod().Name, st.GetFrame(3).GetFileLineNumber().ToString(), logMsg);
            logMsgQueue.Enqueue(log);
        }


        protected async UniTask Open(string logDirectoryPath)
        {

            string logPath = await CheckDirectory(logDirectoryPath);
           
            //开启写入
            ThreadPool.QueueUserWorkItem(new WaitCallback(RwriteLog),logPath);
        }


        protected void RwriteLog(object logPath) {


            while (true) {

                try
                {
                    if (logMsgQueue.Count <= 0)
                    {
                        Thread.Sleep(1000);
                        if (logMsgQueue.Count <= 0&&applicationQuit) return;
                    }
                    else
                    {

                        string msg = logMsgQueue.Dequeue();
                        msg += Environment.NewLine;
                        File.AppendAllText(logPath.ToString(), msg, Encoding.UTF8);
                        Thread.Sleep(10);
                    }
                }
                catch (Exception e)
                {

                    Debug.LogError(e);
     
                }
          
          
            }
        }

        //检查文件地址
        protected async UniTask<string> CheckDirectory(string logPath)
        {

            string nLogPath =string.Empty;
            DirectoryInfo folder = new DirectoryInfo(logPath);
            // 获取文件夹下的所有指定类型的文件
            FileInfo[] files = folder.GetFiles("*" + fileExtension);
            // 检查文件数组是否为空
            if (files.Length == 0)
            {
                nLogPath = CreatLogFile(logPath);
            }
            else {

                //根据创建时间排序
                List<FileInfo> fileInfos = files.ToList();
                fileInfos.Sort(CreationTimeSort);

                //排序后获取第一个文件,判断文件大小
                FileInfo firstFileInfo = fileInfos[0];
                if((firstFileInfo.Length/1024)< LogSaveSize)
                {
                    nLogPath = firstFileInfo.FullName;
                }
                else
                {
                    nLogPath = CreatLogFile(logPath);
                }

            }
            return nLogPath;

        }


        /// <summary>
        /// 创建日志文件
        /// </summary>
        protected string CreatLogFile(string logPath)
        {
            //没有文件创建文件
            string logName = UtilityNF.Time.Get_Timestamp() + fileExtension;
            string nLogPath = logPath + "/" + logName;
            File.Create(nLogPath);

            return nLogPath; 
        }

        /// <summary>
        /// 创建日期排序
        /// </summary>
        /// <param name="a">文件A</param>
        /// <param name="b">文件B</param>
        /// <returns></returns>
        protected int CreationTimeSort(FileInfo a, FileInfo b)
        {
            return a.CreationTime.CompareTo(b.CreationTime);
        }



        /// <summary>
        /// 消息回调
        /// </summary>
        /// <param name="logMessage">log信息</param>
        /// <param name="stackTrace"></param>
        /// <param name="logType"></param>
        private void OnLogMessageReceived(string logMessage, string stackTrace, LogType logType)
        {
            try
            {
                if (logType.Equals(LogType.Error))
                {
                    //只记录错误信息
                }


      
            }
            catch
            {

               
            }
        }

        public  void OnApplicationQuit()
        {
            logMsgQueue.Enqueue(EndRecord);
            applicationQuit = true;
         
        }

    }
}