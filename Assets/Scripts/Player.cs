using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public int health;
    public int attack;
    public float speed;
    public int souls;
    public Animator animator;

    public void StandAttack(){
       animator.SetTrigger("attack");
    }

    public void Idle(){
        animator.Play("idle");
    }

    public void Run(){
        animator.Play("run");
    }

    public void RunAttack(){
       animator.SetTrigger("run_attack");
    }

    public bool Running() {
        return animator.GetBool("run");
    }
}
