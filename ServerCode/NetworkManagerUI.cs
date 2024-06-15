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

        //* �� ��ư�� ������ �߻��� �̺�Ʈ�� ���ٽ����� ����
        serverBtn.onClick.AddListener(() => {
            //* ��Ʈ��ũ �Ŵ����� �̱������� ���ְ� 
            //* StartServer ��ư�� ����� ������
            NetworkManager.Singleton.StartServer();
        });
        hostBtn.onClick.AddListener(() => {
            //* StartHost ��ư�� ����� ������
            NetworkManager.Singleton.StartHost();
        });
        clientBtn.onClick.AddListener(() => {
            //* StartClient ��ư�� ����� ������
            NetworkManager.Singleton.StartClient();
        });
    }
}
