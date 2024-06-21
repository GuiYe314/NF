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


        public FileLogHelper()
        {

  

            Application.logMessageReceived += OnLogMessageReceived;

        
            try
            {
                Log( GameFrameworkLogLevel.Info,"程序初始化完成，开启项目");
                //查看路径是否存在不存在创建路径
                if (!Directory.Exists(LogSavePathDirectory)) {

                    Directory.CreateDirectory(LogSavePathDirectory);
                }
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
                    Debug.Log(Utility.Text.Format("<color=#888888>{0}</color>", message));
                    break;

                case GameFrameworkLogLevel.Info:
                    Debug.Log(message.ToString());
                    break;

                case GameFrameworkLogLevel.Warning:
                    Debug.LogWarning(message.ToString());
                    break;

                case GameFrameworkLogLevel.Error:
                    Debug.LogError(message.ToString());
                    break;

                default:
                    throw new GameFrameworkException(message.ToString());
            }
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
                        if (applicationQuit) return;
                    }
                    else
                    {
                        File.AppendAllText(logPath.ToString(), logMsgQueue.Dequeue(), Encoding.UTF8);
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



        private void OnLogMessageReceived(string logMessage, string stackTrace, LogType logType)
        {
            string log = Utility.Text.Format("[{0}][{1}] {2}{4}{3}{4}", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"), logType.ToString(), logMessage ?? "<Empty Message>", stackTrace ?? "<Empty StackTrace>", Environment.NewLine);

         
            try
            {
                lock (logMsgQueue) {
                    //通过队列记录Log日志
                    logMsgQueue.Enqueue(logMessage);
                }
     
                //File.AppendAllText(LogSavePath, log, Encoding.UTF8);
            }
            catch
            {

               
            }
        }

        public  void OnApplicationQuit()
        {
         
            applicationQuit = true;
        }

    }
}