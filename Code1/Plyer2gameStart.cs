using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
public class Plyer2gameStart : MonoBehaviourPunCallbacks
{
    public static bool gameStart;
    void Start()
    {
        gameStart = true;
    }
    [PunRPC]
    public void PlayergameStart(bool playegGmeBool)
    {
        // ����ȭ�� �÷��̾��� ���¸� ����
        gameStart = playegGmeBool;

    }
}
