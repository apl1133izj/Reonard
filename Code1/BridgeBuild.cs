using TMPro;
using UnityEngine;
using Photon.Pun;
using UnityEngine.Analytics;

public class BridgeBuild : MonoBehaviourPunCallbacks
{
    public float bridgeBuildTime = 480;
    public float victoryTime = 1200;
    public TextMeshProUGUI bridgeBuildTimeText;
    public TextMeshProUGUI gameText;
    public GameManager gameManager;
    public GameObject playerOnlineBoolGameObject;

    public bool sceneLoaded;

    void Update()
    {

        if (sceneLoaded || !PhotonNetwork.IsMasterClient)
        {
            if (PhotonNetwork.IsMasterClient)
            {
                playerOnlineBoolGameObject.SetActive(false);
            }
            if (Mathf.Round(bridgeBuildTime) != 0)
            {
                gameText.text = "다리 건설 까지";
                bridgeBuildTime -= Time.deltaTime;
                bridgeBuildTimeText.text = "" + Mathf.RoundToInt(bridgeBuildTime);
            }
            if (Mathf.Round(bridgeBuildTime) <= 0)
            {
                gameText.text = "게임 승리 까지";
                victoryTime -= Time.deltaTime;
                bridgeBuildTimeText.text = "" + Mathf.RoundToInt(victoryTime);
            }
        }
    }
}
