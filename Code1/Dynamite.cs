using System.Collections;
using UnityEngine;
public class Dynamite : MonoBehaviour
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

    BoxCollider2D boxCollider2D;
    private void Awake()
    {
        boxCollider2D = GetComponent<BoxCollider2D>();
        animator = GetComponent<Animator>();

    }
    private void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();

    }
    void Update()
    {
        //DynamiteAction();
        StartCoroutine(DynamiteActionCo());

    }
    IEnumerator DynamiteActionCo()
    {
        DynamiteAction();
        yield return new WaitForSeconds(1);
        Destroy(gameObject);
    }
    public void DynamiteAction()
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
                }
                if (closestDistance <= 0f)
                {
                    //"나무 베는중"
                    goblinActionsBool = true;
                }
            }
        }
        else
        {

            goblinActionsBool = false;
        }
    }


    public void ExplosionEffect()
    {
        Collider2D warkercollider2D = Physics2D.OverlapBox(transform.position, Vector2.one, 0, wakerlayerMask);
        if (warkercollider2D != null)
        {
            Warker warker = warkercollider2D.GetComponent<Warker>();

            if (warker != null)
            {
                if (warkercollider2D.gameObject.CompareTag("Warker"))
                {
                    warker.hpImage.fillAmount -= 0.05f;
                }

            }
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Warker"))
        {
            animator.SetTrigger("ExplosionTrigger");

            moveSpeed = 0;
            boxCollider2D.isTrigger = false;
            Destroy(gameObject, 0.8f);
        }
        if (collision.gameObject.CompareTag("Warrior"))
        {
            animator.SetTrigger("ExplosionTrigger");
            moveSpeed = 0;
            boxCollider2D.isTrigger = false;
            Destroy(gameObject, 0.8f);
        }
        if (collision.gameObject.CompareTag("Archer"))
        {
            animator.SetTrigger("ExplosionTrigger");
            moveSpeed = 0;
            boxCollider2D.isTrigger = false;
            Destroy(gameObject, 0.8f);
        }
        if (tagName == "Castle")
        {
            if (collision.gameObject.CompareTag("Castle"))
            {
                animator.SetTrigger("ExplosionTrigger");
                moveSpeed = 0;
                boxCollider2D.isTrigger = false;
                Destroy(gameObject, 0.8f);
            }
        }
    }
}
