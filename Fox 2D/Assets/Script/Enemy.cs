using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    protected Animator animator;
    protected Rigidbody2D rb;
    protected AudioSource audio;

    protected virtual void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        audio = GetComponent<AudioSource>();
    }

    public void JumpedOn()
    {
        animator.SetTrigger("death");
        audio.Play();
    }

    private void Death()
    {
        Destroy(this.gameObject);
    }
}
