using UnityEngine;

public class insButtonUI : MonoBehaviour
{
    public GameObject[] buttonUI;
    public Warkerhouse warkerhouse;
    Animator animator;
    private void Awake()
    {
        animator = GetComponent<Animator>();    
    }

    public void ButtonUIFalse()
    {
        if (warkerhouse.buildCount == 1)
        {
            buttonUI[0].SetActive(false);
            buttonUI[1].SetActive(false);
            buttonUI[2].SetActive(false);
            buttonUI[4].SetActive(true);
            buttonUI[5].SetActive(true);
            buttonUI[6].SetActive(true);
            buttonUI[7].SetActive(true);
            animator.enabled = true;
        }
        else if (warkerhouse.buildCount == 2)
        {
            buttonUI[0].SetActive(true);
            buttonUI[1].SetActive(true);
            buttonUI[2].SetActive(true);
            buttonUI[4].SetActive(false);
            buttonUI[5].SetActive(false);
            buttonUI[6].SetActive(false);
            buttonUI[7].SetActive(false);
            animator.enabled = false;
            warkerhouse.buildCount = 0;
        }

    }
    public void animatorFalse()
    {
       if (warkerhouse.buildCount == 2)
        {
            buttonUI[3].SetActive(false);
            buttonUI[4].SetActive(false);
            buttonUI[5].SetActive(false);
            buttonUI[6].SetActive(false);
            warkerhouse.buildanimator.enabled = false;
        }

    }
}
