using System.Collections;
using UnityEngine;
using UnityEngine.UI;
public class Sheep : MonoBehaviour
{
    Animator animator;
    Vector2 sheepPos;
    public GameManager manager;

    int x;
    int y;
    private Vector2 previousPosition;
    public bool farmGreadBool;
    public Image hungerGauge;

    public int pumpkinProbability;
    float positionThreshold = 0.01f;
    public GameObject pumpkinPrefab;
    public GameObject dieFood;

    public float growthTime;
    public bool sheepOld;
    private void Awake()
    {
        animator = GetComponent<Animator>();

        StartCoroutine(SheepMove());
    }
    void Start()
    {
        previousPosition = transform.position;
    }
    void Update()
    {
        Growth();
        manager = FindObjectOfType<GameManager>();
        if (farmGreadBool)
        {
            Vector2 targetPosition = new Vector2(x, y);

            sheepPos = Vector2.MoveTowards(transform.position, targetPosition, Time.deltaTime);

            transform.position = sheepPos;
            if (Vector2.Distance(sheepPos, targetPosition) < 0.01f)
            {
                animator.SetBool("RunBool", false);
            }
        }
        if (hungerGauge.fillAmount == 0)
        {
            Destroy(gameObject);
            GameObject inst = Instantiate(dieFood, previousPosition, Quaternion.identity);
        }
        SheepFlip();
    }
    void Growth()
    {
        growthTime += Time.deltaTime;

        if ((int)growthTime == 180)
        {
            transform.localScale = new Vector3(1.2f, 1.2f, 1f);
        }
        if (transform.localScale == new Vector3(1.2f, 1.2f, 1f))
        {
            sheepOld = true;
        }
    }
    void SheepFlip()
    {

        if (transform.position.x > previousPosition.x)
        {
            if (!sheepOld)
            {
                transform.localScale = new Vector2(0.7f, 0.7f);
            }
            else
            {
                transform.localScale = new Vector2(1.2f, 1.2f);
            }
        }
        else if (transform.position.x < previousPosition.x)
        {
            if (!sheepOld)
            {
                transform.localScale = new Vector2(-0.7f, 0.7f);
            }
            else
            {
                transform.localScale = new Vector2(-1.2f, 1.2f);
            }
        }


        previousPosition = transform.position;
    }
    void PumpkinProbabilityInst()
    {
        if (pumpkinProbability == 0)
        {

            GameObject inst = Instantiate(pumpkinPrefab, previousPosition, Quaternion.identity);

        }
    }
    IEnumerator SheepMove()
    {

        x = Random.Range(-6, 9);
        y = Random.Range(7, -5);

        animator.SetBool("RunBool", true);
        hungerGauge.fillAmount -= 0.02f;
        yield return new WaitForSeconds(12);
        hungerGauge.fillAmount -= 0.02f;
        pumpkinProbability = Random.Range(0, 7);
        PumpkinProbabilityInst();
        StartCoroutine(SheepMove());
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 12)
        {
            farmGreadBool = true;
        }
    }
}
