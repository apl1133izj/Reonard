using Photon.Pun;
using UnityEngine;

public class SceneTransitionManager1 : MonoBehaviourPunCallbacks
{
    public static SceneTransitionManager1 instance; // �̱��� �ν��Ͻ�
    public static bool player2on;
   private void Update()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // �� �̵� �� �ı����� �ʵ��� ����
        }
        else
        {
            Destroy(gameObject);
        }
    }

    [PunRPC]
    private void ReceiveEvent(string message)
    {
        Debug.Log("�ٸ� ������ ���� �޽���: " + message);
        // �̺�Ʈ�� ó���ϴ� �ڵ� �߰�
    }
}
