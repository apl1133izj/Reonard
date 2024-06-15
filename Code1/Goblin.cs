using UnityEngine;
using UnityEngine.UI;
public class Goblin : MonoBehaviour
{
    public float moveSpeed;//�÷��̾� ����Ʈ
    public Animator goblinAnimator;
    public float checkRadius = 1.0f; // Ȯ���� �ݰ�
    public LayerMask goblinLayer;
    public bool goblinActionsBool;//������ �ٸ� ������ �ִ��� 
    public bool treeHitBool;//������ ���� �ִ���
    public string tagName;//������ ã�� tag
    public string houseName;//������ ���� ���� �� ã�� �� �̸� 
    public bool goblinFound;//�� �� �Ÿ��� ã�Ҵ°�
    public bool goHouseBool;//������ ���ư��� ���ΰ�
    public bool mineBool; //���꿡 �����ߴ°�
    public bool mineWarkeEnd;
    private Vector2 previousPosition;
    SpriteRenderer spriteRenderer;
    public Image hpImage;

    public new BoxCollider2D collider2D;
    buildeMouseAndTouch buildeMouseAnd;
    float stopPosX;
    float stopPosy;
    public bool treeFlipBool;

    public bool BridgeStart;
    public bool BridgeEnd;
    public bool mapOut;
    public GameObject attackPrefab;
    public Transform attackPos;

    bool right;
    bool left;

    public float attackRange = 1f;
    public float goblinSpacing = 1f;

    public bool attackRangeBool;

    public GameObject reSearchTagGameObject;
    public bool searchBool;
    public bool bridgeFalse;
    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        collider2D = GetComponent<BoxCollider2D>();
        buildeMouseAnd = FindObjectOfType<buildeMouseAndTouch>();
        goblinAnimator = GetComponent<Animator>();
    }
    void Start()
    {
        previousPosition = transform.position;

    }
    void Update()
    {

        GoblinAction();

        GoblinFlip();

        HP();
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
            goblinAnimator.SetBool("DeadBool", true);
        }
    }

    public void Dead()
    {
        Destroy(gameObject);
    }
    void GoblinFlip()
    {
        // ���� ��ġ�� ���� ��ġ�� ���Ͽ� ������ �ٲ���� Ȯ��
        if (transform.position.x > previousPosition.x)
        {
            // ���������� �̵� ��
            transform.localScale = new Vector2(1, 1);
            right = true;
            left = false;
        }
        else if (transform.position.x < previousPosition.x)
        {
            // �������� �̵� ��
            transform.localScale = new Vector2(-1, 1);
            left = true;
            right = false;
        }

        // ���� ��ġ�� ���� ��ġ�� ������Ʈ
        previousPosition = transform.position;
    }

    public void GoblinAction()
    {
        // ��� ���� ������Ʈ�� ã��
        GameObject[] warkerActionGameObject = GameObject.FindGameObjectsWithTag(tagName);
        if (warkerActionGameObject.Length > 0)
        {
            GameObject closestHuman = null;
            float closestDistance = Mathf.Infinity;

            // 
            foreach (GameObject warkerActions in warkerActionGameObject)
            {
                float distanceToTree = Vector2.Distance(transform.position, new Vector2(warkerActions.transform.position.x + stopPosX, warkerActions.transform.position.y + stopPosy));
                //Debug.Log(distanceToTree);
                if (distanceToTree <= closestDistance)
                {
                    // 
                    closestHuman = warkerActions;
                    closestDistance = distanceToTree;
                }
            }

            if (closestHuman != null)
            {
                // 가장 가까운 인간에 대한 처리 수행
                if (!goblinActionsBool)
                {
                    reSearchTagGameObject.SetActive(false);
                    if (!attackRangeBool)
                    {
                        //다이너마이트 와 고블린 궁수의 tagName
                        attackPrefab.gameObject.GetComponent<Dynamite>().tagName = tagName;
                        goblinAnimator.SetBool("AttackBool", false);
                        goblinAnimator.SetBool("RunBool", true);
                        Vector2 targetPosition = new Vector2(closestHuman.transform.position.x + stopPosX, closestHuman.transform.position.y + stopPosy);
                        MoveToTargetPosition(targetPosition);
                    }
                }
                if (closestDistance <= attackRange)
                {
                    //"공격 하는중"
                    goblinActionsBool = true;
                    goblinAnimator.SetBool("RunBool", false);
                    goblinAnimator.SetBool("AttackBool", true);
                }
            }
            else
            {

                // 혼자 있을 때의 처리
                goblinActionsBool = false;
                goblinAnimator.SetBool("RunBool", true);
                //GoHouse(); // 집으로 돌아가는 로직 수행

            }
        }
        else
        {
            //모든 조건이 아닐 경우
            // 혼자 있을 때의 처리
            reSearchTagGameObject.SetActive(true);
            goblinActionsBool = false;
            goblinAnimator.SetBool("AttackBool", false);
            goblinAnimator.SetBool("RunBool", true);
            if (!searchBool)
            {
                tagName = "Castle";
            }
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
            if (collider.CompareTag("Goblin Archer") && collider.gameObject != gameObject)
            {

                Vector2 direction = (targetPosition - (Vector2)transform.position).normalized;
                newPosition += direction * goblinSpacing;
            }
        }
        return newPosition;
    }
