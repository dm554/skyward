using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerScript : MonoBehaviour
{
    
    public float runspeed;
    public float jumpspeed;
    
    public int coinWinCon;
    private int coinCount;
    
    public float dashTime;
    private float timeLeft;
    private float dashCount;
    public float dashSpeed;

    private Rigidbody2D rb;
    Animator animator;
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
        animator = GetComponent<Animator>();
        timeLeft = dashTime;
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
            if(isGrounded()){
                animator.Play("Player_Run");
            }
            
            spriteRenderer.flipX = false;
        }
        else if(Input.GetKey("left")){
            rb.velocity = new Vector2(-runspeed, rb.velocity.y);
            if(isGrounded()){
                animator.Play("Player_Run");
            }
            spriteRenderer.flipX = true;
        }
        else{
            rb.velocity = new Vector2(0, rb.velocity.y);
            if(isGrounded()){
                animator.Play("Player_Idle");
            }
        }
        if (Input.GetKey("space") && (isGrounded() || onWall()) ){
            rb.velocity = new Vector2(rb.velocity.x, jumpspeed);
            animator.Play("Player_Jump");
        }
        if (isGrounded()){
            dashCount = 0; 
            timeLeft = dashTime;
        }
        if(Input.GetKey("z") && dashCount < 1){
            playerDash();

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

    void playerDeath(){
        Debug.Log("Player Death");
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.tag == "Enemy" 
        || other.gameObject.tag == "Hazard"){
            playerDeath();
        }
        if (other.gameObject.tag == "Pickup"){
            PickupHandler(other.gameObject);
        }
        if (other.gameObject.tag == "Level Completer"){
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }

    void PickupHandler(GameObject pickup){
        if(pickup.tag == "Coin"){
            Debug.Log("Coin picked up");
            coinCount++;
        }
        Destroy(pickup);
    }

    void playerDash(){
        while(timeLeft > 0){
            if (spriteRenderer.flipX){
                rb.velocity = new Vector2(-dashSpeed, rb.velocity.y);
            }
            else{
                rb.velocity = new Vector2(dashSpeed, rb.velocity.y);
            }timeLeft -= Time.deltaTime;
            return;
        }
        dashCount++;
    }

    void LevelComplete(){

    }
    
}

