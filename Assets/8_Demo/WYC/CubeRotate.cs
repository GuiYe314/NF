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
    public void SetTextInfo(string info)
    {
        byte[] bytes = Convert.FromBase64String(info);
        var decodedMessage = Encoding.UTF8.GetString(bytes);
        Debug.Log($"收到消息：{info}----{decodedMessage}");
        tx.text = decodedMessage;
    }
    public void AddScale()
    {
        transform.localScale += Vector3.one * 0.1f;
    }
    public void SubtractScale()
    {
        transform.localScale -= Vector3.one * 0.1f;
    }
}