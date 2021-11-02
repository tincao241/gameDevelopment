using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.ComTypes;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator animator;
    private Collider2D coll;
    
    

    //Các biến cho State
    private enum State { idle, running, jumping, falling, hurt}
    private State state = State.idle;
    
    //Các biến cho Player
    [SerializeField] private LayerMask ground;
    [SerializeField] private float speed = 4f;
    [SerializeField] private float jumpForce = 8f;
    [SerializeField] private int cherries = 0;
    [SerializeField] private Text cherryText ;
    [SerializeField] private float hurtForce=10f;
    [SerializeField] private AudioSource cherry;
    [SerializeField] private AudioSource footsteps;
    [SerializeField] private AudioSource jumpSound;
    [SerializeField] private AudioSource wounded;



    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        coll = GetComponent<Collider2D>();
        footsteps = GetComponent<AudioSource>();
    } 

    private void Update()
    {
        if (state != State.hurt)
        {
            PlayerMovement();
        }
        
        //Gọi function cho animation
        VelocityState();
        animator.SetInteger("state", (int)state); //Set giá trị cho animation

    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        Cherry cherryCom = collision.gameObject.GetComponent<Cherry>();
        if (collision.tag == "Collectible")
        {
            cherry.Play();
            cherryCom.Collects();
            cherries += 1;
            cherryText.text = cherries.ToString();
        }
        
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            Enemy enemy = collision.gameObject.GetComponent<Enemy>();

            if (state == State.falling || state == State.jumping)
            {
                enemy.JumpedOn();
                Jump();
            }
            else
            {
                state = State.hurt;
                if (collision.gameObject.transform.position.x > transform.position.x)
                {

                    rb.velocity = new Vector2(-hurtForce, rb.velocity.y);
                }
                else
                {
                    rb.velocity = new Vector2(hurtForce, rb.velocity.y);
                }
                wounded.Play();

            }
            
        }
    }

    private void PlayerMovement()
    {
        float hDirection = Input.GetAxis("Horizontal");

        //Đi qua trái
        if (hDirection < 0)
        {
            rb.velocity = new Vector2(-speed, rb.velocity.y);

            transform.localScale = new Vector2(-6, 6);

        }
        //Đi qua phải
        else if (hDirection > 0)
        {
            rb.velocity = new Vector2(speed, rb.velocity.y);
            transform.localScale = new Vector2(6, 6);
        }
        //Nhảy
        if (Input.GetButtonDown("Jump") && coll.IsTouchingLayers(ground))
        {
            Jump();
        }
    }
    private void Jump()
    {
        jumpSound.Play();
        rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        state = State.jumping;
    }

    //Function cho animation
    private void VelocityState()
    {
        if (state == State.jumping)
        {
            if (rb.velocity.y < .1f)
            {
                state = State.falling;
            }
        }
        else if (state == State.falling)
        {
            if (coll.IsTouchingLayers(ground))
            {
                state = State.idle;
            }
        }
        else if (state == State.hurt)
        {
            if (Mathf.Abs(rb.velocity.x )< .1f)
            {
                state = State.idle;
            }
        }
        else if (Mathf.Abs(rb.velocity.x) > 2f)
        {
            state = State.running;
        }
        else
        {
            state = State.idle;
        }
        
    }

    private void Footstep()
    {
        footsteps.Play(); 
    }
    

}

