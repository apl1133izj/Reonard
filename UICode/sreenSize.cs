using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sreenSize : MonoBehaviour
{
    void Start()
    {
        // 현재 모니터의 해상도를 가져옵니다.
        int screenWidth = Screen.currentResolution.width;
        int screenHeight = Screen.currentResolution.height;

        // 세로 모드 게임의 기본 해상도 비율을 계산합니다.
        float targetAspectRatio = 1080.0f / 1920.0f;
        float currentAspectRatio = (float)screenHeight / screenWidth;


        Camera camera = Camera.main;

        if (currentAspectRatio >= targetAspectRatio)
        {
            // 현재 화면 비율이 더 크면 레터박스를 추가합니다.
            float inset = 1.0f - targetAspectRatio / currentAspectRatio;
            camera.rect = new Rect(0.0f, inset / 2.0f, 1.0f, 1.0f - inset);
        }
        else
        {
            // 현재 화면 비율이 더 작으면 필러박스를 추가합니다.
            float inset = 1.0f - currentAspectRatio / targetAspectRatio;
            camera.rect = new Rect(inset / 2.0f, 0.0f, 1.0f - inset, 1.0f);
        }
    }
}

