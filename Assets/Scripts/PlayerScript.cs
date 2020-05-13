using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    
    public float runspeed;
    public float jumpspeed;
    private Rigidbody2D rb;
    Vector2 moveVelocity;
    SpriteRenderer spriteRenderer;
    [SerializeField]
    Transform groundCheck;

    [SerializeField]
    Transform wallCheckRight;

    [SerializeField]
    Transform wallCheckLeft;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate() {
        MovementController();

    }
    void MovementController(){
        
        if (Input.GetKey("right")){
            rb.velocity = new Vector2(runspeed, rb.velocity.y);
            spriteRenderer.flipX = false;
        }
        else if(Input.GetKey("left")){
            rb.velocity = new Vector2(-runspeed, rb.velocity.y);
            spriteRenderer.flipX = true;
        }
        else{
            rb.velocity = new Vector2(0, rb.velocity.y);
        }
        if (Input.GetKey("space") && (isGrounded() || onWall()) ){
            rb.velocity = new Vector2(rb.velocity.x, jumpspeed);
        }

    }

    bool isGrounded(){
        if(Physics2D.Linecast(transform.position, groundCheck.position, 1 << LayerMask.NameToLayer("Ground"))){
            return true;
        }
        else{
            return false;
        }
    }

    bool onWall(){
        if(Physics2D.Linecast(transform.position, wallCheckRight.position, 1 << LayerMask.NameToLayer("Wall")) 
        || Physics2D.Linecast(transform.position, wallCheckLeft.position, 1 << LayerMask.NameToLayer("Wall"))){
        
            return true;
        }
        else{

            return false;
        }
    }
}

