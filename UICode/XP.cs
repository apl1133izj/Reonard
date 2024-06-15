using TMPro;
using UnityEngine;
public class XP : MonoBehaviour
{
    GameManager gameManager;
    buildeMouseAndTouch buildeMouseAndTouchs;
    CuserKind cuserKind;
    public TextMeshProUGUI xpText;
    public bool[] xpKind;
    private void Awake()
    {
        gameManager = FindObjectOfType<GameManager>();
        buildeMouseAndTouchs = FindObjectOfType<buildeMouseAndTouch>();
        cuserKind = FindObjectOfType<CuserKind>();
        XPText();
    }
    void XPText()
    {
        if (buildeMouseAndTouchs.buildKind == buildeMouseAndTouch.BuildKind.BuildHouse)
        {
            xpKind[0] = true;
            xpText.text = "" + 40;
        }
        else if (buildeMouseAndTouchs.buildKind == buildeMouseAndTouch.BuildKind.BuildMine)
        {
            xpKind[1] = true;
            xpText.text = "" + 30;
        }
        else if (buildeMouseAndTouchs.buildKind == buildeMouseAndTouch.BuildKind.BuildTower)
        {
            xpKind[2] = true;
            xpText.text = "" + 20;
        }
        if (cuserKind.cuserKind[0])//나무xp
        {
            xpKind[3] = true;
            xpText.text = "" + 10;
        }
        if (cuserKind.cuserKind[1])//돈xp
        {
            xpKind[4] = true;
            xpText.text = "" + 30;
        }
        if (cuserKind.cuserKind[2])//음식xp
        {
            xpText.text = "" + 40;
            xpKind[5] = true;
        }
        if (cuserKind.cuserKind[3])//양 생성xp
        {
            xpText.text = "" + 10;
            xpKind[6] = true;
        }
        if (cuserKind.cuserKind[4])//양 도축xp
        {
            xpText.text = "" + 10;
            xpKind[7] = true;
        }


    }
    public void XPUP()
    {
        if (xpKind[0])//집 짓기
        {
            gameManager.levelGauge += 0.05f;
            Destroy(gameObject);
        }
        else if (xpKind[1])//광산 짓기
        {
            gameManager.levelGauge += 0.04f;
            Destroy(gameObject);
        }
        else if (xpKind[2])//타워 짓기
        {
            gameManager.levelGauge += 0.03f;
            Destroy(gameObject);
        }
        else if (xpKind[3])//나무 줍기
        {
            gameManager.levelGauge += 0.02f;
            Destroy(gameObject);
        }
        else if (xpKind[4])//돈 줍기
        {
            gameManager.levelGauge += 0.04f;
            Destroy(gameObject);
        }
        else if (xpKind[5])//음식 줍기
        {
            gameManager.levelGauge += 0.05f;
            Destroy(gameObject);
        }
        else if (xpKind[6])//양 생성
        {
            gameManager.levelGauge += 0.02f;
            Destroy(gameObject);
        }
        else if (xpKind[7])//양 도축
        {
            gameManager.levelGauge += 0.02f;
            Destroy(gameObject);
        }
    }
}
