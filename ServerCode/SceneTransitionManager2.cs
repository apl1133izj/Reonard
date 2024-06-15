using Photon.Pun;
using UnityEngine;

public class SceneTransitionManager2 : MonoBehaviourPunCallbacks
{
    
    public void TriggerEventInOtherScene()
    {
        // RPC�� ȣ���Ͽ� �ٸ� ���� �÷��̾�鿡�� �̺�Ʈ�� ����
        photonView.RPC("ReceiveEvent", RpcTarget.Others, "�̺�Ʈ �߻�!");
    }

    [PunRPC]
    private void ReceiveEvent(bool onlinePlayer)
    {
        onlinePlayer = true;
    }
}
