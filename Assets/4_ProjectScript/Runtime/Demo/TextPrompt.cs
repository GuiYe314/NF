using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;




/// <summary>
/// ������ʾ�����������������ж���ʾ
/// </summary>
public class TextPrompt : MonoBehaviour
{

    /// <summary>
    /// �����Ƶ��ʱ��
    /// </summary>
    [SerializeField]
    protected float maxFrequencyTime;

    /// <summary>
    /// ��С���Ƶ��ʱ��
    /// </summary>
    [SerializeField]
    protected float minFrequencyTime;

    /// <summary>
    /// �������
    /// </summary>
    [SerializeField]
    protected Transform detectionOjb;


    /// <summary>
    /// ��ʾ�ı�
    /// </summary>
    [SerializeField]
    protected TextMeshProUGUI hintText;


    /// <summary>
    /// ��ʾ����
    /// </summary>
    [SerializeField]
    protected float showDistance;

    /// <summary>
    /// ��ʾUI
    /// </summary>
    [SerializeField]
    protected CanvasGroup showUI;



    float frequencyTime = 1.0f;



    protected void Start()
    {
        frequencyTime = UnityEngine.Random.Range(minFrequencyTime, maxFrequencyTime);

        StartCoroutine(Detection());

    }


    protected IEnumerator Detection()
    {

        while (true) {



            try
            {

                //������
                float dis = Vector3.Distance(transform.position, detectionOjb.position);

                showUI.alpha = dis < showDistance?1:0;



            }
            catch (System.Exception e)
            {
                Debug.LogError(e);
                
            }
        
        
            yield return new WaitForSecondsRealtime(frequencyTime);
        
        
        }



    }




}
