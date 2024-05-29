// **********************************************************************
// 文件信息
// 文件名(File Name):                ControlMovement.cs
// 作者(Author):                     填写你的名字
// 创建时间(CreateTime):             2024年5月29日 9:27:56
// Unity版本(UnityVersion)：         2021.3.38f1c1
// 脚本描述(Module description):
// 脚本修改(Script modification):
// **********************************************************************

using UnityEngine;
using UniRx;
using DG.Tweening;

using TMPro;
using System;



namespace Project.Demo
{
    public class ControlMovement : MonoBehaviour
    {
        /// <summary>
        /// 外地面
        /// </summary>
        [SerializeField]
        protected GameObject W_DM;

        /// <summary>
        /// 外地面
        /// </summary>
        [SerializeField]
        protected GameObject N_DM;



        public TextMeshProUGUI text;
        protected int i;

        private void Start()
        {
            i = 0;
        }


        private void Update()
        {


#if UNITY_EDITOR_WIN 
            if (Input.GetMouseButtonDown(0))
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit))
                {
                    GameObject go = hit.collider.gameObject;    //获得选中物体
                    string goName = go.name;    //获得选中物体的名字，使用hit.transform.name也可以
                    print(goName);

                    Vector3 pos = hit.point;
                    pos.y = 0f;

                    transform.DOMove(pos, 1f);
                }
            }



            return;
#endif

    
            if (Input.touchCount == 1 && Input.GetTouch(0).phase == TouchPhase.Began)//判断几个点击位置而且是最开始点击的屏幕，而不是滑动屏幕
            {
                i++;
                Observable.Timer(TimeSpan.FromSeconds(0.2f)).Subscribe(x =>{

                    i = 0;
                });


                text.text = "开启点击:::" + i;

                if (i >= 2)//tapcount是点击次数
                {
                    i = 0;
                    text.text = "两次点击";
                    Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                    RaycastHit hit;
                    if (Physics.Raycast(ray, out hit))
                    {
                        GameObject go = hit.collider.gameObject;    //获得选中物体
                        string goName = go.name;    //获得选中物体的名字，使用hit.transform.name也可以
                        print(goName);

                        Vector3 pos = hit.point;
                        pos.y = 0f;

                        transform.DOMove(pos, 1f);
                    }
                }
                   
            }

        }



    }
}