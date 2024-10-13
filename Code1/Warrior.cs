using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Warrior : MonoBehaviour
{
    public Ground ground;
    public Animator warriorAnimator;
    public LayerMask warriorLayer;
    public LayerMask groundLayer;
    private Vector2 previousPosition;
    public GameObject buildEffectPrefab;
    public Image hpImage;
    public GameObject attackGame;
    public GameObject reSearchTagGameObject;
    Rigidbody2D rigidbody2;

    public float moveSpeed;//플레이어 스피트
    public float checkRadius = 1.0f; // 확인할 반경
    public bool goblinActionsBool;//나무에 다른 고블린이 있는지 
    public bool treeHitBool;//나무를 베고 있는지
    public string tagName;//고블린이 찾는 tag
    public string houseName;//고블린이 일을 다한 후 찾는 집 이름 
    public bool goblinFound;//일 할 거리를 찾았는가
    public bool goHouseBool;//집으로 돌아가는 중인가
    public bool mineWarkeEnd;

    float stopPosX;
    float stopPosy;

    //부딪힌 콜라이더 크기 
    float colliderWidth;
    float colliderHeight;

    public float attackRange = 1f;


    public bool attackRangeBool;

    public bool searchBool;
    public bool groundBool;
    public bool bridgeFalse;
    public bool avoidingBool;//다른 건물과 부딪치는지 확인
    public bool avoidingSaveBool;//다른 건물과 부딪치는지 확인

    public bool up;
    public bool down;

    public float warriorHp = 200.0f;

    public AudioSource knifeAudioSource;

    bool isGround;
    private void Awake()
    {
        rigidbody2 = GetComponent<Rigidbody2D>();
        ground = FindObjectOfType<Ground>();
        warriorAnimator = GetComponent<Animator>();
    }
    void Start()
    {
        previousPosition = transform.position;
    }
    void Update()
    {
        if (ground.goblinInvasionbool && !avoidingSaveBool)
        {
            WarriorAction();
        }
        if (avoidingBool)
        {
            GoHouse();
        }
        HP();

        Avoiding();
        
        if (avoidingSaveBool)
        {
            StartCoroutine(AvoidingCoroutine());
        }
    }
    public void HP()
    {
        if (transform.localScale.x == -1)
        {
            hpImage.rectTransform.localScale = new Vector2(0.022f, 0.035f);
        }
        else
        {
            hpImage.rectTransform.localScale = new Vector2(-0.022f, 0.035f);
        }
        if (hpImage.fillAmount <= 0)
        {
            gameObject.tag = "Untagged";
            gameObject.layer = 0;
            moveSpeed = 0f;
            warriorAnimator.SetBool("DeadBool", true);
        }
    }
    public void Die()
    {
        Destroy(gameObject);
    }
    void WakerFlip(Vector2 targetPosition)
    {
        if (transform.position.x < targetPosition.x)
        { 
            transform.localScale = new Vector2(1, 1);
        }
        else if (transform.position.x > targetPosition.x)
        {
            transform.localScale = new Vector2(-1, 1);
        }
    }
    void Avoiding()
    {
        //콜라이더의 크기를 확인해서 피하는 코드
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, 1);
        foreach (Collider2D collider in colliders)
        {
            if (collider.CompareTag("Castle")|| collider.CompareTag("House"))
            {
                Debug.Log(collider.tag);
                BoxCollider2D boxcolliderSize = collider.gameObject.GetComponent<BoxCollider2D>();
                Transform colliderTransform = collider.transform;

                float width = boxcolliderSize.size.x;
                float height = boxcolliderSize.size.y;
                
                
                float yPos = colliderTransform.position.y;
                

                if (avoidingBool)
                    continue;
                if (transform.position.y > yPos)
                {
                    up = true;
                    down = false;
                }
                else if(transform.position.y < yPos)
                {
                    down = true;
                    up = false;
                }
                colliderWidth = width;
                colliderHeight = height;
                avoidingBool = true;
                avoidingSaveBool = avoidingBool; // 코루틴 실행 플래그 설정
                
            }
        }
    }

    IEnumerator AvoidingCoroutine()
    {
        if (up)
        {
            Vector2 targetPosition = transform.position = Vector2.MoveTowards(transform.position, new Vector2(transform.position.x, transform.position.y + colliderHeight), moveSpeed * Time.deltaTime);
            WakerFlip(targetPosition);
            while (Vector2.Distance(transform.position, targetPosition) > 0.1f)
            {
                WakerFlip(targetPosition);
                transform.position = Vector2.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
                yield return null;
            }
        }
        else if (down)
        {
            Vector2 targetPosition = transform.position = Vector2.MoveTowards(transform.position, new Vector2(transform.position.x, transform.position.y - colliderHeight), moveSpeed * Time.deltaTime);
            WakerFlip(targetPosition);
            while (Vector2.Distance(transform.position, targetPosition) > 0.1f)
            {
                WakerFlip(targetPosition);
                transform.position = Vector2.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
                yield return null;
            }
        }

        avoidingBool = false; // 이동 종료 후 avoidingBool을 false로 설정
        avoidingSaveBool = false; // 코루틴 실행 플래그 해제
        
    }
    public void WarriorAction()
{
    // 'tagName'에 해당하는 모든 오브젝트를 찾아 배열에 저장
    GameObject[] warriorActionGameObject = GameObject.FindGameObjectsWithTag(tagName);

    // 찾은 오브젝트가 있을 경우에만 아래 로직 수행
    if (warriorActionGameObject.Length > 0)
    {
        GameObject closestGoblin = null; // 가장 가까운 고블린을 저장할 변수
        float closestDistance = Mathf.Infinity; // 초기화된 가장 가까운 거리 변수

        // 찾은 오브젝트 중에서 가장 가까운 고블린을 탐색
        foreach (GameObject warriorActions in warriorActionGameObject)
        {
            // 현재 위치와 각 오브젝트 사이의 거리를 계산
            float distanceToTree = Vector2.Distance(transform.position, new Vector2(warriorActions.transform.position.x + stopPosX, warriorActions.transform.position.y + stopPosy));

            // 가장 가까운 오브젝트를 업데이트
            if (distanceToTree <= closestDistance)
            {
                // 가장 가까운 고블린을 저장
                closestGoblin = warriorActions;
                closestDistance = distanceToTree; // 가장 가까운 거리 갱신
            }
        }

        // 가장 가까운 고블린이 있을 경우
        if (closestGoblin != null)
        {
            reSearchTagGameObject.SetActive(false); // 재탐색 UI 비활성화

            // 아직 공격 상태가 아니면
            if (!warriorActionsBool)
            {
                // 공격 범위에 도달하지 않았을 때
                if (!attackRangeBool)
                {
                    warriorAnimator.SetBool("AttackBool", false); // 공격 애니메이션 중지
                    warriorAnimator.SetBool("RunBool", true); // 달리기 애니메이션 실행

                    // 고블린의 위치로 이동
                    Vector2 targetPosition = new Vector2(closestGoblin.transform.position.x + stopPosX, closestGoblin.transform.position.y + stopPosy);
                    transform.position = Vector2.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime); // 해당 위치로 이동
                    WakerFlip(targetPosition); // 방향 전환 함수 호출
                }
            }

            // 공격 범위 내에 고블린이 있을 경우
            if (closestDistance <= attackRange)
            {
                warriorActionsBool = true; // 공격 상태 활성화
                warriorAnimator.SetBool("RunBool", false); // 달리기 애니메이션 중지
                warriorAnimator.SetBool("AttackBool", true); // 공격 애니메이션 실행
            }
            else
            {
                // 공격 범위 밖이면 달리기 상태 유지
                warriorActionsBool = false;
                warriorAnimator.SetBool("RunBool", true); // 달리기 애니메이션 실행
            }
        }
        else
        {
            // 고블린이 없을 경우 재탐색을 위한 UI 활성화
            reSearchTagGameObject.SetActive(true);
            warriorActionsBool = false; // 공격 상태 해제
        }
    }
    else
    {
        // 고블린이 없을 때 재탐색을 수행하지 않고 혼자 있을 때의 처리
        warriorActionsBool = false; // 공격 상태 해제
        warriorAnimator.SetBool("AttackBool", false); // 공격 애니메이션 중지
        warriorAnimator.SetBool("RunBool", true); // 달리기 애니메이션 실행
        tagName = "Goblin Archer"; // 새로운 타겟 태그 설정

        // 재탐색이 필요하지 않을 때 집으로 돌아가는 로직 수행
        if (searchBool == false)
        {
            reSearchTagGameObject.SetActive(false); // 재탐색 UI 비활성화
            GoHouse(); // 집으로 돌아가는 함수 호출
        }
    }
}
    public void GoHouse()
    {
        Tower[] warriorhouses = FindObjectsOfType<Tower>();

        foreach (Tower warriorhouse in warriorhouses)
        {
            if (warriorhouse.name == houseName && !warriorActionsBool)
            {
                goHouseBool = true;
                warriorAnimator.SetBool("RunBool", true);
                
                Vector2 targetPosition = warriorhouse.transform.position;
                
                transform.position = Vector2.MoveTowards(transform.position, warriorhouse.transform.position, moveSpeed * Time.deltaTime);
                WakerFlip(targetPosition);
                if (Vector2.Distance(transform.position, targetPosition) < 1f)
                {
                    Debug.Log("집도착");
                    goHouseBool = false;
                    warriorAnimator.SetBool("RunBool", false);
                }
            }
        }
    }
    public void Attack()
    {
        attackGame.SetActive(true);
        knifeAudioSource.Play();
    }
    public void DeAttack()
    {
        attackGame.SetActive(false);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject == null)
        {
            searchBool = false;
        }
        else
        {
            if (collision.gameObject.CompareTag("Goblin Archer") || collision.gameObject.CompareTag("GoblinWarrior"))
            {
                searchBool = true;
            }
            else
            {
                searchBool = false;
            }
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("GoblinWarrior") || collision.gameObject.CompareTag("Goblin Archer") || collision.gameObject.CompareTag("Bomber Goblin") || collision.gameObject.CompareTag("Bomber Goblin"))
        {
            attackRangeBool = false;
        }
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("GoblinWarrior") || collision.gameObject.CompareTag("Goblin Archer") || collision.gameObject.CompareTag("Bomber Goblin"))
        {
            tagName = collision.gameObject.tag;
            attackRangeBool = true;
        }
        else
        {
            attackRangeBool = false;
        }
        if(collision.gameObject.layer == 11)
        {
            isGround = true;
        }
        else
        {
            isGround = false;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("AttackRangeG"))
        {
            if (collision.gameObject != gameObject)
            {
                
                hpImage.fillAmount -= 3.0f / warriorHp;
            }
        }
        if (collision.gameObject.CompareTag("Dynamite"))
        {
            if (collision.gameObject != gameObject)
            {
                 hpImage.fillAmount -= 10.0f / warriorHp;
            }
        }
        if (collision.gameObject.CompareTag("Explosions"))
        {
            if (collision.gameObject != gameObject)
            {
                 hpImage.fillAmount -= 80.0f / warriorHp;

            }
        }

        if (collision.gameObject == null)
        {
            searchBool = false;
        }
        else
        {
            if (collision.gameObject.CompareTag("Goblin Archer") || collision.gameObject.CompareTag("GoblinWarrior") || collision.gameObject.CompareTag("Bomber Goblin"))
            {
                searchBool = true;
                tagName = collision.gameObject.tag;
            }
            else
            {
                searchBool = false;
            }
        }

    }
}

