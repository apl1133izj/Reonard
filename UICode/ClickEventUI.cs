using TMPro;
using UnityEngine;
public class ClickEventUI : MonoBehaviour
{
    public enum ClickEventUIKind { WoodEvent, GoldEvent, FoodEvent, PumpkinEvent }
    public ClickEventUIKind kind;
    GameManager gameManager;
    public TextMeshProUGUI xpText;
    public bool[] ClickEventUIKindbool;
    private void Awake()
    {
        gameManager = FindObjectOfType<GameManager>();
    }
    void Start()
    {
        
        if (kind == ClickEventUIKind.WoodEvent)
        {
            ClickEventUIKindbool[0] = true;

        }
        else if (kind == ClickEventUIKind.GoldEvent)
        {
            ClickEventUIKindbool[1] = true;

        }
        else if (kind == ClickEventUIKind.FoodEvent)
        {
            ClickEventUIKindbool[2] = true;
            xpText.text = "X" + gameManager.foodeCountRandom;
        }
    }

    public void ClickEvent()
    {
        if (ClickEventUIKindbool[0])
        {
            gameManager.woodeCount += 1;
           
        }
        else if (ClickEventUIKindbool[1])
        {
            gameManager.moneyCount += 100;
           
        }
        else if (ClickEventUIKindbool[2])
        {
            gameManager.foodCount += gameManager.foodeCountRandom;
            
        }
    }
}
