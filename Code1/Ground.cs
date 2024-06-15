using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ground : MonoBehaviour
{ 
    public bool goblinInvasionbool;

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("GoblinWarrior") || collision.gameObject.CompareTag("Goblin Archer") || collision.gameObject.CompareTag("Bomber Goblin"))
        {

            goblinInvasionbool = true;
        }
    }
}
