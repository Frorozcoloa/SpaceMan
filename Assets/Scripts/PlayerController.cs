using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    
    // Const
    const string STATE_ON_GROUD = "isOnTheGround";
    const string STATE_ALIVE = "isAlive";

    //Variabler of moviment
    public float jumpForce = 10f;
    public float runningSpeed = 2f;
    private Rigidbody2D rigidBody;
    public LayerMask groundMask;
    Animator animator;
    private SpriteRenderer spriteRenderer;
    public static PlayerController sharedInstance;
    Vector3 startPosition = Vector3.zero;




    // Start is called before the first frame update
    private void Awake()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        if(sharedInstance == null)
        {
            sharedInstance = this;
        }

    }
    void Start()
    {
        startPosition= transform.position;
    }

    public void StartGame()
    {
        animator.SetBool(STATE_ON_GROUD, true);
        animator.SetBool(STATE_ALIVE, true);
        Invoke("RestarPosition", 1f);
    }
       
    void RestarPosition()
    {
            transform.position = startPosition;
            rigidBody.velocity = Vector2.zero;
     
    }
    // Update is called once per frame
    void Update()
    {

        if ( Input.GetButtonDown("Vertical") && GameManager.sharedInstance.currentGameState == GameState.Playing)
        {
            Jump();
        }
        animator.SetBool(STATE_ON_GROUD, IsTouchingTheGround());
       
    }
    void FixedUpdate()
    {
        if(GameManager.sharedInstance.currentGameState == GameState.Playing)
        {
            MovePlaver();
        }
        else
        {
            rigidBody.velocity = new Vector2(0, rigidBody.velocity.y);
        }
        
    }
    void Jump()
    {
        // Es la acción de saltar.
        if (IsTouchingTheGround())
        {
            rigidBody.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }
    }
    void MovePlaver()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        Vector2 velocity = new Vector2(horizontalInput * runningSpeed, rigidBody.velocity.y);
        rigidBody.velocity = velocity;
        if (horizontalInput > 0)
        {
            spriteRenderer.flipX = false;
        }
        else if (horizontalInput <0)
        {
            spriteRenderer.flipX = true;
        }
    }
    bool IsTouchingTheGround()
    {
        // Nos indica si el personaje está o no tocando el suelo.
        bool isTouchingTheGround = Physics2D.Raycast(transform.position, Vector2.down, 1f, groundMask);
        return isTouchingTheGround;
    } 
    public void Die()
    {
        animator.SetBool(STATE_ALIVE, false);
        GameManager.sharedInstance.GameOver();
    }
}
