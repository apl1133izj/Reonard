using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Unity.Netcode;



public class NetworkManagerUI : MonoBehaviour
{
    [SerializeField] private Button serverBtn;
    [SerializeField] private Button hostBtn;
    [SerializeField] private Button clientBtn;

    private void Awake()
    {

        //* 각 버튼을 누르면 발생할 이벤트를 람다식으로 선언
        serverBtn.onClick.AddListener(() => {
            //* 네트워크 매니저가 싱글톤으로 되있고 
            //* StartServer 버튼의 기능을 빌려옴
            NetworkManager.Singleton.StartServer();
        });
        hostBtn.onClick.AddListener(() => {
            //* StartHost 버튼의 기능을 빌려옴
            NetworkManager.Singleton.StartHost();
        });
        clientBtn.onClick.AddListener(() => {
            //* StartClient 버튼의 기능을 빌려옴
            NetworkManager.Singleton.StartClient();
        });
    }
}
