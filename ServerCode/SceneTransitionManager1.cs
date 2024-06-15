using Photon.Pun;
using UnityEngine;

public class SceneTransitionManager1 : MonoBehaviourPunCallbacks
{
    public static SceneTransitionManager1 instance; // 싱글톤 인스턴스
    public static bool player2on;
   private void Update()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // 씬 이동 시 파괴되지 않도록 설정
        }
        else
        {
            Destroy(gameObject);
        }
    }

    [PunRPC]
    private void ReceiveEvent(string message)
    {
        Debug.Log("다른 씬에서 받은 메시지: " + message);
        // 이벤트를 처리하는 코드 추가
    }
}
