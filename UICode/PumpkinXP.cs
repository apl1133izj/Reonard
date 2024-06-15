using TMPro;
using UnityEngine;
public class PumpkinXP : MonoBehaviour
{
    GameManager gameManager;
    public TextMeshProUGUI xpText;
    bool pb;
    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        xpText.text = "" + 10;
        pb = true;
    }

    public void pumpkinXPs()
    {
        if (pb)
        {
            gameManager.levelGauge += 0.01f;
        }
    }
}
