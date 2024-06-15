using Photon.Pun;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class Multimanager : MonoBehaviourPun
{
    public GameObject multiUI;
    // ���� RPC�� ����Ͽ� �ٸ� �÷��̾��� ���¸� ����ȭ
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
    public int[] warriorCount = { 60, 0 };
    public int[] acherCount = { 80, 0 };
    public int[] tntCount = { 120, 0 };

    //SendGobline;
    public int warriorInsCountPunRPC;
    public int archerInsCountPunRPC;
    public int bombInsCountPunRPC;

    public bool sendBoolButton;
    public bool gameOverGet;

    public GameObject[] GameVictoryGameObject;
    private void Start()
    {
        if (PhotonNetwork.IsConnectedAndReady)
        {
            syncedPlayerOrder = LobbyManager.playerOrder;
            photonView.RPC("loadGame", RpcTarget.AllBuffered, syncedPlayerOrder);
            Castle1Name();
        }
    }
    void Update()
    {
        ViewPlayer();
        UI();
        if (PhotonNetwork.IsConnectedAndReady)
        {
            Castle1Hp();
            GameOver();
        }
    }
    // �� ü�� UI�� ����ȭ�ϴ� �޼���
    void Castle1Hp()
    {
        if (PhotonNetwork.IsConnectedAndReady)
        {
            photonView.RPC("PunRPCCastle2Hp", RpcTarget.All, Castle1.castle1Hp);
        }
    }
    [PunRPC]
    void PunRPCCastle1Hp(int hp1)
    {
        castleHpTMPU[1].text = hp1.ToString();
    }

    void GameOver()
    {
        photonView.RPC("GameOver2PunRPC", RpcTarget.All, Castle1.gameOver);
        if (gameOverGet)
        {
            castleImage.sprite = castleSprite;
            Victory();
            Debug.Log("���ӿ���1");
        }
    }


    [PunRPC]
    void GameOver1PunRPC(bool _gameOver)
    {
        gameOverGet = _gameOver;
    }
    void Castle1Name()
    {

        string email = AuthManager.User.Email;
        int atIndex = email.IndexOf('@');
        if (atIndex != -1)
        {
            string id = email.Substring(0, atIndex);
            playerNameText[0].text = id;
            photonView.RPC("Castle2NamePunRPC", RpcTarget.All, id);
        }

    }

    [PunRPC]
    void Castle1NamePunRPC(string id)
    {
        playerNameText[1].text = id;
    }

    public void SendCountButton1()
    {
        if (warriorCount[1] > 0 || acherCount[1] > 0 || tntCount[1] > 0)
        {
            if (!sendBoolButton)
            {
                StartCoroutine(SendCountButton1Corutin());
            }
            else
            {
                StartCoroutine(FadeInMesage("����� ������ ���Դϴ�."));
            }
        }
        else
        {
            StartCoroutine(FadeInMesage("���� ��� ������ 0�Դϴ�."));
        }
    }

    IEnumerator SendCountButton1Corutin()
    {
        sendBoolButton = true;
        photonView.RPC("SendCountPunRPC2", RpcTarget.Others, warriorCount[1], acherCount[1], tntCount[1]);
        yield return new WaitForSeconds(2);
        warriorCount[0] = 60;
        acherCount[0] = 80;
        tntCount[0] = 120;
        warriorCount[1] = 0;
        acherCount[1] = 0;
        tntCount[1] = 0;
        warriorInsCountPunRPC = 0;
        archerInsCountPunRPC = 0;
        bombInsCountPunRPC = 0;
        player1goblineCountMPU[3].text = "" + warriorCount[0];
        player1goblineCountMPU[0].text = "" + warriorCount[1];
        player1goblineCountMPU[4].text = "" + acherCount[0];
        player1goblineCountMPU[1].text = "" + acherCount[1];
        player1goblineCountMPU[5].text = "" + tntCount[0];
        player1goblineCountMPU[2].text = "" + tntCount[1];
        StartCoroutine(FadeInMesage("����� ���������� �����Ͽ����ϴ�."));
        sendBoolButton = false;
    }
    IEnumerator FadeInMesage(string sendMesage)
    {
        float fadeTime = 1;
        Color textColor = sendButtonMesage.color = Color.white;
        textColor.a = 1f; // ���� �� ����
        sendButtonMesage.text = sendMesage;
        yield return new WaitForSeconds(2);
        while (fadeTime > 0)
        {
            fadeTime -= Time.deltaTime;
            textColor.a = 1f * fadeTime; // ���� �� ����
            sendButtonMesage.color = textColor;
            yield return null;
        }
    }
    [PunRPC]
    void SendCountPunRPC1(int wg1, int ag1, int tg1)
    {
        warriorInsCountPunRPC = wg1;
        archerInsCountPunRPC = ag1;
        bombInsCountPunRPC = tg1;
    }
    public void UI()
    {
        castleHpTMPU[0].text = "" + Castle1.castle1Hp;

        attackCoinText.text = "" + attackCoins;
    }

    public void Victory()
    {
        GameVictoryGameObject[0].SetActive(false);
        GameVictoryGameObject[1].SetActive(false);
        GameVictoryGameObject[2].SetActive(false);
        GameVictoryGameObject[3].SetActive(false);
        GameVictoryGameObject[4].SetActive(true);
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
        textColor.a = 1f; // ���� �� ����
        if (bridgeBuild.bridgeBuildTime > 0)
        {
            attackCoinsButtonMesage.text = "�ٸ� �Ǽ��� �Ϸ�Ǹ� ���� �Ҽ� �ֽ��ϴ�";
        }
        else
        {
            attackCoinsButtonMesage.text = "���� ������ �����մϴ�.....";
        }
        yield return new WaitForSeconds(2);
        while (fadeTime > 0)
        {
            fadeTime -= Time.deltaTime;
            textColor.a = 1f * fadeTime; // ���� �� ����
            attackCoinsButtonMesage.color = textColor;
            yield return null;
        }
    }
    public void WG1()
    {
        if (attackCoins > 60 && bridgeBuild.bridgeBuildTime < 0)
        {
            warriorCount[0] += 60;
            warriorCount[1] += 1;
            attackCoins -= 60;
            player1goblineCountMPU[3].text = "" + warriorCount[0];
            player1goblineCountMPU[0].text = "" + warriorCount[1];
        }
        else
        {
            StartCoroutine(attackCoinsButtonMesageFadeIn());
        }
    }
    public void AG1()
    {
        if (attackCoins > 80 && bridgeBuild.bridgeBuildTime < 0)
        {
            acherCount[0] += 80;
            acherCount[1] += 1;
            attackCoins -= 80;
            player1goblineCountMPU[4].text = "" + acherCount[0];
            player1goblineCountMPU[1].text = "" + acherCount[1];
        }
        else
        {
            StartCoroutine(attackCoinsButtonMesageFadeIn());
        }
    }
    public void TG1()
    {
        if (attackCoins > 120 && bridgeBuild.bridgeBuildTime < 0)
        {
            tntCount[0] += 120;
            tntCount[1] += 1;
            attackCoins -= 120;
            player1goblineCountMPU[5].text = "" + tntCount[0];
            player1goblineCountMPU[2].text = "" + tntCount[1];
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

        // ����ȭ�� �÷��̾��� ���¸� ����
        syncedPlayerOrder = playerOrder;

        // ����ȭ�� �÷��̾��� ���¿� ���� �̹����� ��ġ�� ����
        if (playerOrder == 1)
        {
            Debug.Log("���ӿϷ�");
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
