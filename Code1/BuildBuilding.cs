using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class BuildBuilding : MonoBehaviour
{
    public float buildTime = 101f;
    public float fillAmountTime = 0;
    public TextMeshProUGUI buildingText;
    public Image buildImage;
    public GameObject insPrefabBuild;


    
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Warker"))
        {

            float buildTimeRound;
            buildTime -= Time.deltaTime;
            buildTimeRound = Mathf.RoundToInt(buildTime);

            fillAmountTime += Time.deltaTime / 100;
            if (buildTimeRound <= 100)
            {
                buildingText.text = "" + buildTimeRound;
                // -681.31
                buildImage.fillAmount = fillAmountTime;
            }
            if (buildTime < 99.5f)
            {
                if (buildTime > 9.5f)
                {
                    buildingText.rectTransform.anchoredPosition = new Vector2(-681.31f, -578.1f);
                }
                else
                {
                    buildingText.rectTransform.anchoredPosition = new Vector2(-681.19f, -578.1f);
                }

            }

            if (buildImage.fillAmount == 1)
            {
                GameObject inshouse = Instantiate(insPrefabBuild, transform.position, Quaternion.identity);
                Destroy(gameObject);
            }
        }
    }


}
