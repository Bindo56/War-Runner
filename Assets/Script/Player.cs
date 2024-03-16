using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Player : MonoBehaviour
{
    

    
    public Rigidbody2D rb;
     public Animator anim;
    private SpriteRenderer sr;
    [HideInInspector] public bool playerUnlocked;
    public bool isDead;
    [HideInInspector] public bool extraLife;
    private bool dontJump;
    [SerializeField] private ParticleSystem DustFX;
    public ParticleSystem BloodFX;
    private bool readyToLand;

    [Header("Shoot")]
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] Transform shootPoint;
    [SerializeField] int bulletSpeed;
    [SerializeField] float maxDistance;
    [SerializeField] bool canShoot;
  //  [SerializeField] Transform playerPosition;
  //  [SerializeField] 


    [Header("Jump Info")]
    [SerializeField] public float jumpForce;
    [SerializeField] private float doubleJumpForce;
    private bool canDoubleJump;

    [Header("Knocked")]
    public Vector2 knockBackDir;
    private bool isKnocked;
    public bool canBeKnock = true;


    [Header("Move Info")]
    [SerializeField] private float speedToSurvive = 16;
    [SerializeField]public float moveSpeed;
    [SerializeField] private float maxSpeed;
    private float defaultSpeed;
    private float defaultMilestoneIncreaser;
    [SerializeField] private float speedmultipler;
    [Space]
    [SerializeField] private float milestoneIncreaser;
    private float speedMilestone;


    [Header("Slide info")]
    [SerializeField] private float slideSpeed;
    [SerializeField] private float slideTime;
    [SerializeField] private float slidecooldown;
    private float slidecooldowncounter;
    private bool cellingdetected;
    //[SerializeField] private Transform celling;

    private float slideTimeCounter;
    private bool isSliding;


    [Header("ledge info")]
    [SerializeField] private Vector2 offset1;
    [SerializeField] private Vector2 offset2;

    private Vector2 climbBegunPos;
    private Vector2 climOverPos;
    private bool canGrabledge = true;
    [HideInInspector]private bool canClimb;
    [HideInInspector] public bool ledgeDetected; //grab


    [Header("Collision Info")]
    [SerializeField] private float groundCheckDistance;
    [SerializeField]private float cellingCheckDistance;
    [SerializeField] private LayerMask whatIsGround;
    [SerializeField] private Transform wallCheck;
    [SerializeField] private Vector2 wallCheckSize;
    private  bool isGrounded;
    private bool wallDetected;
   
    

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        speedMilestone = milestoneIncreaser;
        sr = GetComponent<SpriteRenderer>();

       // playerPosition = GameObject.FindGameObjectWithTag("Player").transform;
        defaultSpeed = moveSpeed;
        speedMilestone = milestoneIncreaser;
        defaultMilestoneIncreaser = milestoneIncreaser;

        canGrabledge = true;
        
     
    }


   


    // Update is called once per frame
    void Update()
    {
        if(GameManager.Instance.Bullet < 0 )
        {
            canShoot = false;
        }
        
        else if(GameManager.Instance.Bullet > 0 )
        {
            canShoot = true;
        }



        if (isDead)
            return;

        if(extraLife = moveSpeed >= speedToSurvive)
        {
            extraLife = true;
        }

        if (playerUnlocked && !wallDetected)
            Movement();


        else if (wallDetected)
        { speedReset(); }

        if (isKnocked)
            return;

        /* if (Input.GetKeyDown(KeyCode.K)) 
             knockback();

         if (Input.GetKeyDown(KeyCode.O) & !isDead)
             StartCoroutine (Die());*/


        AnimatorController();

        slideTimeCounter -= Time.deltaTime;

        slidecooldowncounter -= Time.deltaTime;


        if (isGrounded)
        {
            canDoubleJump = true;
        }


        CheckInput();

        CkeckLand();
        CheckCollision();

        speedController();
        CheckForSlideCancel();
        CheckForLedge();
        Shoot();

    }

    private void Shoot()
    {
        if (isGrounded && canShoot)
        {
          if(Input.GetKey(KeyCode.E))
            {
                ShootInput();

                /* float distanceToPlayer = Vector2.Distance(Bullet.transform.position, playerPosition.position);

                 if( distanceToPlayer > maxDistance)
                 {
                     Debug.Log("Distance Reached");
                     Destroy(Bullet);
                 }*/

            }

        }
    }

    public void ShootInput()
    {
        GameManager.Instance.Bullet--;
        AudioManager.instance.PlaySFX(5);
        GameObject Bullet = Instantiate(bulletPrefab, shootPoint.position, Quaternion.identity, shootPoint);
        Rigidbody2D bulletrb = Bullet.GetComponent<Rigidbody2D>();
        bulletrb.AddForce(shootPoint.right * bulletSpeed, ForceMode2D.Impulse);

        Destroy(Bullet, 3f);
    }

    private void CkeckLand()
    {
        if (rb.velocity.y < -5 && !isGrounded)
        {
            readyToLand = true;
        }

        if (readyToLand && !isGrounded)
        {
            //DustFX.Play();
            readyToLand = false;
        }
    }

    public void Damage()
    {

        BloodFX.Play();
       if (extraLife == true)
        {
           knockback();

        }
       
        else 
            StartCoroutine (Die()); 
    }

    protected IEnumerator Die() //Courotine 
    {
        AudioManager.instance.PlaySFX(4);
        
        isDead = true;
        canBeKnock = false;
        rb.velocity = knockBackDir;
        anim.SetBool("isDead", true);

        Time.timeScale = .6f;

        yield return new WaitForSeconds(1);
        rb.velocity = new Vector2(0, 0);
        GameManager.Instance.GameEnded();
        

    }

    private IEnumerator Incivincibility() //coroutine to wait for the second to perform the nxt line
    {

        Color orginalColour = sr.color;
        Color darkenColor = new Color (sr.color.r, sr.color.g, sr.color.b,.5f);


        canBeKnock = false; //true in default
        sr.color = orginalColour;
        yield return new  WaitForSeconds(.1f);

        sr.color = darkenColor;
        yield return new WaitForSeconds(.15f);

        sr.color = orginalColour;
        yield return new WaitForSeconds(.25f);

        sr.color = darkenColor;
        yield return new WaitForSeconds(.35f);

        sr.color = orginalColour;
        yield return new WaitForSeconds(.45f);

        sr.color = darkenColor;
        yield return new WaitForSeconds(.55f);

        sr.color = orginalColour;
        yield return new WaitForSeconds(.65f);
        canBeKnock = true;

    }

    public void knockback() //if knocked coroutine will start invicicbility 
    {
        if (!canBeKnock) //if false wait for 5 second to konck again
            return;
       
        StartCoroutine(Incivincibility());


        isKnocked = true;
        rb.velocity = knockBackDir;
        speedReset();
        knockOver();
    }

    private void knockOver() => isKnocked = false;

    private void speedReset()
    {
        if(isSliding)
        {
            return;
        }
        moveSpeed = defaultSpeed;
        //defaultMilestoneIncreaser = milestoneIncreaser;

        milestoneIncreaser = defaultMilestoneIncreaser;

            
    }


    private void speedController()
    {
        if (moveSpeed == maxSpeed) 
            return;

        if (transform.position.x > speedMilestone) //player position if speedmilestone greater then 
        {
            speedMilestone = speedMilestone + milestoneIncreaser;
           
            moveSpeed = moveSpeed * speedmultipler; //speed ==  to maxspped into speedmultitpler

            milestoneIncreaser = milestoneIncreaser * speedmultipler;  //for increseing speedmilstone

            if(moveSpeed > maxSpeed)
                moveSpeed = maxSpeed;
        }
    }

    private void CheckForLedge()
    {
        if (ledgeDetected && canGrabledge) //cangrab default true value  setted
        {
            canGrabledge = false;//cannot climb second time

            rb.gravityScale = 0;

            Vector2 ledgePosition = GetComponentInChildren<ledgeDetection>().transform.position; //finding ledge detection child object in hierarchy //ledgeDetection object to find the position for offset1(climbBegunPosition)

            climbBegunPos = ledgePosition + offset1; //climb pos
            climOverPos = ledgePosition + offset2; //climb over

            canClimb = true; //ledge detected so can grab the ledge 
        }

        if(canClimb)
            transform.position = climbBegunPos; //setting position of player  form offset1 
    }

    private void LedgeClimbOver() //called from animation event to set position of player after the climb
    {
        canClimb = false;
        rb.gravityScale = 5;
        transform.position = climOverPos;
        
        Invoke("AllowLedgeGrab", .1f); //so cannot grab second tym "bug"
    }

    private void AllowLedgeGrab() => canGrabledge = true; //for invoke the function... slight delay 

    private void Movement()
    {
        if (wallDetected) //wall detected stop the movement 
        {
            speedReset();            
           // return;
        }

        if (isSliding )
        {
            rb.velocity = new Vector2(slideSpeed, rb.velocity.y); //slidespeed
        }

        else
             rb.velocity = new Vector2(moveSpeed, rb.velocity.y); //if not slide normal movement speed 
                
    }

    private void CheckForSlideCancel()
    {
        if(slideTimeCounter < 0 && !cellingdetected) //slide time is 0 and celling not detected slide will bw done by given silde timer 
        {
           // JumpCancel();
            isSliding = false;  //default false means sliding begun 
        }
    }


    public void SlideButton()
    {
         if(isDead) return;

        if (rb.velocity.x != 0 && slidecooldowncounter < 0) //if player speed is 0 cannot do slide & slidecooldown time is greater then 0 cannot slide
        {
            DustFX.Play();
        isSliding = true; //sliding cannot be done 
        slideTimeCounter = slideTime; 
            slidecooldowncounter = slidecooldown;
            
        }
    }

    public void Jump()
    {
        if (isDead) return;

        isSliding = false; //if sliding jumping function will ot be happen 

        rollAnimFinesh();

        if (isGrounded )
        {
            DustFX.Play();
            AudioManager.instance.PlaySFX(Random.Range(1, 2));
         rb.velocity = new Vector2(rb.velocityX, jumpForce);
            
        }

        else if(canDoubleJump)
        {
            AudioManager.instance.PlaySFX(Random.Range(1, 2));
            canDoubleJump = false; //canDoubleJump default has been set true
            rb.velocity = new Vector2(rb.velocityX, doubleJumpForce);
           
            
        }
    }

   /* private void JumpCancel()
    {
        if (!cellingdetected)
        {
          isGrounded = false;
        }
    }*/
    private void CheckInput()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        }

       /* if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            playerUnlocked = true;
        }*/


        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            SlideButton();
        }
    }
    private void AnimatorController()
    {
        anim.SetBool("canDoubleJump", canDoubleJump);
        anim.SetBool("isGrounded", isGrounded);
        anim.SetBool("isSliding", isSliding);
        anim.SetFloat("xVelocity",rb.velocity.x);
        anim.SetFloat("yVelocity", rb.velocity.y);
        anim.SetBool("canClimb", canClimb);
        anim.SetBool("isKnocked", isKnocked);

        if(rb.velocity.y < -20)
        {
            anim.SetBool("canRoll", true);
        }
    }

    private void rollAnimFinesh()
    {
        anim.SetBool("canRoll", false);
    } //access for animation.

    private void CheckCollision()
    {
        isGrounded = Physics2D.Raycast(transform.position, Vector2.down, groundCheckDistance, whatIsGround);
        cellingdetected = Physics2D.Raycast(transform.position,Vector2.up,cellingCheckDistance, whatIsGround);
        wallDetected = Physics2D.BoxCast(wallCheck.position, wallCheckSize, 0, Vector2.zero, 0, whatIsGround);

       
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(transform.position, new Vector2(transform.position.x, transform.position.y - groundCheckDistance));
        Gizmos.DrawLine(transform.position,new Vector2(transform.position.x,transform.position.y + groundCheckDistance));
        Gizmos.DrawWireCube(wallCheck.position, wallCheckSize);

    }





}
