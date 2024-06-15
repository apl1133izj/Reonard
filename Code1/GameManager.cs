using Photon.Pun;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public Castle1 castle1;
    public Castle2 castle2;
    public BridgeBuild bridgeBuild;
    public Animator uiAnimator;
    int animatorCount;
    public Image image;
    public Sprite[] buttonChangeImage;
    public TextMeshProUGUI[] textMeshProUGUI; //��,����,����
    public int woodeCount;
    public int foodCount;
    public int moneyCount;
    public int pempkinCount;

    public int foodeCountRandom;

    public float levelGauge;
    public Image levelImage;
    public int levelcount = 0;
    public TextMeshProUGUI leveText;

    public float bridgeBuildTime;

    
    public GameObject[] goMenu;
    public GameObject[] gameStartGameObject;
    public GameObject insMap;
    public GameObject startTextGameObject;//(StartTextGameObject)
    public float gameClearTime;

    private void Update()
    {
        UI();
        Level();
        GameOver();
        if (bridgeBuild.bridgeBuildTime >= 0f)
        {
            GameClear();
        }

    }
    void Level()
    {
        levelImage.fillAmount = 0 + levelGauge;
        if (levelcount == 1)
        {
            leveText.rectTransform.anchoredPosition = new Vector2(82.7f, 44.2f);

        }
        leveText.text = "" + levelcount;

        if ((int)levelGauge == 1 + levelcount)
        {
            if (castle1 != null)
            {
                if (castle1.playerenum == Castle1.Players.player1)
                {
                    Castle1.castle1MAXHp += 100;
                    Castle1.castle1Hp = Castle1.castle1MAXHp;
                }
            }
            if (castle2 != null)
            {
                if (castle2.playerenum == Castle2.Players.player2)
                {
                    Castle2.castle2MAXHp += 100;
                    Castle2.castle2Hp = Castle2.castle2MAXHp;
                }
            }
            foodCount += 200;
            moneyCount += 200;
            pempkinCount += 5;
            gameClearTime -= 20;
            levelGauge = 0;
            levelcount++;
        }


    }
    void UI()
    {
        textMeshProUGUI[0].text = "" + moneyCount;
        if (moneyCount <= 9)
        {
            textMeshProUGUI[0].rectTransform.anchoredPosition = new Vector2(6f, 13.4f);
        }
        else if (moneyCount <= 99)
        {
            textMeshProUGUI[0].rectTransform.anchoredPosition = new Vector2(-2.7f, 13.4f);
        }
        else if (moneyCount >= 100)
        {
            textMeshProUGUI[0].rectTransform.anchoredPosition = new Vector2(-11.4f, 13.4f);
        }
        textMeshProUGUI[1].text = "" + woodeCount;
        if (woodeCount <= 9)
        {
            textMeshProUGUI[1].rectTransform.anchoredPosition = new Vector2(-1f, -0.8f);
        }
        else if (woodeCount <= 99)
        {
            textMeshProUGUI[1].rectTransform.anchoredPosition = new Vector2(-8.9f, -0.8f);
        }
        else if (woodeCount >= 100)
        {
            textMeshProUGUI[1].rectTransform.anchoredPosition = new Vector2(-17.9f, -0.8f);
        }
        textMeshProUGUI[2].text = "" + foodCount;
        if (foodCount <= 9)
        {
            textMeshProUGUI[2].rectTransform.anchoredPosition = new Vector2(0.3f, 42.7f);
        }
        else if (foodCount <= 99)
        {
            textMeshProUGUI[2].rectTransform.anchoredPosition = new Vector2(-6.9f, 42.7f);
        }
        else if (foodCount >= 100)
        {
            textMeshProUGUI[2].rectTransform.anchoredPosition = new Vector2(-16.5f, 42.7f);
        }
        textMeshProUGUI[3].text = "" + pempkinCount;
        if (pempkinCount <= 9)
        {
            textMeshProUGUI[3].rectTransform.anchoredPosition = new Vector2(0.3f, 42.7f);
        }
        else if (pempkinCount <= 99)
        {
            textMeshProUGUI[3].rectTransform.anchoredPosition = new Vector2(-6.9f, 42.7f);
        }
        else if (pempkinCount >= 100)
        {
            textMeshProUGUI[3].rectTransform.anchoredPosition = new Vector2(-16.5f, 42.7f);
        }
    }
    public void UIAnimator()
    {
        uiAnimator.enabled = true;
        animatorCount += 1;
        if (animatorCount == 1)
        {
            image.sprite = buttonChangeImage[1];
            uiAnimator.SetBool("Down", false);
        }
        else if (animatorCount == 2)
        {
            animatorCount = 0;
            image.sprite = buttonChangeImage[0];

            uiAnimator.SetBool("Down", true);
        }
    }

    public void GOTitle()
    {
        goMenu[0].SetActive(true);
        goMenu[1].SetActive(true);
        goMenu[2].SetActive(true);
        goMenu[3].SetActive(true);
        startTextGameObject.SetActive(false);
    }

    public void GameStart()
    {
       
        if (!PhotonNetwork.IsConnected)
        {
            gameStartGameObject[0].SetActive(false);
        }
        gameStartGameObject[0].SetActive(false);
        gameStartGameObject[1].SetActive(true);
        gameStartGameObject[2].SetActive(true);
        gameStartGameObject[3].SetActive(true);
        gameStartGameObject[4].SetActive(true);
        gameStartGameObject[5].SetActive(true);


    }

    public void GameClear()
    {
        
        gameClearTime += Time.deltaTime;
        if ((int)gameClearTime == 1200)
        {
            gameStartGameObject[1].SetActive(false);
            gameStartGameObject[2].SetActive(false);
            gameStartGameObject[3].SetActive(false);
            gameStartGameObject[4].SetActive(false);
            gameStartGameObject[5].SetActive(false);
            gameStartGameObject[7].SetActive(true);
            gameStartGameObject[8].SetActive(false);
        }
    }
    public void GameOver()
    {
        if (Castle1.castle1Hp < 0)
        {

            GameObject[] allObjects = FindObjectsOfType<GameObject>();

          
            foreach (GameObject obj in allObjects)
            {
                
                if (obj.name.Contains("(Clone)"))
                {
                    
                    Destroy(obj);
                }
            }
            gameStartGameObject[1].SetActive(false);
            gameStartGameObject[2].SetActive(false);
            gameStartGameObject[3].SetActive(false);
            gameStartGameObject[4].SetActive(false);
            gameStartGameObject[5].SetActive(false);
            gameStartGameObject[6].SetActive(true);
            gameStartGameObject[8].SetActive(false);
        }
        if (Castle2.castle2Hp < 0)
        {
            
            GameObject[] allObjects = FindObjectsOfType<GameObject>();

           
            foreach (GameObject obj in allObjects)
            {
              
                if (obj.name.Contains("(Clone)"))
                {
                    
                    Destroy(obj);
                }
            }

            gameStartGameObject[1].SetActive(false);
            gameStartGameObject[2].SetActive(false);
            gameStartGameObject[3].SetActive(false);
            gameStartGameObject[4].SetActive(false);
            gameStartGameObject[5].SetActive(false);
            gameStartGameObject[6].SetActive(true);
            gameStartGameObject[8].SetActive(false);
        }
    }
    public void RestartGame()
    {
        
       
        gameStartGameObject[1].SetActive(false);
        gameStartGameObject[2].SetActive(false);
        gameStartGameObject[3].SetActive(false);
        gameStartGameObject[4].SetActive(false);
        gameStartGameObject[5].SetActive(false);
        gameStartGameObject[6].SetActive(false); 
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        gameStartGameObject[0].SetActive(true);
        
    }
    public void MenuButton()
    {
        SceneManager.LoadScene("SampleScene");
    }
    public void MuityButton()
    {
        SceneManager.LoadScene("SignIn");
    }
    public void QuitGame()
    {
       
        Application.Quit();
    }
}
