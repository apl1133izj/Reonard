using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class Food : MonoBehaviour
{
    GameManager Manager;
    public TextMeshProUGUI textMeshProUGUI;
    Rigidbody2D rigidbody2; 
    void Awake()
    {
        rigidbody2 = GetComponent<Rigidbody2D>();
        Manager = FindObjectOfType<GameManager>();
    }
    private void Start()
    {
        rigidbody2.AddForce(new Vector2 (1f, 1f) * Time.deltaTime);
        transform.position = new Vector2(transform.position.x + 1, transform.position.y + 1);
        Debug.Log(Manager.foodeCountRandom = Random.Range(3, 5));
        Manager.foodeCountRandom = Random.Range(15, 20);
    }
    // Update is called once per frame
    void Update()
    {

        textMeshProUGUI.text = "X" + Manager.foodeCountRandom;
    }
}
