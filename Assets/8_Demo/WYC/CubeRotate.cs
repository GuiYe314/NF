using System;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CubeRotate : MonoBehaviour
{
    public TextMeshProUGUI tx;

    void Update()
    {
        transform.Rotate(Vector3.up * Time.deltaTime * 30, Space.World);
    }
    public string SetTextInfo(string info)
    {
        byte[] bytes = Convert.FromBase64String(info);
        var decodedMessage = Encoding.UTF8.GetString(bytes);
        Debug.Log($"收到消息：{info}----{decodedMessage}");
        tx.text = decodedMessage;



        return "SetTextInfo";
    }
    public string AddScale()
    {
        transform.localScale += Vector3.one * 0.1f;

        return "AddScale";
    }
    public string SubtractScale()
    {
        transform.localScale -= Vector3.one * 0.1f;


        return "SubtractScale";
    }
}