/*    public void GoHouse()
    {
        EnemyHouseAI[] enemyHouseAI = FindObjectsOfType<EnemyHouseAI>();

        foreach (EnemyHouseAI enemyHouseAIse in enemyHouseAI)
        {
            if (enemyHouseAIse.name == houseName && !goblinActionsBool)
            {
                goHouseBool = true;
                goblinAnimator.SetBool("RunBool", true);
                transform.position = Vector2.MoveTowards(transform.position, enemyHouseAIse.transform.position, moveSpeed * Time.deltaTime);
                if (enemyHouseAIse.transform.position == transform.position)
                {
                    Destroy(gameObject);
                    goHouseBool = false;
                    goblinAnimator.SetBool("RunBool", false);
                }
            }
        }
    }*/
    public void Attack()//�ʾ�
    {

        GameObject ThrowPrefab = Instantiate(attackPrefab, attackPos.position, Quaternion.identity);

        // Rigidbody2D throwPrefabRigidbody = ThrowPrefab.GetComponent<Rigidbody2D>();
        if (right)
        {

            //throwPrefabRigidbody.velocity = Vector2.right * 1.3f;
        }
        else if (left)
        {
            //throwPrefabRigidbody.velocity = Vector2.left * 1.3f;
        }
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject != null)
        {
            if (collision.gameObject.CompareTag("Warrior"))
            {
                attackRangeBool = true;
            }
            attackRangeBool = false;
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Warrior"))
        {
            attackRangeBool = false;
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {

        /*  if (collision.gameObject.CompareTag("MapOutRight"))
          {
              if (tagName == "Warker")
              {
                  mapOut = true;
                  stopPosX = 2f;
                  stopPosy = 4f;
              }
          }
          if (collision.gameObject.CompareTag("MapintLeft"))
          {
              if (tagName == "Warker")
              {
                  mapOut = false;
              }
          }*/


    }
    private void OnTriggerStay2D(Collider2D collision)
    {
         if (collision.gameObject == null)
        {
            searchBool = false;
        }
        else
        {
            if (collision.gameObject.CompareTag("Warrior") || collision.gameObject.CompareTag("Archer") || collision.gameObject.CompareTag("Castle"))
            {
                //Debug.Log("재검색 성공");
                searchBool = true;
                tagName = collision.gameObject.tag;
                Debug.Log(collision.gameObject.tag);
            }
            else
            {
                searchBool = false;
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!bridgeFalse)
        {
            if (collision.gameObject.CompareTag("Bridge"))
            {
                tagName = "BridgeEnd";
            }
            if (collision.gameObject.CompareTag("BridgeEnd"))
            {
                reSearchTagGameObject.SetActive(true);
                tagName = "Warrior";
                //searchBool = true;
                bridgeFalse = true;
                attackRange = 4;
            }
        }
        if (collision.gameObject.CompareTag("AttackRangeW"))
        {
            Training training = FindObjectOfType<Training>();
            hpImage.fillAmount -= training.enhance[2][training.level[2]] / 80.0f;
        }
        if (collision.gameObject.CompareTag("Arrow"))
        {
           // Debug.Log("고블린 궁수맞음");
            Debug.Log(hpImage.fillAmount -= collision.gameObject.GetComponent<Arrow>().archerDamage / 200.0f);
            hpImage.fillAmount -= (int)collision.gameObject.GetComponent<Arrow>().archerDamage  / (int)200.0f;
        }

       

    }
}
