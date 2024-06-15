using UnityEngine;
using UnityEngine.UI;
public class GoblineWarrior : MonoBehaviour
{
    Training training;

    public float moveSpeed;//�÷��̾� ����Ʈ
    public Animator warriorAnimator;
    //public int cuttingInt;//������ ��� ���� ī��Ʈ
    public float checkRadius = 1.0f; // Ȯ���� �ݰ�
    public LayerMask warriorLayer;
    public bool warriorActionsBool;//������ �ٸ� ��Ŀ�� �ִ��� 
    public bool treeHitBool;//
    public float warkTime;
    public string tagName;//
    public string houseName;//
    public bool warkerFound;//
    public bool goHouseBool;//
    public bool mineBool; //
    public bool mineWarkeEnd;
    private Vector2 previousPosition;
    public GameObject buildEffectPrefab;
    public Image hpImage;

    public GameObject attackGame;
    float stopPosX;
    float stopPosy;
    public bool treeFlipBool;

    public float attackRange = 1f;
    public float goblinSpacing = 1f;

    public bool attackRangeBool;

    public GameObject reSearchTagGameObject;
    public bool searchBool;
    public bool bridgeFalse;

    public AudioSource flameAudioSource;
    private void Awake()
    {
        warriorAnimator = GetComponent<Animator>();
    }
    void Start()
    {
        previousPosition = transform.position;
    }
    void Update()
    {
        if (searchBool)
        {
            reSearchTagGameObject.SetActive(true);
        }
        else
        {
            reSearchTagGameObject.SetActive(false);
        }
        WarriorAction();
        WakerFlip();
        HP();
        StopPos();
    }

    void StopPos()
    {
        if (tagName == "Castle")
        {
            stopPosX = 2;
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
    void WakerFlip()
    {
        // 
        if (transform.position.x > previousPosition.x)
        {

            transform.localScale = new Vector2(1, 1);
        }
        else if (transform.position.x < previousPosition.x)
        {

            transform.localScale = new Vector2(-1, 1);
        }
        // 
        previousPosition = transform.position;
    }

    public void WarriorAction()
    {
        // 모든 나무 오브젝트를 찾기
        GameObject[] warkerActionGameObject = GameObject.FindGameObjectsWithTag(tagName);
        if (warkerActionGameObject.Length > 0)
        {
            GameObject closestHuman = null;
            float closestDistance = Mathf.Infinity;

            // 모든 나무에 대해 거리를 계산하고 가장 가까운 나무 찾기
            foreach (GameObject warkerActions in warkerActionGameObject)
            {
                float distanceToTree = Vector2.Distance(transform.position, new Vector2(warkerActions.transform.position.x + stopPosX, warkerActions.transform.position.y + stopPosy));
                //Debug.Log(distanceToTree);
                if (distanceToTree <= closestDistance)
                {
                    // 가장 가까운 나무 주변에 다른 "Warker" 오브젝트가 있는지 확인

                    // 현재 나무가 더 가까우면 업데이트
                    closestHuman = warkerActions;
                    closestDistance = distanceToTree;
                }
            }

            if (closestHuman != null)
            {
                reSearchTagGameObject.SetActive(false);
                // 가장 가까운 인간에 대한 처리 수행
                if (!warriorActionsBool)
                {

                    if (!attackRangeBool)
                    {
                        warriorAnimator.SetBool("AttackBool", false);
                        warriorAnimator.SetBool("RunBool", true);
                        Vector2 targetPosition = new Vector2(closestHuman.transform.position.x + stopPosX, closestHuman.transform.position.y + stopPosy);
                        MoveToTargetPosition(targetPosition);
                    }
                }
                if (closestDistance < attackRange)
                {
                    //"공격 하는중"
                    if (tagName == "Bridge" || tagName == "BridgeEnd")
                    {
                        warriorAnimator.SetBool("AttackBool", false);
                    }
                    else
                    {
                        warriorActionsBool = true;
                        warriorAnimator.SetBool("RunBool", false);
                        warriorAnimator.SetBool("AttackBool", true);
                    }

                }
                else
                {
                    warriorActionsBool = false;
                    warriorAnimator.SetBool("RunBool", true);
                }
            }
            else
            {
                // 혼자 있을 때의 처리
                reSearchTagGameObject.SetActive(true);
                warriorActionsBool = false;
                warriorAnimator.SetBool("RunBool", true);
                GoHouse(); // 집으로 돌아가는 로직 수행

            }
        }
        else
        {
            //모든 조건이 아닐 경우
            // 혼자 있을 때의 처리
            reSearchTagGameObject.SetActive(true);
            warriorActionsBool = false;
            warriorAnimator.SetBool("AttackBool", false);
            warriorAnimator.SetBool("RunBool", true);
            if (!searchBool)
            {
                tagName = "Castle"; // 집으로 돌아가는 로직 수행
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
            if (collider.CompareTag("GoblinWarrior") && collider.gameObject != gameObject)
            {

                Vector2 direction = (targetPosition - (Vector2)transform.position).normalized;
                newPosition += direction * goblinSpacing;
            }
        }

        return newPosition;
    }
    public void GoHouse()
    {
        GoblinTower[] goblinTowerse = FindObjectsOfType<GoblinTower>();

        foreach (GoblinTower warkerhouse in goblinTowerse)
        {
            if (warkerhouse.name == houseName && !warriorActionsBool)
            {
                goHouseBool = true;
                warriorAnimator.SetBool("RunBool", true);
                transform.position = Vector2.MoveTowards(transform.position, warkerhouse.transform.position, moveSpeed * Time.deltaTime);
                if (warkerhouse.transform.position == transform.position)
                {
                    goHouseBool = false;
                    warriorAnimator.SetBool("RunBool", false);
                    Destroy(gameObject);
                }
            }
        }
    }

    public void Attack()
    {
        attackGame.SetActive(true);
        flameAudioSource.Play();
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
            if (collision.gameObject.CompareTag("Castle"))
            {
                reSearchTagGameObject.SetActive(true);
                searchBool = true;
                collision.gameObject.tag = tagName;
            }
            else
            {
                searchBool = false;
            }
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
                tagName = "Warrior";
                //searchBool = true;
                attackRange = 0.7f;
                bridgeFalse = true;
            }
        }
        else
        {
            searchBool = false;
        }
        if (collision.gameObject.CompareTag("AttackRangeW"))
        {
            if (collision.gameObject != gameObject)
            {
                Training training = FindObjectOfType<Training>();
                hpImage.fillAmount -= training.enhance[2][training.level[2]] / 200.0f;
            }
        }
        if (collision.gameObject.CompareTag("Arrow"))
        {
            if (collision.gameObject != gameObject)
            {
                Debug.Log("고블린 전사맞음");
                Training training = FindObjectOfType<Training>();
                Debug.Log(hpImage.fillAmount -= training.enhance[1][training.level[1]] / 200.0f);
                hpImage.fillAmount -= training.enhance[1][training.level[1]] / 200.0f;
            }
        }


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
                collision.gameObject.tag = tagName;
            }
            else
            {
                searchBool = false;
            }
        }
    }
}
