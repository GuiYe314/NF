using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PersonHint : MonoBehaviour
{

    /// <summary>
    /// 提示UI
    /// </summary>
    [SerializeField]
    protected TextMeshProUGUI m_TextMeshPro;


    /// <summary>
    /// UI画布组
    /// </summary>
    [SerializeField]
    protected CanvasGroup m_Group;

    /// <summary>
    /// 注视物体
    /// </summary>
    [SerializeField]
    protected Transform lookAtObj;

    [SerializeField]
    protected bool xAstrict;
    [SerializeField]
    protected bool yAstrict;
    [SerializeField]
    protected bool zAstrict;



    private void Update()
    {
        
        if(m_Group.alpha == 1)
        {

            Vector3  v3 = lookAtObj.position;
            v3.x = xAstrict?0:v3.x;
            v3.y = yAstrict?0:v3.y;
            v3.z = zAstrict?0:v3.z;

            transform.LookAt(v3);
        }

    }


    /// <summary>
    /// 设置显示信息
    /// </summary>
    /// <param name="textInfo"></param>
    public void Set_HintText(string textInfo)
    {

        m_TextMeshPro.text = textInfo;

    }


    /// <summary>
    /// 显示UI
    /// </summary>
    /// <param name="show">是否显示</param>
    public void ShowUI(bool show)
    {
        m_Group.alpha = show ? 1 : 0;
    }



}
