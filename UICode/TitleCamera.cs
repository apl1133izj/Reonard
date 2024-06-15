using UnityEngine;

public class TitleCamera : MonoBehaviour
{

    float slideTime;
    float speed = 1;
    void Update()
    {


        float moveDistance = Time.deltaTime * speed;
        transform.position = new Vector2(transform.position.x + moveDistance, transform.position.y);

    }



    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.name == "Clmap")
        {
            speed = -1;
        }
        if (collision.gameObject.name == "Clmap2")
        {
            speed = 1;
        }
    }

}
