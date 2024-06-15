using UnityEngine;

public class Tree : MonoBehaviour
{
    SpriteRenderer treesSriteRenderer;
    public Sprite cuttingTreeSprite;
    public Animator treeAnimator;
    public LayerMask layerMask;
    int cuttingIntMax = 30;

    public int cuttingInt;
    public bool treeHitBool;//나무를 베고 있는지
    public float resetTreeTime;
    bool treeReSetBool;
    public float overlapCheckRadius = 6f; // 오브젝트 간의 최소 거리
    float timeCutting;
    bool warkerOn;
    private void Awake()
    {
        treeAnimator = GetComponent<Animator>();
        treesSriteRenderer = GetComponent<SpriteRenderer>();
    }
    private void Update()
    {
        ResetTree();
        if (warkerOn && cuttingInt == 0)
        {
            timeCutting += Time.deltaTime;
        }
        else
        {
            timeCutting = 0;
        }
    }
    private void ResetTree()
    {

        if (cuttingInt == 31)
        {
            treeReSetBool = true;
            cuttingInt = 0;
        }
        if (treeReSetBool)
        {
            resetTreeTime += Time.deltaTime;
            if (resetTreeTime >= 200f)
            {
                treeAnimator.enabled = true;
                gameObject.tag = "Tree";
                resetTreeTime = 0;
                treeReSetBool = false;

            }
        }
    }
    Vector3 GetNonOverlappingPosition()
    {
        Vector3 spawnPosition;

        do
        {
            // 무작위 위치를 생성하거나 특정한 로직으로 위치를 결정합니다.
            spawnPosition = new Vector2(transform.position.x + Random.Range(-1f, 1f), transform.position.y + Random.Range(-1f, 1f));
        }
        while (CheckOverlap(spawnPosition));

        return spawnPosition;
    }

    bool CheckOverlap(Vector3 position)
    {
        // 새로운 위치 주변에 다른 오브젝트가 있는지 체크
        Collider[] colliders = Physics.OverlapSphere(position, overlapCheckRadius);

        // 다른 Collider가 존재하면 겹치는 것으로 간주
        return colliders.Length > 0;
    }
    public GameObject woodeInstGameObject;
    void InstantiatePrefab(Vector3 position)
    {
        if (cuttingInt % 6 == 0)
        {
            // 위치에 Prefab을 생성
            Instantiate(woodeInstGameObject, position, Quaternion.identity);
        }
    }
    
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Warker warker = collision.gameObject.GetComponent<Warker>();
        if (collision.gameObject.CompareTag("Warker"))
        {
            warkerOn = true;
            if (cuttingInt == cuttingIntMax)
            {
                warker.collider2D.enabled = false;
                treeAnimator.enabled = false;
                treesSriteRenderer.sprite = cuttingTreeSprite;
                treeHitBool = false;
                cuttingInt = 0;
                warker.warkerActionsBool = false;
                gameObject.tag = "CuttingTree";
                Debug.Log("나무 베기 완료");
            }

            
            if (timeCutting >= 0.95f || cuttingInt >= 1)
            {
                treeAnimator.SetTrigger("TreeHitTrigger");
                Vector3 spawnPosition = GetNonOverlappingPosition();
                InstantiatePrefab(spawnPosition);
                warkerOn = false;
                cuttingInt += 1;
                treeHitBool = true;
            }
        }
    }
}
