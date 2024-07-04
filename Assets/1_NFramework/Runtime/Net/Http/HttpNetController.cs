// **********************************************************************
// 文件信息
// 文件名(File Name):                HttpNetController.cs
// 作者(Author):                     王云超
// 创建时间(CreateTime):             2024年7月4日 16:33:13
// Unity版本(UnityVersion)：         2021.3.39f1
// 脚本描述(Module description):     HTTP信息
// 脚本修改(Script modification):
// **********************************************************************
using Best.HTTP;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using System.IO;

namespace NF
{
    public class HttpNetController
    {


        /// <summary>
        /// 返回消息
        /// </summary>
        public static Subject<HttpNetMsgDataStructure> rx_respMsg { get; set; }

        protected static HttpNetMsgDataStructure httpNetMsgDataStructure { get; set; }

        static HttpNetController() {rx_respMsg = new Subject<HttpNetMsgDataStructure>(); }

        /// <summary>
        /// 初始化
        /// </summary>
        public static void Init() { }


        /// <summary>
        /// Post请求 数据流参数
        /// </summary>
        /// <param name="url">url</param>
        /// <param name="sendMsg">数据流参数</param>
        /// <returns></returns>
        public static HTTPRequest PostDataStream(string url,Stream sendMsg)
        {
            HTTPRequest request = HTTPRequest.CreatePost(url, Callback);
            request.UploadSettings.UploadStream = sendMsg;
            request.Send();
            return request;
        }




        protected static void Callback(HTTPRequest req, HTTPResponse resp)
        {

            switch (req.State)
            {
                case HTTPRequestStates.Finished:
                    if (resp.IsSuccess)
                    {
                        httpNetMsgDataStructure.httpNetMsgState = HttpNetMsgStateEnum.msg;
                        httpNetMsgDataStructure.msg = resp.Message;
                        rx_respMsg.OnNext(httpNetMsgDataStructure);
                        Debug.Log("Upload finished succesfully!");
                    }
                    else
                    {
                        httpNetMsgDataStructure.httpNetMsgState = HttpNetMsgStateEnum.error;
                        httpNetMsgDataStructure.msg = $"Server sent an error: {resp.StatusCode}-{resp.Message}";
                        rx_respMsg.OnNext(httpNetMsgDataStructure);
                        Debug.Log(httpNetMsgDataStructure.msg);
                    }
                    break;

                default:
                    httpNetMsgDataStructure.httpNetMsgState = HttpNetMsgStateEnum.error;
                    httpNetMsgDataStructure.msg = $"Request finished with error! Request state: {req.State}";
                    rx_respMsg.OnNext(httpNetMsgDataStructure);
                    Debug.LogError(httpNetMsgDataStructure.msg);
                    break;
            }


        }

        /// <summary>
        /// 销毁
        /// </summary>
        public static void Destroy()
        {
            rx_respMsg.Dispose();
        }



    }



    public class HttpNetMsgDataStructure
    {
        public HttpNetMsgStateEnum httpNetMsgState { get; set; }

        public string msg { get; set; }


    }



    /// <summary>
    /// 消息装填
    /// </summary>
    public enum HttpNetMsgStateEnum
    {
        msg = 0,
        error
    }

}