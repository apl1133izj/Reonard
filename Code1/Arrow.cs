using UnityEngine;

public class Arrow : MonoBehaviour
{
    public float moveSpeed;//플레이어 스피드
    public float checkRadius = 1.0f; // 확인할 반경
    public LayerMask goblinLayer;
    public bool goblinActionsBool;//나무에 다른 고블린이 있는지 
    public string tagName;//고블린이 찾는 tag
    public bool goblinFound;//일 할 거리를 찾았는가
    public bool mineWarkeEnd;
    float stopPosX;
    float stopPosy;
    Animator animator;
    Rigidbody2D rigidbody;
    public LayerMask wakerlayerMask;
    public LayerMask castlelayerMask;

    public float archerDamage = 10.0f;
    private Vector2 previousPosition;
    public float angleDegrees;
    private void Awake()
    {
        animator = GetComponent<Animator>();

    }
    private void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();

    }
    void Update()
    {
        ArrowAction();
        //ArrowFlip();
    }
    void ArrowFlip(Vector2 rotion)
    {
        if (transform.position.x > previousPosition.x)
        {
            transform.localScale = new Vector2(1, 1);
        }
        else if (transform.position.x < previousPosition.x)
        {
            transform.localScale = new Vector2(1, 1);
        }


        // 상대 위치 계산
        Vector2 relativePosition = rotion - (Vector2)transform.position;

        // 상대 위치와의 각도 계산 (라디안 단위)
        float angleRadians = Mathf.Atan2(relativePosition.y, relativePosition.x);

        // 라디안에서 각도로 변환
        float angleDegrees = angleRadians * Mathf.Rad2Deg;
        Debug.Log(angleDegrees);
        // 회전 설정
        transform.rotation = Quaternion.Euler(0, 0, angleDegrees);

        previousPosition = transform.position;
    }
    public void ArrowAction()
    {
        GameObject[] warkerActionGameObject = GameObject.FindGameObjectsWithTag(tagName);
        if (warkerActionGameObject.Length > 0)
        {
            GameObject closestTree = null;
            float closestDistance = Mathf.Infinity;

            // 모든 나무에 대해 거리를 계산하고 가장 가까운 나무 찾기
            foreach (GameObject warkerActions in warkerActionGameObject)
            {
                float distanceToTree = Vector2.Distance(transform.position, new Vector2(warkerActions.transform.position.x + stopPosX, warkerActions.transform.position.y + stopPosy));
                //Debug.Log(distanceToTree);
                if (distanceToTree < closestDistance)
                {
                    // 현재 나무가 더 가까우면 업데이트
                    closestTree = warkerActions;
                    closestDistance = distanceToTree;
                }
            }

            if (closestTree != null)
            {
                // 가장 가까운 나무에 대한 처리 수행
                // Debug.Log("가장 가까운 나무: " + closestTree.name + ", 거리: " + closestDistance);
                //나무를 베고있을 경우 true
                // 가장 가까운 gameobject(나무,광산)에 대한 추가 작업

                if (!goblinActionsBool)
                {
                    Vector2 targetPosition = new Vector2(closestTree.transform.position.x + stopPosX, closestTree.transform.position.y + stopPosy);
                    transform.position = Vector2.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
                    ArrowFlip(targetPosition);
                }
                if (closestDistance <= 0f)
                {
                    //"히트"
                    Destroy(gameObject);
                    goblinActionsBool = true;
                }
            }
        }
        else
        {
            goblinActionsBool = false;
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Warker"))
        {
            moveSpeed = 0;
            Destroy(gameObject);
        }
        if (collision.gameObject.CompareTag("GoblineWarrior"))
        {
            moveSpeed = 0;
            Destroy(gameObject);
        }
        if (collision.gameObject.CompareTag("Gobline Archer"))
        {
            moveSpeed = 0;
            Destroy(gameObject);
        }
        if (tagName == "Castle")
        {
            if (collision.gameObject.CompareTag("Castle"))
            {
                moveSpeed = 0;
                Destroy(gameObject);
            }
        }
    }
}
