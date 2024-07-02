using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Best.HTTP;
using System.Text;
using Best.HTTP.Request.Upload.Forms;
using Best.HTTP.Request.Upload;
using Best.HTTP.Request.Authenticators;

using System.Linq;
using System;

public class Net_Link : MonoBehaviour
{


    [SerializeField]
    protected string userName;

    [SerializeField]
    protected string password;

    [SerializeField]
    protected string scenicId;


    protected string token = string.Empty;

    // Start is called before the first frame update
    void Start()
    {
        //登录
        Login();
        //查询语音位置

    }



    protected void Login()
    {
        HTTPRequest request = HTTPRequest.CreatePost($"https://cultural-tourism.xianyanyu.tech/users/user/auth/form", LoginCallback);

        // 2. Setup request parameters
        request.UploadSettings.UploadStream = new MultipartFormDataStream()
            .AddField("username", userName)
            .AddField("password", password);

        // 3. Send request
        request.Send();
    }



    protected void LoginCallback(HTTPRequest req, HTTPResponse resp)
    {
        switch (req.State)
        {
            case HTTPRequestStates.Finished:
                if (resp.IsSuccess)
                {


                    //Debug.LogError(resp.DataAsText);


                    LoginClass login = JsonUtility.FromJson<LoginClass>(resp.DataAsText);

                    //JObject jo = (JObject)JsonConvert.DeserializeObject(resp.DataAsText);
                    token = login.data;
                    // 5. Here we can process the server's response
                    Debug.Log("Upload finished succesfully!");

                    StartCoroutine(GetAudio());
                }
                else
                {
                    // 6. Error handling
                    Debug.Log($"Server sent an error: {resp.StatusCode}-{resp.Message}");
                }
                break;

            default:
                // 6. Error handling
                Debug.LogError($"Request finished with error! Request state: {req.State}");
                break;
        }
    }





    /// <summary>
    /// 获取音频
    /// </summary>
    /// <returns></returns>
    protected IEnumerator GetAudio()
    {

        yield return new WaitForSecondsRealtime(2);
        HTTPRequest request = HTTPRequest.CreatePost($"https://cultural-tourism.xianyanyu.tech/api/cultural-tourism/v1.0/app/explanation/page", Callback);

        request.AddHeader("Jwt-Token", token);

        YYB yYB = new YYB();
        yYB.pageNum = "0";
        yYB.approveType = "1";
        yYB.pageSize = "20";
        yYB.roleId = "0";
        yYB.scenicId = "1792441056474693632";

        request.UploadSettings.UploadStream = new JSonDataStream<YYB>(yYB);
        request.Send();
    }



    protected void Callback(HTTPRequest req, HTTPResponse resp)
    {

        switch (req.State)
        {
            case HTTPRequestStates.Finished:
                if (resp.IsSuccess)
                {
                    //Debug.LogError(resp.DataAsText);
                    DisposeJson(resp.DataAsText);
                    // 5. Here we can process the server's response
                    Debug.Log("Upload finished succesfully!");
                }
                else
                {
                    // 6. Error handling
                    Debug.Log($"Server sent an error: {resp.StatusCode}-{resp.Message}");
                }
                break;

            default:
                // 6. Error handling
                Debug.LogError($"Request finished with error! Request state: {req.State}");
                break;
        }


    }



    protected void DisposeJson(string json)
    {

        //处理语音数据
        //JObject jsonObj = JsonConvert.DeserializeObject<JObject>(json);

        ////获取景点ID 和语音位置
        //JObject data = (JObject)jsonObj["data"];
        //JObject list = (JObject)(((JArray)data["list"])[0]);
        //JArray wlAppExplanationContentVos = (JArray)list["wlAppExplanationContentVos"];


        ScenicInfo scenicInfo = JsonUtility.FromJson<ScenicInfo>(json);



        Play_Audio[] play_Audio = GameObject.FindObjectsOfType<Play_Audio>();
        List<Play_Audio> playAduios = play_Audio.ToList();

        foreach (var item in scenicInfo.data.list[0].wlAppExplanationContentVos)
        {
            Play_Audio playA = playAduios.Find(x => x.attractionsId.Equals(item.attractionsId.ToString()));
            if (playA)
            {
                playA.Set_AudioUrl(item.fileUrl.ToString());
            }

        }



    }



    public class YYB
    {

        public string pageNum;
        public string approveType;
        public string id;
        public string pageSize;
        public string roleId;
        public string scenicId;


    }


