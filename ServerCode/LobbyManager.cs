using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;

public class LobbyManager : MonoBehaviourPunCallbacks
{
    private readonly string gameVersion = "1";

    public Text connectionInfoText;
    public Button joinButton;
    public static int playerOrder = 0;
   
    private void Start()
    {
        PhotonNetwork.GameVersion = gameVersion; ;
        PhotonNetwork.ConnectUsingSettings();
        photonView.RPC("SyncPlayerState", RpcTarget.OthersBuffered);
        joinButton.interactable = false;
        connectionInfoText.text = "Connecting TO Master Server....";
    }

    public override void OnConnectedToMaster()
    {
        joinButton.interactable = true;
        connectionInfoText.text = "Online :  Connecting TO Master Server....";
    }

    public override void OnDisconnected(DisconnectCause cause)
    {
        joinButton.interactable = false;
        connectionInfoText.text = $"Offline :  Connecting TO Disable{cause.ToString()}....";

        PhotonNetwork.ConnectUsingSettings();
    }

    public void Connect()
    {
        joinButton.interactable = false;
        if (PhotonNetwork.IsConnected)
        {
            connectionInfoText.text = "Connectinh to Random Room...";
            PhotonNetwork.JoinRandomRoom();
        }
        else
        {
            connectionInfoText.text = "Offline :Connecting TO Disable - Try reconnecting ...";
            PhotonNetwork.ConnectUsingSettings();
        }
    }

    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        connectionInfoText.text = "Trere is no empty room, Creating new Room.";
        PhotonNetwork.CreateRoom(null, new RoomOptions { MaxPlayers = 2 });
    }

    public override void OnJoinedRoom()
    {
        connectionInfoText.text = "Connected with Room";
        playerOrder = PhotonNetwork.CurrentRoom.PlayerCount;
        PhotonNetwork.LoadLevel("Muity");
    }
    [PunRPC]
    public void SyncPlayerState()
    {
        // 클래스 레벨의 playerOrder 변수를 업데이트
        playerOrder = PhotonNetwork.CurrentRoom.PlayerCount; // 방에 있는 플레이어 수를 순서로 사용
        ExitGames.Client.Photon.Hashtable playerProperties = new ExitGames.Client.Photon.Hashtable
    {
        { "playerOrder", playerOrder }
    };
        PhotonNetwork.LocalPlayer.SetCustomProperties(playerProperties);
    }
}