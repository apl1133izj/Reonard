using UnityEngine;
using UnityEngine.UI;
using System.Collections;
public class Archer : MonoBehaviour
{
    Training training;

    public Ground ground;
    public Animator archerAnimator;  
    public LayerMask archerLayer; 
    public GameObject attackPrefab;
    public Transform attackPos;
    public Image hpImage;
    public GameObject reSearchTagGameObject;

    public float moveSpeed;//플레이어 스피트
    public float checkRadius = 1.0f; // 확인할 반경
    public bool goblinActionsBool;//나무에 다른 고블린이 있는지 
    public bool treeHitBool;//나무를 베고 있는지
    public string tagName;//고블린이 찾는 tag
    public string houseName;//고블린이 일을 다한 후 찾는 집 이름 
    public bool goblinFound;//일 할 거리를 찾았는가
    public bool goHouseBool;//집으로 돌아가는 중인가
    private Vector2 previousPosition;

    float stopPosX;
    float stopPosy;
    public bool BridgeStart;
    public bool BridgeEnd;
    public bool mapOut;
   
    bool right;
    bool left;
    public bool isAttackRange;

    public float attackRange = 4f;
    public float goblinSpacing = 1f;

    public bool attackRangeBool;

    
    public bool searchBool;
    public bool groundBool;
    public bool bridgeFalse;
    public bool archerActionsBool;

    //부딪힌 콜라이더 크기 
    float colliderWidth;
    float colliderHeight;

    public bool avoidingBool;//다른 건물과 부딪치는지 확인
    public bool avoidingSaveBool;//다른 건물과 부딪치는지 확인

    public bool up;
    public bool down;