    #region 登录实体
    //如果好用，请收藏地址，帮忙分享。
    [System.Serializable]
    public class LoginClass
    {
        /// <summary>
        /// 
        /// </summary>
        public string code ;
        /// <summary>
        /// 
        /// </summary>
        public string data ;
        /// <summary>
        /// 
        /// </summary>
        public Extra extra ;
        /// <summary>
        /// 
        /// </summary>
        public string httpStatus ;
        /// <summary>
        /// 
        /// </summary>
        public string message ;
        /// <summary>
        /// 
        /// </summary>
        public string ok ;
        /// <summary>
        /// 
        /// </summary>
        public string timestamp ;
    }

    #endregion


    #region 景点实体
    [Serializable]
    //如果好用，请收藏地址，帮忙分享。
    public class WlAppExplanationContentVosItem
    {
        /// <summary>
        /// 
        /// </summary>
        public string id ;
        /// <summary>
        /// 
        /// </summary>
        public string attractionsId ;
        /// <summary>
        /// 
        /// </summary>
        public string explanationId ;
        /// <summary>
        /// 
        /// </summary>
        public string contentMs ;
        /// <summary>
        /// 
        /// </summary>
        public string fileUrl ;
        /// <summary>
        /// 
        /// </summary>
        public string contentTxtUrl ;
        /// <summary>
        /// 
        /// </summary>
        public string isAudition ;
        /// <summary>
        /// 
        /// </summary>
        public string contentImage ;
        /// <summary>
        /// 大雁塔
        /// </summary>
        public string attractionsName ;
    }
    [Serializable]
    public class ListItem
    {
        /// <summary>
        /// 
        /// </summary>
        public string id ;
        /// <summary>
        /// 
        /// </summary>
        public string scenicId ;
        /// <summary>
        /// 大雁塔景区简介
        /// </summary>
        public string explanationName ;
        /// <summary>
        /// 
        /// </summary>
        public string language ;
        /// <summary>
        /// 
        /// </summary>
        public int price ;
        /// <summary>
        /// 后台管理员
        /// </summary>
        public string author ;
        /// <summary>
        /// 
        /// </summary>
        public string filePath ;
        /// <summary>
        /// 
        /// </summary>
        public int explanationContentCount ;
        /// <summary>
        /// 
        /// </summary>
        public string buyCount ;
        /// <summary>
        /// 
        /// </summary>
        public List<WlAppExplanationContentVosItem> wlAppExplanationContentVos ;
    }

    [Serializable]
    public class Data
    {
        /// <summary>
        /// 
        /// </summary>
        public int total ;
        /// <summary>
        /// 
        /// </summary>
        public List<ListItem> list ;
        /// <summary>
        /// 
        /// </summary>
        public int pageNum ;
        /// <summary>
        /// 
        /// </summary>
        public int pageSize ;
        /// <summary>
        /// 
        /// </summary>
        public int size ;
        /// <summary>
        /// 
        /// </summary>
        public int startRow ;
        /// <summary>
        /// 
        /// </summary>
        public int endRow ;
        /// <summary>
        /// 
        /// </summary>
        public int pages ;
        /// <summary>
        /// 
        /// </summary>
        public int prePage ;
        /// <summary>
        /// 
        /// </summary>
        public int nextPage ;
        /// <summary>
        /// 
        /// </summary>
        public string isFirstPage ;
        /// <summary>
        /// 
        /// </summary>
        public string isLastPage ;
        /// <summary>
        /// 
        /// </summary>
        public string hasPreviousPage ;
        /// <summary>
        /// 
        /// </summary>
        public string hasNextPage ;
        /// <summary>
        /// 
        /// </summary>
        public int navigatePages ;
        /// <summary>
        /// 
        /// </summary>
        public List<int> navigatepageNums ;
        /// <summary>
        /// 
        /// </summary>
        public int navigateFirstPage ;
        /// <summary>
        /// 
        /// </summary>
        public int navigateLastPage ;
    }

    [Serializable]
    public class Extra
    {
    }

    [Serializable]
    public class ScenicInfo
    {
        /// <summary>
        /// 
        /// </summary>
        public string code ;
        /// <summary>
        /// 
        /// </summary>
        public string message ;
        /// <summary>
        /// 
        /// </summary>
        public string path ;
        /// <summary>
        /// 
        /// </summary>
        public Data data ;
        /// <summary>
        /// 
        /// </summary>
        public string httpStatus ;
        /// <summary>
        /// 
        /// </summary>
        public Extra extra ;
        /// <summary>
        /// 
        /// </summary>
        public string timestamp ;
        /// <summary>
        /// 
        /// </summary>
        public string ok ;
    }

    #endregion

}
