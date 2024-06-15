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

    public float moveSpeed;//�÷��̾� ����Ʈ
    public float checkRadius = 1.0f; // Ȯ���� �ݰ�
    public bool goblinActionsBool;//������ �ٸ� ����� �ִ��� 
    public bool treeHitBool;//������ ���� �ִ���
    public string tagName;//����� ã�� tag
    public string houseName;//����� ���� ���� �� ã�� �� �̸� 
    public bool goblinFound;//�� �� �Ÿ��� ã�Ҵ°�
    public bool goHouseBool;//������ ���ư��� ���ΰ�
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

    //�ε��� �ݶ��̴� ũ�� 
    float colliderWidth;
    float colliderHeight;

    public bool avoidingBool;//�ٸ� �ǹ��� �ε�ġ���� Ȯ��
    public bool avoidingSaveBool;//�ٸ� �ǹ��� �ε�ġ���� Ȯ��

    public bool up;
    public bool down;

    public bool isGround;
    //ȿ����
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
            right = false;
            left = true;
        }

        // ���� ��ġ�� ���� ��ġ�� ������Ʈ
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
        //�ݶ��̴��� ũ�⸦ Ȯ���ؼ� ���ϴ� �ڵ�
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
                avoidingSaveBool = avoidingBool; // �ڷ�ƾ ���� �÷��� ����
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

        avoidingBool = false; // �̵� ���� �� avoidingBool�� false�� ����
        avoidingSaveBool = false; // �ڷ�ƾ ���� �÷��� ����
    }
    public void ArcherAction()
    {
        // ��� ���� ������Ʈ�� ã��
        GameObject[] warkerActionGameObject = GameObject.FindGameObjectsWithTag(tagName);
        if (warkerActionGameObject.Length > 0 )
        {
            GameObject closestGoblin = null;
            float closestDistance = Mathf.Infinity;

            // ��� ������ ���� �Ÿ��� ����ϰ� ���� ����� ���� ã��
            foreach (GameObject warkerActions in warkerActionGameObject)
            {
                float distanceToTree = Vector2.Distance(transform.position, new Vector2(warkerActions.transform.position.x + stopPosX, warkerActions.transform.position.y + stopPosy));
                //Debug.Log(distanceToTree);
                if (distanceToTree <= closestDistance)
                {
                    attackPrefab.gameObject.GetComponent<Arrow>().tagName = tagName;
                    // ���� ������ �� ������ ������Ʈ
                    closestGoblin = warkerActions;
                    closestDistance = distanceToTree;
                }
            }

            if (closestGoblin != null)
            {

                reSearchTagGameObject.SetActive(false);
                // ���� ����� �ΰ��� ���� ó�� ����
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

                    //"���� �ϴ���"
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
                // ȥ�� ���� ���� ó��
                archerActionsBool = false;

            }
        }
        else
        {
            //reSearchTagGameObject.SetActive(true);
            // ȥ�� ���� ���� ó��
            archerActionsBool = false;
            archerAnimator.SetBool("AttackBool", false);
            archerAnimator.SetBool("RunBool", true);
            tagName = "Goblin Archer";
            /*���� Ÿ���̾��� ���ӿ�����Ʈ�� ��������� ��Ž�� ������ ��Ž����
            �ص� ��� ���ӿ�����Ʈ �� ������� GoHouse����*/

            if (!searchBool)
            {
                reSearchTagGameObject.SetActive(false);
                GoHouse(); // ������ ���ư��� ���� ����
                goblinSpacing = 1.5f;
            }
            //��� ������ �ƴ� ���
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
                targetPosition = FindNonOverlappingPosition(targetPosition); // ���� ����
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

    public void Attack()//�ʾ�
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
