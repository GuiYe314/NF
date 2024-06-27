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
    /// ����ID
    /// </summary>
    public string attractionsId;

    /// <summary>
    ///��Ƶ·��
    /// </summary>
    [SerializeField]
    protected string audioUlr;

    public AudioClip audioClip;

    protected AudioSource audioSource;

    protected AudioSource[] audioSources;


    /// <summary>
    /// ������Ƶ·��
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
        //��������

        using (UnityWebRequest www = UnityWebRequestMultimedia.GetAudioClip(audioUlr, AudioType.MPEG))
        {
            yield return www.SendWebRequest();

            if (www.result == UnityWebRequest.Result.ConnectionError)
            {
                Debug.LogError(www.error);
            }
            else
            {

                Debug.LogError(audioUlr+"~~~~~~"+"�������");
                audioClip = DownloadHandlerAudioClip.GetContent(www);
                audioSource.clip = audioClip;
            }




        }



    }




 
    public void OnPointerClick(PointerEventData eventData)
    {
        audioSource.Play();
    }



    // ...�������
    public void OnMouseDown()
    {

        Debug.LogError("����"+audioSource.clip);

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
