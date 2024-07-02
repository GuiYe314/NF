using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;




/// <summary>
/// 文字提示，根据物体距离进行判断显示
/// </summary>
public class TextPrompt : MonoBehaviour
{

    /// <summary>
    /// 最大检测频率时间
    /// </summary>
    [SerializeField]
    protected float maxFrequencyTime;

    /// <summary>
    /// 最小检测频率时间
    /// </summary>
    [SerializeField]
    protected float minFrequencyTime;

    /// <summary>
    /// 检测物体
    /// </summary>
    [SerializeField]
    protected Transform detectionOjb;


    /// <summary>
    /// 提示文本
    /// </summary>
    [SerializeField]
    protected TextMeshProUGUI hintText;


    /// <summary>
    /// 显示距离
    /// </summary>
    [SerializeField]
    protected float showDistance;

    /// <summary>
    /// 显示UI
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

                //检测距离
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
