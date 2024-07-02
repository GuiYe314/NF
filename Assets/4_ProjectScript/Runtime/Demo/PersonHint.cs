using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PersonHint : MonoBehaviour
{

    /// <summary>
    /// ��ʾUI
    /// </summary>
    [SerializeField]
    protected TextMeshProUGUI m_TextMeshPro;


    /// <summary>
    /// UI������
    /// </summary>
    [SerializeField]
    protected CanvasGroup m_Group;

    /// <summary>
    /// ע������
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
    /// ������ʾ��Ϣ
    /// </summary>
    /// <param name="textInfo"></param>
    public void Set_HintText(string textInfo)
    {

        m_TextMeshPro.text = textInfo;

    }


    /// <summary>
    /// ��ʾUI
    /// </summary>
    /// <param name="show">�Ƿ���ʾ</param>
    public void ShowUI(bool show)
    {
        m_Group.alpha = show ? 1 : 0;
    }



}
