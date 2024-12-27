using TMPro;
using UnityEngine;
public class CuserKind : MonoBehaviour
{
    SpriteRenderer cuserSpriteRenderer;
    public Sprite[] spriteBuildKindCusers;
    public buildeMouseAndTouch bMTouch;
    private new BoxCollider2D collider2D;
    public bool BuildCuser;
    public GameManager gameManager;
    public float mouseYPos;
    ChageMap chageMap;

    public GameObject sheepslaughterGameObject;//占썹도占쏙옙
    public bool historicalTextGameObjectBool;
    public GameObject historicalTextGameObject;
    public bool[] cuserKind;
    public bool[] variousKind;
    public GameObject xp;//xp占쏙옙占쏙옙占쏙옙 占쏙옙占쏙옙占쏙옙 占쌉댐옙 효占쏙옙 占쏙옙占쏙옙
    public GameObject[] various;//占쏙옙占쏙옙,占쏙옙,占쏙옙占?占쏙옙占?占쌉댐옙 효占쏙옙 占쏙옙占쏙옙
    public TextMeshProUGUI sheepSlaughterText;

    public Castle1 castle1;
    public Castle2 castle2;
    private void Start()
    {
        //Cursor.visible = false;
    }
    private void Awake()
    {
        chageMap = FindObjectOfType<ChageMap>();
        cuserSpriteRenderer = GetComponent<SpriteRenderer>();
    }
    // Update is called once per frame
    void Update()
    {

        collider2D = GetComponent<BoxCollider2D>();
        bMTouch = FindObjectOfType<buildeMouseAndTouch>();
        transform.position = new Vector2(bMTouch.mousePosition.x, bMTouch.mousePosition.y);
        if (bMTouch.buildKind == buildeMouseAndTouch.BuildKind.BuildHouse)
        {
            collider2D.size = new Vector2(2, 2);
            cuserSpriteRenderer.sprite = spriteBuildKindCusers[1];
        }
        else if (bMTouch.buildKind == buildeMouseAndTouch.BuildKind.BuildMine)
        {
            collider2D.size = new Vector2(3, 2);
            cuserSpriteRenderer.sprite = spriteBuildKindCusers[2];
        }
        else if (bMTouch.buildKind == buildeMouseAndTouch.BuildKind.BuildTower)
        {
            collider2D.size = new Vector2(2, 3);
            cuserSpriteRenderer.sprite = spriteBuildKindCusers[3];
        }
        else if (bMTouch.buildKind == buildeMouseAndTouch.BuildKind.False)
        {
            collider2D.size = new Vector2(1, 1);
            cuserSpriteRenderer.sprite = spriteBuildKindCusers[4];
        }
        else if (bMTouch.buildKind == buildeMouseAndTouch.BuildKind.Null)
        {
            collider2D.size = new Vector2(0.12f, 0.1f);
            cuserSpriteRenderer.sprite = spriteBuildKindCusers[0];
        }
        else if (bMTouch.buildKind == buildeMouseAndTouch.BuildKind.Feed)
        {
            collider2D.size = new Vector2(1, 1);
            cuserSpriteRenderer.sprite = spriteBuildKindCusers[5];
        }
        else if (bMTouch.buildKind == buildeMouseAndTouch.BuildKind.Slaughter)
        {
            collider2D.size = new Vector2(1, 1);
            cuserSpriteRenderer.sprite = spriteBuildKindCusers[6];
        }

        if (!BuildCuser && bMTouch.buildStart)
        {
            cuserSpriteRenderer.sprite = spriteBuildKindCusers[4];
        }
    }
    void InstantiateWoode()
    {
        GameObject WoodeGameObject = Instantiate(various[0], transform.position, Quaternion.identity);
        Destroy(WoodeGameObject, 1.5f);
    }
    void InstantiateGold()
    {
        GameObject GoldGameObject = Instantiate(various[1], transform.position, Quaternion.identity);
        Destroy(GoldGameObject, 1.5f);
    }
    void InstantiateFood()
    {
        GameObject FoodGameObject = Instantiate(various[2], transform.position, Quaternion.identity);
        Destroy(FoodGameObject, 1.5f);
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (Input.GetMouseButton(0))
        {
            if (collision.gameObject.CompareTag("Woode"))
            {
                Destroy(collision.gameObject);
                cuserKind[0] = true;
                GameObject xpGameObject = Instantiate(xp, transform.position, Quaternion.identity);
                Destroy(xpGameObject, 1.5f);
                Invoke("InstantiateWoode", 0.5f);
                //gameManager.woodeCount++;
                if (castle1 != null)
                {
                    if (castle1.playerenum == Castle1.Players.player1)
                    {
                        Multimanager.attackCoins += 5;
                    }
                }
                if (castle2 != null)
                {
                    if (castle2.playerenum == Castle2.Players.player2)
                    {
                        Debug.Log("player2");
                        Multimanager1.attackCoins += 5;
                    }
                }
            }
            if (collision.gameObject.CompareTag("Gold"))
            {
                Destroy(collision.gameObject);
                cuserKind[1] = true;
                GameObject xpGameObject = Instantiate(xp, transform.position, Quaternion.identity);
                Destroy(xpGameObject, 1.5f);
                Invoke("InstantiateGold", 0.5f);
                if (castle1 != null)
                {
                    if (castle1.playerenum == Castle1.Players.player1)
                    {
                        Multimanager.attackCoins += 15;
                    }
                }
                if (castle2 != null)
                {
                    if (castle2.playerenum == Castle2.Players.player2)
                    {
                        Multimanager1.attackCoins += 15;
                    }
                }
                //gameManager.moneyCount++;
            }
            if (collision.gameObject.CompareTag("Food"))
            {
                Destroy(collision.gameObject);
                cuserKind[2] = true;
                GameObject xpGameObject = Instantiate(xp, transform.position, Quaternion.identity);
                Destroy(xpGameObject, 1.5f);
                Invoke("InstantiateFood", 0.5f);
                if (castle1 != null)
                {
                    if (castle1.playerenum == Castle1.Players.player1)
                    {
                        Multimanager.attackCoins += 25;
                    }
                }
                if (castle2 != null)
                {
                    if (castle2.playerenum == Castle2.Players.player2)
                    {
                        Multimanager1.attackCoins += 25;
                    }
                }
                // gameManager.foodCount += gameManager.foodeCountRandom;
            }
        }
        //건물이 지어질수 있는 땅인지 확인
        if (collision.gameObject.layer == 11 && bMTouch.buildStart)
        {
            BuildCuser = true;
            bMTouch.buildGread = true;
        }
        else
        {
            BuildCuser = false;
            bMTouch.buildGread = false;
        }
    }
 private void OnTriggerStay2D(Collider2D collision)
 {
     if (Input.GetMouseButton(0))
     {
         //먹이주기
         if (collision.gameObject.CompareTag("Sheep") && bMTouch.buildKind == buildeMouseAndTouch.BuildKind.Feed)
         {
             Sheep sheep = collision.gameObject.GetComponent<Sheep>();
             if (gameManager.pempkinCount > 0)
             {
                 Debug.Log("pempkinCount--");
                 cuserKind[3] = true;
                 gameManager.pempkinCount--;
                 sheep.hungerGauge.fillAmount += 0.4f;
                 if (castle1 != null)
                 {
                     if (castle1.playerenum == Castle1.Players.player1)
                     {
                         Multimanager.attackCoins += 25;
                     }
                 }
                 if (castle2 != null)
                 {
                     if (castle2.playerenum == Castle2.Players.player2)
                     {
                         Multimanager1.attackCoins += 25;
                     }
                 }
             }
             bMTouch.buildKind = buildeMouseAndTouch.BuildKind.Null;
         }
         //도축
         if (collision.gameObject.CompareTag("Sheep") && bMTouch.buildKind == buildeMouseAndTouch.BuildKind.Slaughter)
         {
             Sheep sheep = collision.gameObject.GetComponent<Sheep>();
             if (sheep.sheepOld)
             {
                 GameObject insFood = Instantiate(sheepslaughterGameObject, transform.position, Quaternion.identity);
                 if (castle1 != null)
                 {
                     if (castle1.playerenum == Castle1.Players.player1)
                     {
                         Multimanager.attackCoins += 25;
                     }
                 }
                 if (castle2 != null)
                 {
                     if (castle2.playerenum == Castle2.Players.player2)
                     {
                         Multimanager1.attackCoins += 25;
                     }
                 }
                 Destroy(collision.gameObject);
                 cuserKind[4] = true;
                 bMTouch.buildKind = buildeMouseAndTouch.BuildKind.Null;
             }
         }
     }
 }
}
