using Photon.Pun;
using System.Collections;
using UnityEngine;
using UnityEngine.Analytics;

public class Castle1 : MonoBehaviourPun
{

    public enum Players { player1 };
    public Players playerenum;
    public bool hit;
    public static int castle1Hp = 600;
    public static int castle1MAXHp = 600;
    public GameObject[] fireEffectGameObject;
    public float fireTime;
    public AudioSource[] fireSource;
    public float hitTime;
    public SpriteRenderer sprite;
    public Sprite catleSprite;

    public int syncedPlayerOrder;

    public Multimanager multimanager;

    public BridgeBuild building;
    public static bool gameOver;
    private void Start()
    {
        playerenum = Players.player1;
    }
    void Update()
    {
        if (hit)
        {
            StartCoroutine(hitco());
            fireSource[0].Play();
        }
        if (castle1Hp <= 500)
        {
            FireEffect();
        }
    }

    void FireEffect()
    {
        fireSource[1].Play();
        if (castle1Hp > 499)
        {

        }
        else if (castle1Hp > 450)
        {
            fireEffectGameObject[0].SetActive(true);
            fireEffectGameObject[1].SetActive(true);
        }
        else if (castle1Hp > 400)
        {
            fireEffectGameObject[2].SetActive(true);
        }
        else if (castle1Hp > 300)
        {
            fireEffectGameObject[3].SetActive(true);
        }
        else if (castle1Hp > 200)
        {
            fireEffectGameObject[4].SetActive(true);
        }
        else if (castle1Hp > 100)
        {
            fireEffectGameObject[5].SetActive(true);
        }
        else if (castle1Hp > 20)
        {
            fireEffectGameObject[6].SetActive(true);
        }

        if (castle1Hp < 0)
        {           
            gameOver = true;
            Debug.Log("gameOver:" + gameOver);
            Debug.Log("¼º1Ã¼·Â0");
        }

    }

    IEnumerator hitco()
    {

        transform.localScale = new Vector2(0.95f, 0.95f);
        yield return new WaitForSeconds(0.1f);
        transform.localScale = new Vector2(1f, 1);
        hit = false;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("AttackRangeG"))
        {
            hit = true;
            if (playerenum == Players.player1)
            {
                castle1Hp -= 3;
            }
        }
        if (collision.gameObject.CompareTag("Explosions"))
        {
            hit = true;
            if (playerenum == Players.player1)
            {
                castle1Hp -= 80;
            }
        }
        if (collision.gameObject.CompareTag("Dynamite"))
        {
            if (collision.gameObject.GetComponent<Dynamite>().tagName == "Castle")
            {
                hit = true;
                if (playerenum == Players.player1)
                {
                    castle1Hp -= 10;
                }
            }
        }
    }
}
