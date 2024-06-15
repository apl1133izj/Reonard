using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrainingButton : MonoBehaviour
{

    public Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();    
    }
    public void ArcherButtonEventAnimator()
    {
        animator.enabled = true;
        animator.SetBool("Warrior Bool", false);
        animator.SetBool("Archer Bool", true);
    }

    public void WarriorButtonEventAnimator()
    {
        animator.enabled = true;
        animator.SetBool("Warrior Bool", true);
        animator.SetBool("Archer Bool", false);
    }
}