    public bool isGround;
    //효과음
    public AudioSource bowSound;
    private void Awake()
    {
        ground = FindObjectOfType<Ground>();
        archerAnimator = GetComponent<Animator>();
        bowSound = GetComponent<AudioSource>();
    }
    void Start()
    {
        previousPosition = transform.position;
    }
    void Update()
    {   
        if (ground.goblinInvasionbool)
        {
            ArcherAction();
        }
        else
        {
            GoHouse();
        }
        ArcherFlip();


        HP();

        if (ground.goblinInvasionbool && !avoidingSaveBool)
        {
            ArcherAction();
        }
        if (avoidingBool)
        {
            GoHouse();
        }
        Avoiding();


        if (avoidingSaveBool)
        {
            StartCoroutine(AvoidingCoroutine());
        }
    }
    void WakerFlip(Vector2 targetPosition)
    {

        // 
        if (transform.position.x < targetPosition.x)
        {

            transform.localScale = new Vector2(1, 1);
        }
        else if (transform.position.x > targetPosition.x)
        {
            transform.localScale = new Vector2(-1, 1);
        }

    }
    void ArcherFlip()
    {
        // 현재 위치와 이전 위치를 비교하여 방향이 바뀌는지 확인
        if (transform.position.x > previousPosition.x)
        {
            // 오른쪽으로 이동 중
            transform.localScale = new Vector2(1, 1);
            right = true;
            left = false;
        }
        else if (transform.position.x < previousPosition.x)
        {
            // 왼쪽으로 이동 중
            transform.localScale = new Vector2(-1, 1);
            right = false;
            left = true;
        }

        // 현재 위치를 이전 위치로 업데이트
        previousPosition = transform.position;
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
            Destroy(gameObject);
            // warriorAnimator.SetBool("DieBool", true);
        }
    }
    void Avoiding()
    {
        //콜라이더의 크기를 확인해서 피하는 코드
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, 1);
        foreach (Collider2D collider in colliders)
        {
            if (collider.CompareTag("Castle") || collider.CompareTag("House"))
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
                else if (transform.position.y < yPos)
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
    public void ArcherAction()
    {
        // 모든 나무 오브젝트를 찾기
        GameObject[] warkerActionGameObject = GameObject.FindGameObjectsWithTag(tagName);
        if (warkerActionGameObject.Length > 0 )
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
                    attackPrefab.gameObject.GetComponent<Arrow>().tagName = tagName;
                    // 현재 나무가 더 가까우면 업데이트
                    closestGoblin = warkerActions;
                    closestDistance = distanceToTree;
                }
            }

            if (closestGoblin != null)
            {

                reSearchTagGameObject.SetActive(false);
                // 가장 가까운 인간에 대한 처리 수행
                if (!archerActionsBool)
                {

                    if (!attackRangeBool)
                    {

                        archerAnimator.SetBool("AttackBool", false);
                        archerAnimator.SetBool("RunBool", true);
                        Vector2 targetPosition = new Vector2(closestGoblin.transform.position.x + stopPosX, closestGoblin.transform.position.y + stopPosy);
                        MoveToTargetPosition(targetPosition);
                    }
                }
                if (closestDistance <= attackRange)
                {

                    //"공격 하는중"
                    archerActionsBool = true;
                    archerAnimator.SetBool("RunBool", false);

                    
                    archerAnimator.SetBool("AttackBool", true);

                }
                else
                {

                    archerActionsBool = false;
                    archerAnimator.SetBool("RunBool", true);
                }
            }
            else
            {
                reSearchTagGameObject.SetActive(true);
                // 혼자 있을 때의 처리
                archerActionsBool = false;

            }
        }
        else
        {
            //reSearchTagGameObject.SetActive(true);
            // 혼자 있을 때의 처리
            archerActionsBool = false;
            archerAnimator.SetBool("AttackBool", false);
            archerAnimator.SetBool("RunBool", true);
            tagName = "Goblin Archer";
            /*원래 타겟이었던 게임오브젝트가 없어질경우 재탐색 하지만 재탐색을
            해도 고블린 게임오브젝트 가 없을경우 GoHouse실행*/

            if (!searchBool)
            {
                reSearchTagGameObject.SetActive(false);
                GoHouse(); // 집으로 돌아가는 로직 수행
                goblinSpacing = 1.5f;
            }
            //모든 조건이 아닐 경우
        }
    }

    private void MoveToTargetPosition(Vector2 targetPosition)
    {
        Vector2 newPosition = FindNonOverlappingPosition(targetPosition);
        transform.position = Vector2.MoveTowards(transform.position, newPosition, moveSpeed * Time.deltaTime);
    }

    private Vector2 FindNonOverlappingPosition(Vector2 targetPosition)
    {
        Vector2 newPosition = targetPosition;
        foreach (Collider2D collider in Physics2D.OverlapCircleAll(targetPosition, goblinSpacing))
        {
            if (collider.CompareTag("Warrior") && collider.gameObject != gameObject)
            {
                Vector2 direction = (targetPosition - (Vector2)transform.position).normalized;
                newPosition += direction * goblinSpacing;
            }
        }

        return newPosition;
    }
    public void GoHouse()
    {
        Tower[] warriorhouses = FindObjectsOfType<Tower>();

        foreach (Tower warriorhouse in warriorhouses)
        {
            if (warriorhouse.name == houseName && !archerActionsBool)
            {
                goHouseBool = true;
                archerAnimator.SetBool("RunBool", true);
                Vector2 targetPosition = warriorhouse.transform.position;
                targetPosition = FindNonOverlappingPosition(targetPosition); // 간격 조정
                transform.position = Vector2.MoveTowards(transform.position, warriorhouse.transform.position, moveSpeed * Time.deltaTime);
                if (Vector2.Distance(transform.position, targetPosition) < 2f)
                {
                    //Destroy(gameObject);
                    goHouseBool = false;
                    archerAnimator.SetBool("RunBool", false);
                }
            }
        }
    }

    public void Attack()//않씀
    {

        // Rigidbody2D throwPrefabRigidbody = ThrowPrefab.GetComponent<Rigidbody2D>();
        if (right)
        {
            training = FindObjectOfType<Training>();
            GameObject ThrowPrefab = Instantiate(attackPrefab, attackPos.position, Quaternion.identity);
            Arrow arrow = ThrowPrefab.GetComponent<Arrow>();
            arrow.archerDamage = training.enhance[1][training.level[1]];
            ThrowPrefab.transform.rotation = Quaternion.Euler(0f, 0f, 0f);
            bowSound.Play();
        }
        else if (left)
        {
            GameObject ThrowPrefab = Instantiate(attackPrefab, attackPos.position, Quaternion.identity);
            Arrow arrow = ThrowPrefab.GetComponent<Arrow>();
            arrow.archerDamage = training.enhance[1][training.level[1]];
            ThrowPrefab.transform.rotation = Quaternion.Euler(0f, 0f, 180f);
            bowSound.Play();
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject == null)
        {
            searchBool = false;
        }
        else
        {
            if (collision.gameObject.CompareTag("Goblin Archer") || collision.gameObject.CompareTag("GoblinWarrior") || collision.gameObject.CompareTag("Bomber Goblin"))
            {
                tagName = gameObject.gameObject.tag;
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
        if (collision.gameObject.CompareTag("GoblinWarrior") || collision.gameObject.CompareTag("Goblin Archer") || collision.gameObject.CompareTag("Bomber Goblin"))
        {
            attackRangeBool = false;
        }
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject != null)
        {
            if (collision.gameObject.CompareTag("GoblinWarrior") || collision.gameObject.CompareTag("Goblin Archer") || collision.gameObject.CompareTag("Bomber Goblin"))
            {
                tagName = collision.gameObject.tag;
                attackPrefab.gameObject.GetComponent<Arrow>().tagName = tagName;
                attackRangeBool = true;
                
            }
            attackRangeBool = false;
        }
        if (collision.gameObject.CompareTag("BuildGread"))
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
            hpImage.fillAmount -= (int)3.0f / (int)200.0f;
        }
        if (collision.gameObject.CompareTag("Dynamite"))
        {
            hpImage.fillAmount -= (int)10.0f / (int)200.0f;
        }
        if (collision.gameObject.CompareTag("Explosions"))
        {
            hpImage.fillAmount -= (int)80.0f / (int)200.0f;
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
                
            }
            else
            {
                searchBool = false;
            }
        }

    }
}
