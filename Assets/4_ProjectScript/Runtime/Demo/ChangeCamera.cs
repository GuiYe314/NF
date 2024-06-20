using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.UI;

public class ChangeCamera : MonoBehaviour
{
    public CinemachineVirtualCameraBase cinemachine01;
    public CinemachineVirtualCameraBase cinemachine02;

    public Button button;

    public CanvasGroup canvasGroup;

    private void Start()
    {

        cinemachine01.Priority = 5;
        cinemachine02.Priority = 10;

        Set_UI(false);
            

        button.onClick.AddListener(() =>
        {

            bool oncCamera = cinemachine01.Priority == 5;
            Set_UI(oncCamera);
            cinemachine01.Priority = oncCamera ? 10 : 5;
            cinemachine02.Priority = cinemachine01.Priority == 5 ? 10 : 5;

        });
    }


    protected void Set_UI(bool isEnable)
    {
        canvasGroup.alpha = isEnable ? 1 : 0;
        canvasGroup.blocksRaycasts = isEnable;
        canvasGroup.interactable = isEnable;
    }


    private void OnDestroy()
    {
        button.onClick.RemoveAllListeners();
    }





}
