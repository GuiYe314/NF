
using UnityEngine;
using System;
using Best.HTTP;
using System.Text;
using Best.HTTP.Request.Upload.Forms;
using Best.HTTP.Request.Upload;
using Best.HTTP.Request.Authenticators;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;



public class HttpsTest : MonoBehaviour
{



    public OperatingEnvironment operatingEnvironment;


    HTTPRequest re = null;

    string token = string.Empty;

    public int index = 0;

    // Start is called before the first frame update
    void Start()
    {
        Con();
    }

    // Update is called once per frame
    void Update()
    {
        
    }



    private void OnGUI()
    {
        if (GUILayout.Button("查询所有景点"))
        {

            //HTTPRequest request = HTTPRequest.CreateGet($"http://cultural-tourism-{operatingEnvironment.ToString()}.xayy.tech/api/cultural-tourism/v1.0/app/scenicGuide/getAllScenicInfo/1788842419551207424", Callback);
            HTTPRequest request = HTTPRequest.CreatePost($"https://cultural-tourism.xianyanyu.tech/api/cultural-tourism/v1.0/app/scenic/appPage", Callback);

            request.AddHeader("Jwt-Token", token);

            CS cS = new CS();
            cS.isEnable = "1";
            cS.pageNum = index.ToString();
            cS.pageSize = "10";




            request.UploadSettings.UploadStream = new JSonDataStream<CS>(cS);

            request.Send();
        }


        if (GUILayout.Button("获取景点下的讲解包"))
        {

            //HTTPRequest request = HTTPRequest.CreatePost($"http://cultural-tourism-{operatingEnvironment.ToString()}.xayy.tech/api/cultural-tourism/v1.0/app/explanation/page", Callback);
            HTTPRequest request = HTTPRequest.CreatePost($"https://cultural-tourism.xianyanyu.tech/api/cultural-tourism/v1.0/app/explanation/page", Callback);

            request.AddHeader("Jwt-Token", token);

            YYB yYB = new YYB();
            yYB.pageNum = "0";
            yYB.approveType = "1";
            yYB.pageSize = "10";
            yYB.roleId = "0";
            yYB.scenicId = "1792441056474693632";

            request.UploadSettings.UploadStream = new JSonDataStream<YYB>(yYB);

            request.Send();
        }




    }


    protected void Con()
    {

        // 1. Create request with a callback
        //HTTPRequest request = HTTPRequest.CreatePost($"http://cultural-tourism-{operatingEnvironment.ToString()}.xayy.tech/users/user/auth/form",DLCallback);
        HTTPRequest request = HTTPRequest.CreatePost($"https://cultural-tourism.xianyanyu.tech/users/user/auth/form",DLCallback);

        // 2. Setup request parameters
        request.UploadSettings.UploadStream = new MultipartFormDataStream()
            .AddField("username", "admin")
            .AddField("password", "Xayy@123.");




        // 3. Send request
        request.Send();

    }




    protected void DLCallback(HTTPRequest req, HTTPResponse resp)
    {

        switch (req.State)
        {
            case HTTPRequestStates.Finished:
                if (resp.IsSuccess)
                {
                    Debug.LogError(resp.DataAsText);
                    JObject jo = (JObject)JsonConvert.DeserializeObject(resp.DataAsText);
                    token = jo["data"].ToString();

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


    protected void Callback(HTTPRequest req, HTTPResponse resp)
    {

        switch (req.State)
        {
            case HTTPRequestStates.Finished:
                if (resp.IsSuccess)
                {
                    Debug.LogError(resp.DataAsText);

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




    //运行环境
    public enum OperatingEnvironment
    {
        dev=0,
        test,
        demo
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

    public class CS
    {

        public string isEnable;
        public string pageNum;
        public string pageSize;
        public string scenicArea;



    }


}
