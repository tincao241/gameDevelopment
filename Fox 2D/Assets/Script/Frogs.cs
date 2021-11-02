using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Frogs : Enemy
{
    [SerializeField] private LayerMask ground;

    [SerializeField] private float leftCap;
    [SerializeField] private float rightCap;

    [SerializeField] private float jumpLength = 3f;
    [SerializeField] private float jumpHeight = 3f;

    private Collider2D coll;
    private bool facingLeft = true;


    protected override void Start()
    {
        base.Start(); 
        coll = GetComponent<Collider2D>();
        
    }

    private void Update()
    {
        if (animator.GetBool("jumping"))
        {
            if (rb.velocity.y < .1)
            {
                animator.SetBool("falling", true);
                animator.SetBool("jumping", false);
            }
        }
        if (coll.IsTouchingLayers(ground) && animator.GetBool("falling"))
        {
            animator.SetBool("falling",false);

        }
    }

    private void Move()
    {
        if (facingLeft)
        {
            if (transform.position.x > leftCap)
            {
                if (transform.localScale.x != 4.4)
                {
                    transform.localScale = new Vector3(4.4f, 4.2f);
                }

                if (coll.IsTouchingLayers(ground))
                {
                    rb.velocity = new Vector2(-jumpLength, jumpHeight);
                    animator.SetBool("jumping", true);
                }
            }
            else
            {
                facingLeft = false;
            }

        }

        else
        {
            if (transform.position.x < rightCap)
            {
                if (transform.localScale.x != -4.4)
                {
                    transform.localScale = new Vector3(-4.4f, 4.2f);
                }

                if (coll.IsTouchingLayers(ground))
                {
                    rb.velocity = new Vector2(jumpLength, jumpHeight);
                    animator.SetBool("jumping", true);
                }
            }
            else
            {
                facingLeft = true;
            }


        }
    }
    
}
