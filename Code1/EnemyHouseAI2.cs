using Photon.Pun;
using System.Collections;
using UnityEngine;
public class EnemyHouseAI2 : MonoBehaviourPun
{
    public Multimanager1 multimanager2;
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
    bool sendBoolButton2 = false;
    bool coroutineRunning;
    void Start()
    {
        multimanager2 = FindObjectOfType<Multimanager1>();
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

        if (sendBoolButton2 && !coroutineRunning)
        {
            StartCoroutine(GoblineSendTime(multimanager2.warriorInsCountPunRPC, multimanager2.archerInsCountPunRPC, multimanager2.bombInsCountPunRPC));
        }
    }

    public void SendButton()
    {
        photonView.RPC("SendBool1", RpcTarget.Others, true);
    }
    [PunRPC]
    public void SendBool2(bool sendBool2)
    {
        sendBoolButton2 = sendBool2;
    }

    IEnumerator GoblineSendTime(int warriorInsCountCorutin2, int archerInsCountCorutin2, int bombInsCountCorutin2)
    {
        coroutineRunning = true;
        if (warriorInsCountCorutin2 != 0)
        {
            Debug.Log("warriorInsCountCorutin");
            for (int i = warriorInsCountCorutin2; i > 0; --i)
            {
                multimanager2.warriorInsCountPunRPC -= i;
                Debug.Log("행동 중");
                InstantiateGoblin(0);
                yield return new WaitForSeconds(1);
            }
        }

        yield return new WaitForSeconds(1);

        if (archerInsCountCorutin2 != 0)
        {
            Debug.Log("archerInsCountCorutin");
            for (int i = archerInsCountCorutin2; i > 0; --i)
            {
                multimanager2.archerInsCountPunRPC -= i;
                Debug.Log("행동 중");
                InstantiateGoblin(1);
                yield return new WaitForSeconds(1);
            }
        }

        yield return new WaitForSeconds(1);

        if (bombInsCountCorutin2 != 0)
        {
            Debug.Log("bombInsCountCorutin");
            for (int i = bombInsCountCorutin2; i > 0; --i)
            {
                multimanager2.bombInsCountPunRPC -= i;
                Debug.Log("행동 중");
                InstantiateGoblin(2);
                yield return new WaitForSeconds(1);
            }
        }
        coroutineRunning = false;
        sendBoolButton2 = false;
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
