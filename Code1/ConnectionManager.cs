using Photon.Pun;
using UnityEngine;
using UnityEngine.UI;

public class ConnectionManager : MonoBehaviourPunCallbacks, IPunObservable
{
    public Image[] playerImage;
    public Sprite[] playerSprites;
    public GameObject[] sizeControl;
    public GameObject playerButtons;
    public GameObject[] map;
    // 동기화할 변수
    public int syncedPlayerOrder;
    public typing getTyping;
    public GameObject multiUI;
    // 포톤 RPC를 사용하여 다른 플레이어의 상태를 동기화
    void Update()
    {

        int playerOrder = LobbyManager.playerOrder;



        // 다른 플레이어의 상태를 받아옴
        if (!photonView.IsMine)
        {
            // 현재 플레이어의 상태를 동기화
            photonView.RPC("SyncPlayerState2", RpcTarget.OthersBuffered, playerOrder);//OthersBuffered

            SyncPlayerState2(syncedPlayerOrder);
        }
        PlayerStatew(LobbyManager.playerOrder);
    }
    // 포톤 RPC를 통해 다른 플레이어의 상태를 받아옴
    [PunRPC]
    public void SyncPlayerState2(int playerOrder)
    {

        // 동기화된 플레이어의 상태를 설정
        syncedPlayerOrder = playerOrder;

        // 동기화된 플레이어의 상태에 따라 이미지와 위치를 설정
        if (playerOrder == 1)
        {
            playerImage[0].sprite = playerSprites[0];
            sizeControl[0].transform.localPosition = new Vector2(-28, 20f);
            sizeControl[0].transform.localScale = new Vector2(0.5f, 0.5f);

        }
        else if (playerOrder == 2)
        {
            playerImage[1].sprite = playerSprites[1];
            sizeControl[1].transform.localPosition = new Vector2(28, 10f);
            sizeControl[1].transform.localScale = new Vector2(0.5f, 0.5f);


        }
    }
    void PlayerStatew(int playerOrder)
    {
        // 동기화된 플레이어의 상태에 따라 이미지와 위치를 설정
        if (playerOrder == 1)
        {
            playerImage[0].sprite = playerSprites[0];
            sizeControl[0].transform.localPosition = new Vector2(-28, 20f);
            sizeControl[0].transform.localScale = new Vector2(0.5f, 0.5f);

        }
        else if (playerOrder == 2)
        {
            playerImage[1].sprite = playerSprites[1];
            sizeControl[1].transform.localPosition = new Vector2(28, 10f);
            sizeControl[1].transform.localScale = new Vector2(0.5f, 0.5f);

        }
    }


    // IPunObservable 인터페이스 구현
    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            // 다른 플레이어에게 내 상태를 전송
            stream.SendNext(syncedPlayerOrder);
        }
        else
        {
            // 다른 플레이어의 상태를 받아옴
            syncedPlayerOrder = (int)stream.ReceiveNext();
        }
    }
    public override void OnPlayerEnteredRoom(Photon.Realtime.Player newPlayer)
    {
        Debug.Log("새로운 플레이어가 방에 접속했습니다.");
        if (PhotonNetwork.IsMasterClient && PhotonNetwork.CurrentRoom.PlayerCount == PhotonNetwork.CurrentRoom.MaxPlayers)
        {
            // 방이 가득 찼으면 시작 버튼을 활성화합니다.
            playerButtons.SetActive(true);
            getTyping.typingText.text = "게임 시작버튼이 활성화 됩니다...";
        }
    }

    public void StartGame()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            // 방장인 경우에만 모든 플레이어에게 게임 시작 신호를 보냅니다.
            photonView.RPC("RPC_StartGame", RpcTarget.AllBuffered);
        }
        else
        {
            getTyping.typingText.text = "방장이 게임시작 중입니다.......";
        }
    }

    [PunRPC]
    void RPC_StartGame()
    {
        // 모든 플레이어가 게임 씬으로 이동합니다.
        if (PhotonNetwork.IsMasterClient)
        {
            PhotonNetwork.LoadLevel("Player1");
        }
        else
        {
            PhotonNetwork.LoadLevel("Player3");
        }
    }
}