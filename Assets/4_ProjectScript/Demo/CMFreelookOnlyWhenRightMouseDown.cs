// **********************************************************************
// 文件信息
// 文件名(File Name):                CMFreelookOnlyWhenRightMouseDown.cs
// 作者(Author):                     填写你的名字
// 创建时间(CreateTime):             2024年5月29日 9:50:15
// Unity版本(UnityVersion)：         2021.3.38f1c1
// 脚本描述(Module description):
// 脚本修改(Script modification):
// **********************************************************************
using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Project.Demo
{
    public class CMFreelookOnlyWhenRightMouseDown : MonoBehaviour
    {
        void Start()
        {
            CinemachineCore.GetInputAxis = GetAxisCustom;
        }
        public float GetAxisCustom(string axisName)
        {
#if UNITY_EDITOR_WIN 
            if (axisName == "Mouse X")
            {
                if (Input.GetMouseButton(1))
                {
                    return UnityEngine.Input.GetAxis("Mouse X");
                }
            }
            else if (axisName == "Mouse Y")
            {
                if (Input.GetMouseButton(1))
                {
                    return UnityEngine.Input.GetAxis("Mouse Y");
                }
                else
                {
                    return 0;
                }
            }
            return 0;
#endif

            if (Input.touchCount == 1&& Input.touches[0].phase == TouchPhase.Moved)
            {

                if (axisName == "Mouse X") {


                    // 手指滑动时，要触发的代码 
                    float s01 = Input.GetAxis("Mouse X");    //手指水平移动的距离
                    return s01 * 0.3f;

                }
                else if (axisName == "Mouse Y")
                {
                    // 手指滑动时，要触发的代码 
                    float s02 = Input.GetAxis("Mouse Y");    //手指垂直移动的距离
                    return s02 * 0.4f;

                }
            }

            return 0;

        }
    }
}