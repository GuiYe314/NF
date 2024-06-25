
using UnityEngine;
using System;
using Best.HTTP;
using System.Text;
using Best.HTTP.Request.Upload.Forms;
using Best.HTTP.Request.Upload;
using Best.HTTP.Request.Authenticators;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Unity.VisualScripting.Antlr3.Runtime;


public class HttpsTest : MonoBehaviour
{

    HTTPRequest re = null;

    string token = string.Empty;

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
            HTTPRequest request = HTTPRequest.CreateGet("http://cultural-tourism-dev.xayy.tech/api/cultural-tourism/v1.0/app/scenic/selectAllScenic",
                                        Callback);
            //request.UploadSettings.UploadStream = new MultipartFormDataStream();
            request.Authenticator = new BearerTokenAuthenticator(token);

            request.AddHeader("Authorization",token);
            request.Send();

        }

    }


    protected void Con()
    {

        // 1. Create request with a callback
        HTTPRequest request = HTTPRequest.CreatePost("http://cultural-tourism-dev.xayy.tech/users/user/auth/form",
                                             DLCallback);

        // 2. Setup request parameters
        request.UploadSettings.UploadStream = new MultipartFormDataStream()
            .AddField("username", "admin")
            .AddField("password", "123456");


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

}
