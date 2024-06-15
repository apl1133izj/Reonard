using UnityEngine;

public class Item : MonoBehaviour
{
    Animator animator;
    public GameObject targetObject;
    public Rigidbody2D rb;
    private void Awake()
    {
       animator = GetComponent<Animator>();
    }
    void Start()
    {
        rb.AddForce(Vector2.up * 3f, ForceMode2D.Impulse);
        rb.velocity = Vector2.zero;
    }

    // Update is called once per frame
    void Update()
    {
        //TouchEvent();
        //MouseEvent();

    }

    void TouchEvent()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0); // ù ��° ��ġ ����Ʈ�� ������

            if (touch.phase == TouchPhase.Began)
            {
                HandleTouchBegin(touch.position);
            }
            else if (touch.phase == TouchPhase.Moved)
            {
                // ��ġ�� ������ ���� ó��
            }
            else if (touch.phase == TouchPhase.Ended)
            {
                // ��ġ�� ����� ���� ó��
            }
        }
    }
    void MouseEvent()
    {
        if (Input.GetMouseButtonDown(0))
        {
            // ���콺 ���� ��ư�� ������ ���� ó��
            HandleMouseDown(Input.mousePosition);
        }
        else if (Input.GetMouseButton(0))
        {
            // ���콺 ���� ��ư�� ���� ������ ���� ó��
            //HandleMouseMoved(Input.mousePosition);
        }
        else if (Input.GetMouseButtonUp(0))
        {
            // ���콺 ���� ��ư�� �������� ���� ó��
            //HandleMouseUp(Input.mousePosition);
        }
    }
    void HandleTouchBegin(Vector2 touchPosition)
    {
        // ��ġ�� ���۵� ���� ó��

        // ��ġ�� ��ġ�κ��� Ray�� ���� Ư�� ���� ������Ʈ�� �浹�ϴ��� Ȯ��
        Ray ray = Camera.main.ScreenPointToRay(touchPosition);
        RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction);

        // �浹�� ������Ʈ�� targetObject�� ��쿡 ���ϴ� �̺�Ʈ ����
        if (hit.collider != null && hit.collider.gameObject == targetObject)
        {
            Debug.Log("��ġ ���� - Ư�� ������Ʈ ��ġ!");
            // ���ϴ� �̺�Ʈ ����
            Destroy(hit.collider.gameObject);
        }
    }

    void HandleMouseDown(Vector2 mousePosition)
    {
        // ���콺 ���� ��ư�� ������ ���� ó��

        // ���콺�� Ŭ���� ��ġ�κ��� Ray�� ���� Ư�� ���� ������Ʈ�� �浹�ϴ��� Ȯ��
        Ray ray = Camera.main.ScreenPointToRay(mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction);


        // �浹�� ������Ʈ�� targetObject�� ��쿡 ���ϴ� �̺�Ʈ ����
        if (hit.collider != null && hit.collider.gameObject.CompareTag("Woode"))
        {
            Debug.Log("���콺 Ŭ�� - Ư�� ������Ʈ Ŭ��!");
            Destroy(gameObject);
            // ���ϴ� �̺�Ʈ ����
        }
    }
    public void WoodeAnimator()
    {
        animator.enabled = false;
    }
}
