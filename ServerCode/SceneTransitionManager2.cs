using Photon.Pun;
using UnityEngine;

public class SceneTransitionManager2 : MonoBehaviourPunCallbacks
{
    
    public void TriggerEventInOtherScene()
    {
        // RPC를 호출하여 다른 씬의 플레이어들에게 이벤트를 전달
        photonView.RPC("ReceiveEvent", RpcTarget.Others, "이벤트 발생!");
    }

    [PunRPC]
    private void ReceiveEvent(bool onlinePlayer)
    {
        onlinePlayer = true;
    }
}
