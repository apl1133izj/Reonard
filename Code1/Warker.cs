using UnityEngine;
using UnityEngine.UI;
public class Warker : MonoBehaviour
{

    public float moveSpeed;//
    public Animator warkerAnimator;
    //public int cuttingInt;//
    public float checkRadius = 1.0f; // 
    public LayerMask warkerLayer;
    public bool warkerActionsBool;// 
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
    //bool closestDistanceBool;

    SpriteRenderer spriteRenderer;
    public Image hpImage;

    public  BoxCollider2D collider2D;
    buildeMouseAndTouch buildeMouseAnd;
    float stopPosX;
    float stopPosy;
    public bool treeFlipBool;

    public AudioSource[] wakerAudioSources;
    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        collider2D = GetComponent<BoxCollider2D>();
        buildeMouseAnd = FindObjectOfType<buildeMouseAndTouch>();
        warkerAnimator = GetComponent<Animator>();
    }
    void Start()
    {
        // 시작 시 설정
        if (tagName == "Mine")
        {
            collider2D.enabled = true;
            spriteRenderer.sortingOrder = 5;
        }
        previousPosition = transform.position;
    }
    void Update()
    {
        // 주기적인 동작 실행
        warkerAction(); // 작업자 동작 관련 함수 호출
        WakerFlip(); // 작업자의 방향 전환 관련 함수 호출
        StopPos(); // 정지 위치 설정 함수 호출
        HP(); // HP 관리 함수 호출
    }
    void StopPos()
    {
        // 작업자가 특정 오브젝트에 도달했을 때의 정지 위치 설정
        if (tagName == "Tree")
        {
            stopPosX = 0.8f;
            stopPosy = -1f;
        }
        else if (tagName == "Mine")
        {
            stopPosX = 0f;
            stopPosy = 0f;
        }
        else
        {
            stopPosX = 1f;
            stopPosy = -0.8f;
        }
    }
    public void HP()
    {
        // HP 표시 설정
        if (transform.localScale.x == -1)
        {
            hpImage.rectTransform.localScale = new Vector2(0.022f, 0.035f);
        }
        else
        {
            hpImage.rectTransform.localScale = new Vector2(-0.022f, 0.035f);
        }
        if (hpImage.fillAmount == 0)
        {
            // HP가 0이 되면 작업자를 비활성화
            gameObject.tag = "Untagged";
            gameObject.layer = 0;
            warkerAnimator.SetBool("DieBool", true);
        }
    }
    void WakerFlip()
    {
        // 작업자의 방향 전환
        if (transform.position.x > previousPosition.x)
        {
            transform.localScale = new Vector2(1, 1);
        }
        else if (transform.position.x < previousPosition.x)
        {
            transform.localScale = new Vector2(-1, 1);
        }
        previousPosition = transform.position;
    }

    public void warkerAction()
    {
        // 작업자의 동작 관리
        GameObject[] warkerActionGameObject = GameObject.FindGameObjectsWithTag(tagName);
        if (warkerActionGameObject.Length > 0 && !mineWarkeEnd && warkTime <= 150)
        {
            GameObject closestTree = null;
            float closestDistance = Mathf.Infinity;

            foreach (GameObject warkerActions in warkerActionGameObject)
            {
                // 작업 대상까지의 거리 계산
                float distanceToTree = Vector2.Distance(transform.position, new Vector2(warkerActions.transform.position.x + stopPosX, warkerActions.transform.position.y + stopPosy));

                if (distanceToTree < closestDistance)
                {
                    // 작업 대상과의 거리가 가장 가까운 경우
                    Collider2D[] colliders = Physics2D.OverlapCircleAll(warkerActions.transform.position, checkRadius, warkerLayer);
                    bool warkerFound = false;

                    foreach (Collider2D collider in colliders)
                    {
                        // 작업자간의 충돌 확인
                        if (collider.CompareTag("Warker") && collider.gameObject != gameObject)
                        {
                            warkerFound = true;
                            break;
                        }
                    }

                    if (warkerFound)
                    {
                        // 작업자가 이미 작업을 진행중인 작업자를 찾았을 때
                        continue;
                    }

                    closestTree = warkerActions;
                    closestDistance = distanceToTree;
                }
            }

            if (closestTree != null)
            {
                // 작업 대상이 있는 경우
                if (!warkerActionsBool)
                {
                    // 작업 동작 중이 아닌 경우
                    if (tagName == "Tree")
                        warkerAnimator.SetBool("CuttingBool", false);
                    if (tagName == "BuildHouse" || tagName == "BuildMine" || tagName == "BuildTower")
                        warkerAnimator.SetBool("BuildBool", false);
                    warkerAnimator.SetBool("RunBool", true);
                    Vector2 targetPosition = new Vector2(closestTree.transform.position.x + stopPosX, closestTree.transform.position.y + stopPosy);
                    transform.position = Vector2.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
                }
                else
                {
                    // 작업 동작 중인 경우
                    warkerAnimator.SetBool("RunBool", false);
                }

                if (closestDistance == 0f)
                {
                    // 작업자가 작업 대상에 도달한 경우
                    if (tagName == "Mine")
                    {
                        mineBool = true;
                    }

                    warkerActionsBool = true;
                    warkerAnimator.SetBool("RunBool", false);
                    warkTime += Time.deltaTime;
                    if (tagName == "Tree")
                        warkerAnimator.SetBool("CuttingBool", true);
                    transform.localScale = new Vector2(-1, 1);
                    if (tagName == "BuildHouse" || tagName == "BuildMine" || tagName == "BuildTower")
                        warkerAnimator.SetBool("BuildBool", true);
                    collider2D.enabled = true;
                }
                else
                {
                    // 작업자가 작업 대상에 도달하지 않은 경우
                    warkerActionsBool = false;

                    warkerAnimator.SetBool("RunBool", true);
                    if (tagName == "Tree")
                        warkerAnimator.SetBool("CuttingBool", false);
                    if (tagName == "BuildHouse" || tagName == "BuildMine" || tagName == "BuildTower")
                        warkerAnimator.SetBool("BuildBool", false);
                }
            }
            else
            {
                // 작업 대상이 없는 경우
                warkerActionsBool = false;

                // 집으로 이동
                GoHouse();

                warkerAnimator.SetBool("RunBool", true);
                if (tagName == "Tree")
                    warkerAnimator.SetBool("CuttingBool", false);
                if (tagName == "BuildHouse" || tagName == "BuildMine" || tagName == "BuildTower")
                    warkerAnimator.SetBool("BuildBool", false);
            }
        }
        else
        {
            // 작업 대상이 없거나 작업을 마친 경우
            warkerActionsBool = false;
            GoHouse();

            if (tagName == "Tree")
                warkerAnimator.SetBool("CuttingBool", false);

            if (tagName == "BuildHouse" || tagName == "BuildMine" || tagName == "BuildTower")
                warkerAnimator.SetBool("BuildBool", false);
            collider2D.enabled = false;
            if (!goHouseBool)
            {
                warkerAnimator.SetBool("RunBool", false);
            }
        }
    }
    public void GoHouse()
    {
        // 집으로 이동
        Warkerhouse[] warkerhouses = FindObjectsOfType<Warkerhouse>();

        foreach (Warkerhouse warkerhouse in warkerhouses)
        {
            if (warkerhouse.name == houseName && !warkerActionsBool)
            {
                goHouseBool = true;
                warkerAnimator.SetBool("RunBool", true);
                transform.position = Vector2.MoveTowards(transform.position, warkerhouse.transform.position, moveSpeed * Time.deltaTime);
                if (warkerhouse.transform.position == transform.position)
                {

                    warkerhouse.reWarkerNumber += 1;
                    warkerhouse.warkerMaxNumber += 1;
                    warkerhouse.warkerMaxNumberUIText += 1;
                    Destroy(gameObject);
                    goHouseBool = false;
                    warkerAnimator.SetBool("RunBool", false);
                }
            }
        }
    }

    public void CuttingCount()
    {
        // 나무를 베는 행동 트리거
        wakerAudioSources[0].Play();
        collider2D.enabled = true;
        treeHitBool = true;
    }
    public void buildEffectStart()
    {
        // 건물 건설 시작 시 효과
        wakerAudioSources[0].Play();
        //buildEffectPrefab.SetActive(true);
    }
    public void buildEffectEnd()
    {
        // 건물 건설 종료 시 효과
        //buildEffectPrefab.SetActive(false);
    }

    public void TreeHitfalse()
    {
        // 나무를 베는 동작 종료
        collider2D.enabled = false;
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        // 작업자가 충돌 대상에서 벗어난 경우
        if (collision.gameObject.CompareTag("Tree"))
        {
            treeFlipBool = false;
            //closestDistanceBool = false;
        }
    }
}
