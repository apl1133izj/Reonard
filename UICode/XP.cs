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
        if (cuserKind.cuserKind[0])//����xp
        {
            xpKind[3] = true;
            xpText.text = "" + 10;
        }
        if (cuserKind.cuserKind[1])//��xp
        {
            xpKind[4] = true;
            xpText.text = "" + 30;
        }
        if (cuserKind.cuserKind[2])//����xp
        {
            xpText.text = "" + 40;
            xpKind[5] = true;
        }
        if (cuserKind.cuserKind[3])//�� ����xp
        {
            xpText.text = "" + 10;
            xpKind[6] = true;
        }
        if (cuserKind.cuserKind[4])//�� ����xp
        {
            xpText.text = "" + 10;
            xpKind[7] = true;
        }


    }
    public void XPUP()
    {
        if (xpKind[0])//�� ����
        {
            gameManager.levelGauge += 0.05f;
            Destroy(gameObject);
        }
        else if (xpKind[1])//���� ����
        {
            gameManager.levelGauge += 0.04f;
            Destroy(gameObject);
        }
        else if (xpKind[2])//Ÿ�� ����
        {
            gameManager.levelGauge += 0.03f;
            Destroy(gameObject);
        }
        else if (xpKind[3])//���� �ݱ�
        {
            gameManager.levelGauge += 0.02f;
            Destroy(gameObject);
        }
        else if (xpKind[4])//�� �ݱ�
        {
            gameManager.levelGauge += 0.04f;
            Destroy(gameObject);
        }
        else if (xpKind[5])//���� �ݱ�
        {
            gameManager.levelGauge += 0.05f;
            Destroy(gameObject);
        }
        else if (xpKind[6])//�� ����
        {
            gameManager.levelGauge += 0.02f;
            Destroy(gameObject);
        }
        else if (xpKind[7])//�� ����
        {
            gameManager.levelGauge += 0.02f;
            Destroy(gameObject);
        }
    }
}
