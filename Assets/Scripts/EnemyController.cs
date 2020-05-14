using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    // Start is called before the first frame update
    public float runspeed;
    public float moveTime;
    private float timeLeft;
    private Rigidbody2D rb;
   
    SpriteRenderer spriteRenderer;

   

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        timeLeft = moveTime;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate() {
        Movement();
    }

    void Movement(){
        if (Timer()){
            runspeed = runspeed * -1; 
        }
        rb.velocity = new Vector2(runspeed, rb.velocity.y);
    }

    bool Timer(){
        if (timeLeft < 0){
            timeLeft = moveTime;
            return true;
        }else{
            timeLeft -= Time.deltaTime;
            return false;
        }
    }
    


}
