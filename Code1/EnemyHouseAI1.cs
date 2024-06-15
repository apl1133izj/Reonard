using Photon.Pun;
using System.Collections;
using UnityEngine;
public class EnemyHouseAI1 : MonoBehaviourPun
{
    public Multimanager multimanager;
    public GameObject[] insGoblin; // 0: 전사, 1: 궁수, 2: 폭탄
    public BridgeBuild bridgeBuild;

    float warriorInsTime = 30f;
    float archerInsTime = 50f;
    float bombInsTime = 100f;

    public float warriorTimer = 0f;
    public float archerTimer = 0f;
    public float bombTimer = 0f;

    float timeElapsed = 0f;
    float increaseRate = 0.05f; // 생성 속도를 빠르게 하는 비율
    public float[] maxSpawnRate = { 20, 40, 90 }; // 최대 생성 속도

    [SerializeField]
    bool sendBoolButton1 = false;
    bool coroutineRunning;
    void Start()
    {
        multimanager = FindObjectOfType<Multimanager>();
    }

    void Update()
    {
        if (Mathf.Round(bridgeBuild.bridgeBuildTime) == 0)
        {
            timeElapsed += Time.deltaTime;

            // 일정 시간마다 고블린 생성
            warriorTimer += Time.deltaTime;
            archerTimer += Time.deltaTime;
            bombTimer += Time.deltaTime;

            if (warriorTimer >= warriorInsTime)
            {
                InstantiateGoblin(0);
                warriorTimer = 0f;
            }

            if (archerTimer >= archerInsTime)
            {
                InstantiateGoblin(1);
                archerTimer = 0f;
            }

            if (bombTimer >= bombInsTime)
            {
                InstantiateGoblin(2);
                bombTimer = 0f;
            }

            // 일정 시간마다 생성 속도 증가
            if (timeElapsed >= 60f && warriorInsTime > maxSpawnRate[0] && archerInsTime > maxSpawnRate[1] && bombInsTime > maxSpawnRate[2])
            {
                IncreaseSpawnRate();
                timeElapsed = 0f;
            }
        }
        if (PhotonNetwork.IsConnectedAndReady)
        {
            if (sendBoolButton1 && !coroutineRunning)
            {
                StartCoroutine(GoblineSendTime(multimanager.warriorInsCountPunRPC, multimanager.archerInsCountPunRPC, multimanager.bombInsCountPunRPC));
            }
        }
    }

    public void SendButton()
    {
        photonView.RPC("SendBool2", RpcTarget.Others, true);
    }
    [PunRPC]
    public void SendBool1(bool sendBool1)
    {
        sendBoolButton1 = sendBool1;
    }
    IEnumerator GoblineSendTime(int warriorInsCountCorutin, int archerInsCountCorutin, int bombInsCountCorutin)
    {
        coroutineRunning = true;
        if (warriorInsCountCorutin != 0)
        {
            Debug.Log("warriorInsCountCorutin");
            for (int i = warriorInsCountCorutin; i > 0; --i)
            {
                multimanager.warriorInsCountPunRPC -= i;
                Debug.Log("행동 중");
                InstantiateGoblin(0);
                yield return new WaitForSeconds(1);
            }
        }

        yield return new WaitForSeconds(1);

        if (archerInsCountCorutin != 0)
        {
            Debug.Log("archerInsCountCorutin");
            for (int i = archerInsCountCorutin; i > 0; --i)
            {
                multimanager.archerInsCountPunRPC -= i;
                Debug.Log("행동 중");
                InstantiateGoblin(1);
                yield return new WaitForSeconds(1);
            }
        }

        yield return new WaitForSeconds(1);

        if (bombInsCountCorutin != 0)
        {
            Debug.Log("bombInsCountCorutin");
            for (int i = bombInsCountCorutin; i > 0; --i)
            {
                Debug.Log("행동 중");
                multimanager.bombInsCountPunRPC -= i;
                InstantiateGoblin(2);
                yield return new WaitForSeconds(1);
            }
        }
        coroutineRunning = false;
        sendBoolButton1 = false;
    }

    void InstantiateGoblin(int goblinType)
    {
        Instantiate(insGoblin[goblinType], transform.position, Quaternion.identity);
    }

    void IncreaseSpawnRate()
    {
        // 생성 속도를 빠르게 함
        warriorInsTime *= (1f - increaseRate);
        archerInsTime *= (1f - increaseRate);
        bombInsTime *= (1f - increaseRate);

        // 최대 생성 속도를 넘지 않도록 함
        warriorInsTime = Mathf.Max(warriorInsTime, maxSpawnRate[0]);
        archerInsTime = Mathf.Max(archerInsTime, maxSpawnRate[1]);
        bombInsTime = Mathf.Max(bombInsTime, maxSpawnRate[2]);
    }
}
