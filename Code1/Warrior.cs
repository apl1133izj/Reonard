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
        // 모든 나무 오브젝트를 찾기
        GameObject[] warkerActionGameObject = GameObject.FindGameObjectsWithTag(tagName);
        if (warkerActionGameObject.Length > 0)
        {
            GameObject closestGoblin = null;
            float closestDistance = Mathf.Infinity;
            // 모든 나무에 대해 거리를 계산하고 가장 가까운 나무 찾기
            foreach (GameObject warkerActions in warkerActionGameObject)
            {
                float distanceToTree = Vector2.Distance(transform.position, new Vector2(warkerActions.transform.position.x + stopPosX, warkerActions.transform.position.y + stopPosy));
                //Debug.Log(distanceToTree);
                if (distanceToTree <= closestDistance)
                {
          
                    // 현재 나무가 더 가까우면 업데이트
                    closestGoblin = warkerActions;
                    closestDistance = distanceToTree;
                }
            }

            if (closestGoblin != null)
            {
                reSearchTagGameObject.SetActive(false);
                // 가장 가까운 인간에 대한 처리 수행
                if (!warriorActionsBool)
                {
                    if (!attackRangeBool)
                    {
                        warriorAnimator.SetBool("AttackBool", false);
                        warriorAnimator.SetBool("RunBool", true);
                        
                        Vector2 targetPosition = new Vector2(closestGoblin.transform.position.x + stopPosX, closestGoblin.transform.position.y + stopPosy);
                        transform.position = Vector2.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
                        WakerFlip(targetPosition);

                    }
                }
                if (closestDistance <= attackRange)
                {
                    //"공격 하는중"
                    warriorActionsBool = true;
                    warriorAnimator.SetBool("RunBool", false);
                    warriorAnimator.SetBool("AttackBool", true);
                }
                else
                {
                    warriorActionsBool = false;
                    warriorAnimator.SetBool("RunBool", true);
                }
            }
            else
            {

                reSearchTagGameObject.SetActive(true);
                // 혼자 있을 때의 처리
                warriorActionsBool = false;

            }
        }
        else
        {
            //reSearchTagGameObject.SetActive(true);
            // 혼자 있을 때의 처리
            warriorActionsBool = false;
            warriorAnimator.SetBool("AttackBool", false);
            warriorAnimator.SetBool("RunBool", true);
            /*원래 타겟이었던 게임오브젝트가 없어질경우 재탐색 하지만 재탐색을
            해도 고블린 게임오브젝트 가 없을경우 GoHouse실행*/
            tagName = "Goblin Archer";
            if (searchBool == false)
            {
                reSearchTagGameObject.SetActive(false);
                GoHouse(); // 집으로 돌아가는 로직 수행
            }
            //모든 조건이 아닐 경우
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

