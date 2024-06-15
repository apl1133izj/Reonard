using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pumpkin : MonoBehaviour
{
    public float Growthtime;
    SpriteRenderer spriteRenderer;
    public Sprite[] spritePumpkin;
    public GameObject harvestGameObject;
    GameManager gameManager;
    public GameObject pumpfinXpGameObject;
    private void Awake()
    {
        gameManager = FindObjectOfType<GameManager>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        Growthtime += Time.deltaTime;
        if(Growthtime <= 50) 
        {
            spriteRenderer.sprite = spritePumpkin[0];
        }else if(Growthtime <= 90)
        {
            spriteRenderer.sprite = spritePumpkin[1];
        }
        else if (Growthtime <= 130)
        {
            spriteRenderer.sprite = spritePumpkin[2];
        }
        else if (Growthtime <= 170)
        {
            harvestGameObject.SetActive(true);
            spriteRenderer.sprite = spritePumpkin[3];
        }
    }
    public void harvestButton()
    {
        GameObject xpGameObject = Instantiate(pumpfinXpGameObject, transform.position, Quaternion.identity);
        gameManager.pempkinCount++;
        Destroy(gameObject);
    }
}
