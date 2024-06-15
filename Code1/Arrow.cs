using UnityEngine;

public class Arrow : MonoBehaviour
{
    public float moveSpeed;//�÷��̾� ���ǵ�
    public float checkRadius = 1.0f; // Ȯ���� �ݰ�
    public LayerMask goblinLayer;
    public bool goblinActionsBool;//������ �ٸ� ����� �ִ��� 
    public string tagName;//����� ã�� tag
    public bool goblinFound;//�� �� �Ÿ��� ã�Ҵ°�
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


        // ��� ��ġ ���
        Vector2 relativePosition = rotion - (Vector2)transform.position;

        // ��� ��ġ���� ���� ��� (���� ����)
        float angleRadians = Mathf.Atan2(relativePosition.y, relativePosition.x);

        // ���ȿ��� ������ ��ȯ
        float angleDegrees = angleRadians * Mathf.Rad2Deg;
        Debug.Log(angleDegrees);
        // ȸ�� ����
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

            // ��� ������ ���� �Ÿ��� ����ϰ� ���� ����� ���� ã��
            foreach (GameObject warkerActions in warkerActionGameObject)
            {
                float distanceToTree = Vector2.Distance(transform.position, new Vector2(warkerActions.transform.position.x + stopPosX, warkerActions.transform.position.y + stopPosy));
                //Debug.Log(distanceToTree);
                if (distanceToTree < closestDistance)
                {
                    // ���� ������ �� ������ ������Ʈ
                    closestTree = warkerActions;
                    closestDistance = distanceToTree;
                }
            }

            if (closestTree != null)
            {
                // ���� ����� ������ ���� ó�� ����
                // Debug.Log("���� ����� ����: " + closestTree.name + ", �Ÿ�: " + closestDistance);
                //������ �������� ��� true
                // ���� ����� gameobject(����,����)�� ���� �߰� �۾�

                if (!goblinActionsBool)
                {
                    Vector2 targetPosition = new Vector2(closestTree.transform.position.x + stopPosX, closestTree.transform.position.y + stopPosy);
                    transform.position = Vector2.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
                    ArrowFlip(targetPosition);
                }
                if (closestDistance <= 0f)
                {
                    //"��Ʈ"
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
