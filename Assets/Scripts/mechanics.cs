using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class mechanics : MonoBehaviour
{
    private float speed_X;
    [SerializeField] private float jump = 8f;
    [SerializeField] private float speed = 5f, jumpSpeed = 1f;
    private Rigidbody2D rb;
    private SpriteRenderer rbSprite;
    private Animator animator;
    private BoxCollider2D bcol;
    private static bool superJump = true;
    private enum anim { Idle, Jump, Run, Fall, Crouch, SuperJump, SuperJumpV};
    [SerializeField] private LayerMask jumpRange;
    [SerializeField] private bool playerIsAlive = true;
    private bool facingRight = true;
    anim state;
    private GameObject charObject;
    private GameObject attackCollider;
    private BoxCollider2D snap;
    private EdgeCollider2D ecoll;
    public int health = 4;
    healtBarScript healtBarScript;



    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rbSprite = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        bcol = GetComponent<BoxCollider2D>();
        charObject = GameObject.Find("CoolSnake");
        attackCollider = charObject.transform.GetChild(0).gameObject;
        snap = attackCollider.GetComponent<BoxCollider2D>();
        snap.enabled = false;
        ecoll= GetComponent<EdgeCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!playerIsAlive)
        {
            return;
        }
        if (Input.GetKey("r"))
        {
            resetLevel();
        }
        attack();
        crouch();
        run();
        animations();
        if (speed_X > 0f && !facingRight)
        {
            Flip();
        }
        else if (speed_X < 0f && facingRight)
        {
            Flip();
        }
    }

    private void resetLevel()
    {
        Invoke("sceneLoad", 0.3f);
    }
    private void sceneLoad()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    private void attack()
    {
        if (Input.GetKeyDown("q"))
        {
            Debug.Log("You attacked.");
        }
    }
    private void crouch()
    {
        if (Input.GetKeyDown("c") && gcheck())
        {
            bcol.offset = new Vector2(0.002148628f, -0.12f);
            bcol.size = new Vector2(0.8932524f, 1.647364f);
        }
        else if (Input.GetKeyUp("c") && gcheck())
        {
            bcol.offset = new Vector2(0.002148628f, -0.05026014f);
            bcol.size = new Vector2(0.8932524f, 1.80348f);
        }
    }
    private void run()
    {
        if (gcheck())
        {
            speed_X = Input.GetAxisRaw("Horizontal");
            rb.velocity = new Vector2(speed_X * speed, rb.velocity.y);
        }
        else
        {
            speed_X = Input.GetAxisRaw("Horizontal");
            rb.velocity = new Vector2(speed_X * speed / jumpSpeed, rb.velocity.y);
        }
        if (Input.GetButtonDown("Jump") && gcheck())
        {
            if (Input.GetKey("c"))
            {
                superJump = true;
                rb.velocity = new Vector2(rb.velocity.x, jump * 2);
            }
            else
            {
                superJump = false;
                rb.velocity = new Vector2(rb.velocity.x, jump);
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            hurt();
        }
    }

    private void animations()
    {
        //attack
        if (Input.GetKeyUp("q"))
        {
            animator.SetBool("attack", true);
        }
        else
        {
            animator.SetBool("attack", false);
        }

        //run
        if (speed_X != 0f)
        {
            state = anim.Run;
        }
        else
        {
            state = anim.Idle;
        }
        //jump
        if (rb.velocity.y > 0.1f)
        {
            //state = anim.Jump;
            if (superJump == true)
            {
                if (Input.GetKey("a") || Input.GetKey("d"))
                {
                    state = anim.SuperJump;
                }
                else
                {
                    state = anim.SuperJumpV;
                }
            }
            else
            {
                state = anim.Jump;
            }
        }
        //fall
        else if (rb.velocity.y < -0.1f)
        {
            state = anim.Fall;
        }
        //crouch
        if (Input.GetKey("c") && gcheck())
        {
            state = anim.Crouch;
        }
        animator.SetInteger("state", (int)state);
    }

    private bool gcheck()
    {
        return Physics2D.BoxCast(ecoll.bounds.center, ecoll.bounds.size, 0f, Vector2.down, .1f, jumpRange);
    }

    public void hurt()
    {
        health--;
        if (health == 0)
        {
            die();
        }
        else
        {
            animator.SetTrigger("hurt");
        }
    }
    private void die()
    {
        rb.bodyType = RigidbodyType2D.Static;
        playerIsAlive = false;
        animator.SetTrigger("death");
    }

    void activateCollider()
    {
        snap.enabled = true;
    }

    void deactivateCollider()
    {
        snap.enabled = false;
    }
    private void Flip()
    {
        transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
        facingRight = !facingRight;
    }
}
