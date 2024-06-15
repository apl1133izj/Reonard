
using Photon.Pun;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerNameText : MonoBehaviourPunCallbacks
{
    private Text nameText;

    private void Start()
    {
        nameText = GetComponent<Text>();
        if (AuthManager.User != null)
        {
            string email = AuthManager.User.Email;
            int atIndex = email.IndexOf('@');
            if (atIndex != -1)
            {
                string id = email.Substring(0, atIndex);
                if (SceneManager.GetActiveScene().name != "Muity")
                {
                    nameText.text = "로비 입장.\n" + id;
                }
                else
                {
                    int playerOrder = LobbyManager.playerOrder;
                    if (playerOrder == 1)
                    {
                        nameText.text = id;
                        photonView.RPC("SyncPlayerName", RpcTarget.OthersBuffered, id); // 다른 플레이어에게 플레이어 이름 동기화
                    }
                }
            }
            else
            {
                Debug.LogError("Invalid email address: " + email);
            }
        }
        else
        {
            nameText.text = "ERROR : AuthManager.User == null";
        }
    }

    [PunRPC]
    void SyncPlayerName(string playerName)
    {
        nameText.text = playerName; // 플레이어에게 받은 플레이어 이름으로 설정
    }
}
