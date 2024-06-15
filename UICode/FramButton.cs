using UnityEngine;
using UnityEngine.UI;
public class FramButton : MonoBehaviour
{
    public buildeMouseAndTouch bMTouch;
    public GameObject sheepInst;
    public GameManager manager;
    public Image[] activationButtonImage;
    private void Update()
    {
        if (manager.moneyCount < 40)
        {
            activationButtonImage[0].color = Color.gray;
            activationButtonImage[1].color = Color.gray;
        }
        else
        {
            activationButtonImage[0].color = Color.white;
            activationButtonImage[1].color = Color.white;
        }
        if (manager.pempkinCount < 1)
        {
            activationButtonImage[2].color = Color.gray;
            activationButtonImage[3].color = Color.gray;
        }
        else
        {
            activationButtonImage[2].color = Color.white;
            activationButtonImage[3].color = Color.white;
        }
    }
    public void PurchaseButton()
    {
        if (manager.moneyCount > 40)
        {
            manager.moneyCount -= 40;
            GameObject inst = Instantiate(sheepInst, new Vector2(1.6f, 1), Quaternion.identity);
        }

    }
    public void FeedButton()
    {
        if (manager.pempkinCount >= 1)
        {
            bMTouch.buildKind = buildeMouseAndTouch.BuildKind.Feed;
        }
    }

    public void SlaughterButton()
    {
        bMTouch.buildKind = buildeMouseAndTouch.BuildKind.Slaughter;
    }
}
