using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public PlayerState curState;            

    // values
    public float moveSpeed;                 
    public float flyingSpeed;               
    public bool grounded;                
    public float stunDuration;              
    private float stunStartTime;        
    
    public Rigidbody2D rig;              
    public Animator anim;                 
    public ParticleSystem jetpackParticle;  

    void FixedUpdate ()
    {
        grounded = IsGrounded();
        CheckInputs();
        
        if(curState == PlayerState.Stunned)
        {
            if(Time.time - stunStartTime >= stunDuration)
            {
                curState = PlayerState.Idle;
            }
        }
    }
    
    void CheckInputs ()
    {
        if (curState != PlayerState.Stunned)
        {
            Move();
            
            if (Input.GetKey(KeyCode.UpArrow))
                Fly();
            else
                jetpackParticle.Stop();
        }
        
        SetState();
    }
    
    void SetState ()
    {
        if (curState != PlayerState.Stunned)
        {
            if (rig.velocity.magnitude == 0 && grounded)
                curState = PlayerState.Idle;
            if (rig.velocity.x != 0 && grounded)
                curState = PlayerState.Walking;
            if (rig.velocity.magnitude != 0 && !grounded)
                curState = PlayerState.Flying;
        }
        
        anim.SetInteger("State", (int)curState);
    }
    
    void Move ()
    {
        float dir = Input.GetAxis("Horizontal");
        
        if (dir > 0)
            transform.localScale = new Vector3(1, 1, 1);
        else if (dir < 0)
            transform.localScale = new Vector3(-1, 1, 1);
        
        rig.velocity = new Vector2(dir * moveSpeed, rig.velocity.y);
    }
    
    void Fly ()
    {
        rig.AddForce(Vector2.up * flyingSpeed, ForceMode2D.Impulse);
        
        if (!jetpackParticle.isPlaying)
            jetpackParticle.Play();
    }
    
    public void Stun ()
    {
        curState = PlayerState.Stunned;
        rig.velocity = Vector2.down * 3;
        stunStartTime = Time.time;
        jetpackParticle.Stop();
    }
    
    bool IsGrounded ()
    {
        RaycastHit2D hit = Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y - 0.85f), Vector2.down, 0.3f);
        
        if(hit.collider != null)
        {
            if(hit.collider.CompareTag("Floor"))
            {
                return true;
            }
        }

        return false;
    }
    
    void OnTriggerEnter2D (Collider2D col)
    {
        if(curState != PlayerState.Stunned)
        {
            if(col.CompareTag("Obstacle"))
            {
                Stun();
            }
        }
    }
}

public enum PlayerState
{
    Idle,       // 0
    Walking,    // 1
    Flying,     // 2
    Stunned     // 3
}