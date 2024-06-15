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
    // ����ȭ�� ����
    public int syncedPlayerOrder;
    public typing getTyping;
    public GameObject multiUI;
    // ���� RPC�� ����Ͽ� �ٸ� �÷��̾��� ���¸� ����ȭ
    void Update()
    {

        int playerOrder = LobbyManager.playerOrder;



        // �ٸ� �÷��̾��� ���¸� �޾ƿ�
        if (!photonView.IsMine)
        {
            // ���� �÷��̾��� ���¸� ����ȭ
            photonView.RPC("SyncPlayerState2", RpcTarget.OthersBuffered, playerOrder);//OthersBuffered

            SyncPlayerState2(syncedPlayerOrder);
        }
        PlayerStatew(LobbyManager.playerOrder);
    }
    // ���� RPC�� ���� �ٸ� �÷��̾��� ���¸� �޾ƿ�
    [PunRPC]
    public void SyncPlayerState2(int playerOrder)
    {

        // ����ȭ�� �÷��̾��� ���¸� ����
        syncedPlayerOrder = playerOrder;

        // ����ȭ�� �÷��̾��� ���¿� ���� �̹����� ��ġ�� ����
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
        // ����ȭ�� �÷��̾��� ���¿� ���� �̹����� ��ġ�� ����
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


    // IPunObservable �������̽� ����
    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            // �ٸ� �÷��̾�� �� ���¸� ����
            stream.SendNext(syncedPlayerOrder);
        }
        else
        {
            // �ٸ� �÷��̾��� ���¸� �޾ƿ�
            syncedPlayerOrder = (int)stream.ReceiveNext();
        }
    }
    public override void OnPlayerEnteredRoom(Photon.Realtime.Player newPlayer)
    {
        Debug.Log("���ο� �÷��̾ �濡 �����߽��ϴ�.");
        if (PhotonNetwork.IsMasterClient && PhotonNetwork.CurrentRoom.PlayerCount == PhotonNetwork.CurrentRoom.MaxPlayers)
        {
            // ���� ���� á���� ���� ��ư�� Ȱ��ȭ�մϴ�.
            playerButtons.SetActive(true);
            getTyping.typingText.text = "���� ���۹�ư�� Ȱ��ȭ �˴ϴ�...";
        }
    }

    public void StartGame()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            // ������ ��쿡�� ��� �÷��̾�� ���� ���� ��ȣ�� �����ϴ�.
            photonView.RPC("RPC_StartGame", RpcTarget.AllBuffered);
        }
        else
        {
            getTyping.typingText.text = "������ ���ӽ��� ���Դϴ�.......";
        }
    }

    [PunRPC]
    void RPC_StartGame()
    {
        // ��� �÷��̾ ���� ������ �̵��մϴ�.
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