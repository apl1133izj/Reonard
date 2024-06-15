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
            Touch touch = Input.GetTouch(0); // 첫 번째 터치 포인트를 가져옴

            if (touch.phase == TouchPhase.Began)
            {
                HandleTouchBegin(touch.position);
            }
            else if (touch.phase == TouchPhase.Moved)
            {
                // 터치가 움직일 때의 처리
            }
            else if (touch.phase == TouchPhase.Ended)
            {
                // 터치가 종료될 때의 처리
            }
        }
    }
    void MouseEvent()
    {
        if (Input.GetMouseButtonDown(0))
        {
            // 마우스 왼쪽 버튼이 눌렸을 때의 처리
            HandleMouseDown(Input.mousePosition);
        }
        else if (Input.GetMouseButton(0))
        {
            // 마우스 왼쪽 버튼이 눌린 상태일 때의 처리
            //HandleMouseMoved(Input.mousePosition);
        }
        else if (Input.GetMouseButtonUp(0))
        {
            // 마우스 왼쪽 버튼이 떼어졌을 때의 처리
            //HandleMouseUp(Input.mousePosition);
        }
    }
    void HandleTouchBegin(Vector2 touchPosition)
    {
        // 터치가 시작될 때의 처리

        // 터치한 위치로부터 Ray를 쏴서 특정 게임 오브젝트와 충돌하는지 확인
        Ray ray = Camera.main.ScreenPointToRay(touchPosition);
        RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction);

        // 충돌한 오브젝트가 targetObject일 경우에 원하는 이벤트 수행
        if (hit.collider != null && hit.collider.gameObject == targetObject)
        {
            Debug.Log("터치 시작 - 특정 오브젝트 터치!");
            // 원하는 이벤트 수행
            Destroy(hit.collider.gameObject);
        }
    }

    void HandleMouseDown(Vector2 mousePosition)
    {
        // 마우스 왼쪽 버튼이 눌렸을 때의 처리

        // 마우스가 클릭한 위치로부터 Ray를 쏴서 특정 게임 오브젝트와 충돌하는지 확인
        Ray ray = Camera.main.ScreenPointToRay(mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction);


        // 충돌한 오브젝트가 targetObject일 경우에 원하는 이벤트 수행
        if (hit.collider != null && hit.collider.gameObject.CompareTag("Woode"))
        {
            Debug.Log("마우스 클릭 - 특정 오브젝트 클릭!");
            Destroy(gameObject);
            // 원하는 이벤트 수행
        }
    }
    public void WoodeAnimator()
    {
        animator.enabled = false;
    }
}
