
using System.Collections;
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

    [SerializeField]
    protected AudioSource audioSource;

    protected Play_Audio[] playAudio;

    [SerializeField]
    protected Animator animator;

    public int narrateIndex { get; set; }


    /// <summary>
    /// ������Ƶ·��
    /// </summary>
    /// <param name="audioPaht"></param>
    public void Set_AudioUrl(string audioUlr)
    {
        this.audioUlr = audioUlr;
        if(!audioSource)
            audioSource = GetComponent<AudioSource>();
        if(!animator)
            animator = GetComponent<Animator>();



        narrateIndex = Animator.StringToHash("narrate");


        StartCoroutine(Load());

        playAudio = GameObject.FindObjectsOfType<Play_Audio>();
    }


    /// <summary>
    /// �л���˵ ״̬
    /// </summary>
    /// <param name="isNarrate"></param>
    public void SwitchNarrate(bool isNarrate)
    {
        animator.SetBool(narrateIndex, isNarrate);

        if(isNarrate)
            audioSource.Play();
        else
            audioSource.Stop();

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



    void Update()
    {

        //��״̬��ɽ����������������ɺ��������
        if (animator.GetBool(narrateIndex) && !audioSource.isPlaying)
        {
            SwitchNarrate(false);
        }
    }





 
    public void OnPointerClick(PointerEventData eventData)
    {
        audioSource.Play();
    }



    // ...�������
    public void OnMouseDown()
    {

    
        SwitchNarrate(!audioSource.isPlaying);


        for (int i = 0; i < playAudio.Length; i++)
        {
            if (playAudio[i].name.Equals(name)) continue;
            playAudio[i].SwitchNarrate(false);
        }


    }

}
