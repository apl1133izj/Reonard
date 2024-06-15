using UnityEngine;
using System.Collections;
using TMPro;
public class Mine : MonoBehaviour
{
    Warker warker;
    public float mineWarkeTime;
    public int goldCount;
    public int goldCountPluse;
    public int goldCountMAx;
    public GameObject goldPrefab;
    public GameObject warkerPrefab;
    public LayerMask layerMask;
    public bool warkerMineOn;
    public bool warkerMineOff;
    public string houseNameMine;//�� �̸��� ���꿡����
    int warkerIN;
    public SpriteRenderer spriteRenderer;
    public Sprite[] mineWarkesprite;
    public TextMeshProUGUI maxWarker;
    public AudioSource audioSource;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        mineWarke();
        wakerInMine();
        maxWarker.text = "[최대 인원 : 3] | " + warkerIN;
    }

    void wakerInMine()
    {
        Collider2D collider2D = Physics2D.OverlapCircle(transform.position, 0.1f, layerMask);
        if (collider2D != null)
        {
            warker = collider2D.gameObject.GetComponent<Warker>();
            if (warker != null)  // warker�� null���� Ȯ��
            {
                audioSource.Play();
                houseNameMine = warker.houseName;
                if (warker.tagName == "Mine" && !warkerMineOff)
                {
                    warkerMineOn = true;
                    spriteRenderer.sprite = mineWarkesprite[1];
                    warkerIN++;
                    if (warkerIN <= 3)
                    {
                        Destroy(collider2D.gameObject);
                    }
                    else if (warkerIN == 4)
                    {
                        audioSource.Stop();
                        warker.GoHouse();
                    }

                }
            }
        }
    }
    void mineWarke()
    {
        if (warkerIN == 1)
        {
            goldCountMAx = 6;
            goldCountPluse = 1;
        }
        else if (warkerIN == 2)
        {
            goldCountMAx = 12;
            goldCountPluse = 2;
        }
        else if (warkerIN == 3)
        {
            goldCountMAx = 18;
            goldCountPluse = 3;
        }

        if (warkerMineOn && !warkerMineOff)
        {
            mineWarkeTime += Time.deltaTime;
            Mathf.RoundToInt(mineWarkeTime);
            if (mineWarkeTime >= 30)
            {
                goldCount += goldCountPluse;
                mineWarkeTime = 0;
                StartCoroutine(InstGoid());
                if (goldCount == goldCountMAx)
                {
                    spriteRenderer.sprite = mineWarkesprite[0];

                    GameObject WarkerGameObject = Instantiate(warkerPrefab, transform.position, Quaternion.identity);
                    Warker warkerScript = WarkerGameObject.GetComponent<Warker>();
                    warkerScript.houseName = houseNameMine;
                    goldCount = 0;
                    warkerScript.tagName = "Mine";
                    warkerScript.mineWarkeEnd = true;
                    warkerScript.warkerActionsBool = false;
                    warkerMineOn = false;
                    warkerMineOff = true;
                }
            }
        }
    }
    IEnumerator InstGoid()
    {
        while (true)
        {
            if (warkerIN == 1)
            {
                GameObject goldGameObject = Instantiate(goldPrefab, transform.position, Quaternion.identity);
                break;
            }
            else if (warkerIN == 2)
            {
                GameObject goldGameObject = Instantiate(goldPrefab, transform.position, Quaternion.identity);
                yield return new WaitForSeconds(1f);
                GameObject goldGameObject2 = Instantiate(goldPrefab, transform.position, Quaternion.identity);
                break;
            }
            else if (warkerIN == 3)
            {
                GameObject goldGameObject = Instantiate(goldPrefab, transform.position, Quaternion.identity);
                yield return new WaitForSeconds(1f);
                GameObject goldGameObject2 = Instantiate(goldPrefab, transform.position, Quaternion.identity);
                yield return new WaitForSeconds(1f);
                GameObject goldGameObject3 = Instantiate(goldPrefab, transform.position, Quaternion.identity);
                break;
            }
        }
    }

    private void OnDrawGizmos()
    {
        Collider2D collider2D = Physics2D.OverlapCircle(transform.position, 1, gameObject.layer = 6);

        // Gizmos�� ����Ͽ� �ð������� ǥ��
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, 1);
    }
}
