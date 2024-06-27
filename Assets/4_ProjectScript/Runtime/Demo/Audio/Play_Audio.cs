using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Networking;

public class Play_Audio : MonoBehaviour, IPointerClickHandler
{
    /// <summary>
    /// 景点ID
    /// </summary>
    public string attractionsId;

    /// <summary>
    ///音频路径
    /// </summary>
    [SerializeField]
    protected string audioUlr;

    public AudioClip audioClip;

    protected AudioSource audioSource;

    protected AudioSource[] audioSources;


    /// <summary>
    /// 设置音频路径
    /// </summary>
    /// <param name="audioPaht"></param>
    public void Set_AudioUrl(string audioUlr)
    {
        this.audioUlr = audioUlr;
        audioSource = GetComponent<AudioSource>();
        StartCoroutine(Load());

        audioSources = GameObject.FindObjectsOfType<AudioSource>();
    }


    protected IEnumerator Load()
    {
        //加载语音

        using (UnityWebRequest www = UnityWebRequestMultimedia.GetAudioClip(audioUlr, AudioType.MPEG))
        {
            yield return www.SendWebRequest();

            if (www.result == UnityWebRequest.Result.ConnectionError)
            {
                Debug.LogError(www.error);
            }
            else
            {

                Debug.LogError(audioUlr+"~~~~~~"+"下载完成");
                audioClip = DownloadHandlerAudioClip.GetContent(www);
                audioSource.clip = audioClip;
            }




        }



    }




 
    public void OnPointerClick(PointerEventData eventData)
    {
        audioSource.Play();
    }



    // ...当鼠标点击
    public void OnMouseDown()
    {

        Debug.LogError("播放"+audioSource.clip);

        if (audioSource.isPlaying)
        {
            audioSource.Stop();
        }
        else
        {
            audioSource.Play();
        }


        for (int i = 0; i < audioSources.Length; i++)
        {
            if (audioSources[i].name.Equals(name)) continue;
            audioSources[i].Stop(); ;
        }


    }

}
