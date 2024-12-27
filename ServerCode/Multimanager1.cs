using Photon.Pun;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class Multimanager1 : MonoBehaviourPun
{

    public GameObject multiUI;
    // 포톤 RPC를 사용하여 다른 플레이어의 상태를 동기화
    public int syncedPlayerOrder;
    public BridgeBuild bridgeBuild;
    public static int attackCoins = 260;
    public TextMeshProUGUI[] player1goblineCountMPU;

    public TextMeshProUGUI[] player2goblineCountMPU;

    public TextMeshProUGUI[] castleHpTMPU; //0:P1 1:P2

    public TextMeshProUGUI attackCoinText;
    public TextMeshProUGUI[] playerNameText;
    public TextMeshProUGUI sendButtonMesage;
    public TextMeshProUGUI attackCoinsButtonMesage;
    public Image castleImage;
    public Sprite castleSprite;

    bool send = false;
    public int[] warriorCount2 = { 60, 0 };
    public int[] acherCount2 = { 80, 0 };
    public int[] tntCount2 = { 120, 0 };

    //SendGobline;
    public int warriorInsCountPunRPC;
    public int archerInsCountPunRPC;
    public int bombInsCountPunRPC;

    [SerializeField]
    bool sendBoolButton2 = false;
    public bool gameOverGet;
    public GameObject[] GameVictoryGameObject;
    private void Start()
    {
        if (PhotonNetwork.IsConnectedAndReady)
        {
            syncedPlayerOrder = LobbyManager.playerOrder;
            photonView.RPC("loadGame", RpcTarget.AllBuffered, syncedPlayerOrder);
            Castle2Name();
        }
    }
    void Update()
    {
        ViewPlayer();
        UI();
        if (PhotonNetwork.IsConnectedAndReady)
        {
            Castle2Hp();
            GameOver();
        }
    }
    // 성 체력 UI를 동기화하는 메서드
    void Castle2Hp()
    {
        photonView.RPC("PunRPCCastle1Hp", RpcTarget.All, Castle2.castle2Hp);
    }
    [PunRPC]
    void PunRPCCastle2Hp(int hp2)
    {
        castleHpTMPU[0].text = hp2.ToString();
    }

    void GameOver()
    {
        photonView.RPC("GameOver1PunRPC", RpcTarget.All, Castle2.gameOver);
        if (gameOverGet)
        {
            castleImage.sprite = castleSprite;
            Victory();
            Debug.Log("게임오버2");
        }
    }

    [PunRPC]
    void GameOver2PunRPC(bool _gameOver)
    {
        gameOverGet = _gameOver;
    }

    void Castle2Name()
    {
        string email = AuthManager.User.Email;
        int atIndex = email.IndexOf('@');
        if (atIndex != -1)
        {
            string id = email.Substring(0, atIndex);
            playerNameText[1].text = id;
            photonView.RPC("Castle1NamePunRPC", RpcTarget.All, id);
        }
    }
    [PunRPC]
    void Castle2NamePunRPC(string id)
    {
        playerNameText[0].text = id;
    }
    public void SendCountButton2()
    {
        if (warriorCount2[1] > 0 || acherCount2[1] > 0 || tntCount2[1] > 0)
        {
            if (!sendBoolButton2)
            {
                StartCoroutine(SendCountButton2Corutin());
            }
            else
            {
                StartCoroutine(FadeInMesage("고블린을 보내는 중입니다."));
            }
        }
        else
        {
            StartCoroutine(FadeInMesage("보낼 고블린 수량이 0입니다."));
        }
    }
    IEnumerator SendCountButton2Corutin()
    {
        sendBoolButton2 = true;
        photonView.RPC("SendCountPunRPC1", RpcTarget.Others, warriorCount2[1], acherCount2[1], tntCount2[1]);
        yield return null;
        warriorCount2[0] = 60;
        acherCount2[0] = 80;
        tntCount2[0] = 120;
        warriorCount2[1] = 0;
        acherCount2[1] = 0;
        tntCount2[1] = 0;
        warriorInsCountPunRPC = 0;
        archerInsCountPunRPC = 0;
        bombInsCountPunRPC = 0;
        player2goblineCountMPU[3].text = "" + warriorCount2[0];
        player2goblineCountMPU[0].text = "" + warriorCount2[1];
        player2goblineCountMPU[4].text = "" + acherCount2[0];
        player2goblineCountMPU[1].text = "" + acherCount2[1];
        player2goblineCountMPU[5].text = "" + tntCount2[0];
        player2goblineCountMPU[2].text = "" + tntCount2[1];
        StartCoroutine(FadeInMesage("고블린을 성공적으로 전송하였습니다."));
        sendBoolButton2 = false;
    }
    IEnumerator FadeInMesage(string sendMesage)
    {
        float fadeTime = 1;
        Color textColor = sendButtonMesage.color = Color.white;
        textColor.a = 1f; // 알파 값 설정
        sendButtonMesage.text = sendMesage;
        yield return new WaitForSeconds(2);
        while (fadeTime > 0)
        {
            fadeTime -= Time.deltaTime;
            textColor.a = 1f * fadeTime; // 알파 값 설정
            sendButtonMesage.color = textColor;
            yield return null;
        }
    }

    [PunRPC]
    void SendCountPunRPC2(int wg1, int ag1, int tg1)
    {
        warriorInsCountPunRPC = wg1;
        archerInsCountPunRPC = ag1;
        bombInsCountPunRPC = tg1;
    }
    public void UI()
    {
        castleHpTMPU[1].text = "" + Castle2.castle2Hp;
        attackCoinText.text = "" + attackCoins;
    }

    public void Victory()
    {
        GameVictoryGameObject[0].SetActive(false);
        GameVictoryGameObject[1].SetActive(false);
        GameVictoryGameObject[2].SetActive(false);
        GameVictoryGameObject[3].SetActive(false);
        GameVictoryGameObject[4].SetActive(false);
        GameVictoryGameObject[5].SetActive(false);
    }
    public int buttoncount;
    public void multiUIButton()
    {
        buttoncount += 1;
    }
    void mobileUI()
    {
        if (buttoncount == 1)
        {
            multiUI.SetActive(true);
        }
        else if (buttoncount == 2)
        {
            buttoncount = 0;
            multiUI.SetActive(false);
        }
    }

    IEnumerator attackCoinsButtonMesageFadeIn()
    {
        float fadeTime = 1;
        Color textColor = attackCoinsButtonMesage.color = Color.white;
        textColor.a = 1f; // 알파 값 설정
        if (bridgeBuild.bridgeBuildTime > 0)
        {
            attackCoinsButtonMesage.text = "다리 건설이 완료되면 공격 할수 있습니다";
        }
        else
        {
            attackCoinsButtonMesage.text = "공격 코인이 부족합니다.....";
        }

        yield return new WaitForSeconds(2);
        while (fadeTime > 0)
        {
            fadeTime -= Time.deltaTime;
            textColor.a = 1f * fadeTime; // 알파 값 설정
            attackCoinsButtonMesage.color = textColor;
            yield return null;
        }
    }


    public void WG2()
    {
        if (attackCoins > 60 && bridgeBuild.bridgeBuildTime < 0)
        {
            warriorCount2[0] += 60;
            warriorCount2[1] += 1;
            attackCoins -= 60;
            player2goblineCountMPU[3].text = "" + warriorCount2[0];
            player2goblineCountMPU[0].text = "" + warriorCount2[1];
        }
        else
        {
            StartCoroutine(attackCoinsButtonMesageFadeIn());
        }
    }
    public void AG2()
    {
        if (attackCoins > 80 && bridgeBuild.bridgeBuildTime < 0)
        {
            acherCount2[0] += 80;
            acherCount2[1] += 1;
            attackCoins -= 80;
            player2goblineCountMPU[4].text = "" + acherCount2[0];
            player2goblineCountMPU[1].text = "" + acherCount2[1];
        }
        else
        {
            StartCoroutine(attackCoinsButtonMesageFadeIn());
        }
    }
    public void TG2()
    {
        if (attackCoins > 120 && bridgeBuild.bridgeBuildTime < 0)
        {
            tntCount2[0] += 120;
            tntCount2[1] += 1;
            attackCoins -= 120;
            player2goblineCountMPU[5].text = "" + tntCount2[0];
            player2goblineCountMPU[2].text = "" + tntCount2[1];
        }
        else
        {
            StartCoroutine(attackCoinsButtonMesageFadeIn());
        }
    }




    [PunRPC]
    void loadGame(int playerOrder)
    {

        Debug.Log(playerOrder);

        // 동기화된 플레이어의 상태를 설정
        syncedPlayerOrder = playerOrder;

        // 동기화된 플레이어의 상태에 따라 이미지와 위치를 설정
        if (playerOrder == 1)
        {
            Debug.Log("접속완료");
        }
        else if (playerOrder == 2)
        {
            bridgeBuild.sceneLoaded = true;
        }
    }


    public void ViewPlayer()
    {
        mobileUI();
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            multiUI.SetActive(true);
        }
        if (Input.GetKeyUp(KeyCode.Tab))
        {
            multiUI.SetActive(false);

        }
    }
}
