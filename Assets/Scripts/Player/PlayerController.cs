using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private LayerMask ground;
    private Vector3 startLocation;
    public Rigidbody2D body;
    private SpriteRenderer spriteRenderer;
    public Animator anim;
    private BoxCollider2D melee;
    private AudioSource[] audio;

    // Player attributes
    public int currentHealth;
    private float moveSpeed = 3F;
    private float jumpForce = 7;

    private GameObject groundPoint1;
    private GameObject groundPoint2;
    private GameObject groundPoint3;

    // State
    public bool grounded;
    private bool invincible;
    public bool canFire;
    private bool scopeIsCool = true;
    private float scopeCooldownTime = 10;
    private Animator HUDScope;
    private bool canMove;
    public float facing;
    public bool alive;
    private bool hurt = false;
    private int currentLevel;

    Color fullBrightness = new Color(1f, 1f, 1f, 1f);
    Color noBrightness = new Color(1f, 1f, 1f, 0f);

    // Use this flag to set an attack (melee) as cancelled/interrupted if the player takes damage
    // before executing the melee attack fully
    private bool cancelAttack = false;
    public bool hasPowerglove = false;
    public bool hasZapper = false;
    public bool hasSuperscope = false;

    public bool debug;

    // Use this for initialization
    void Start()
    {
        // If the level manager isn't present (most likely during dev/debug), spawn it and restart level
        if (!LevelManager.Instance)
        {
            Instantiate(Resources.Load("LevelManager"));
            LevelManager.Instance.gameStarted = true;
            LevelManager.Instance.updateCurrentLevel();
            //LevelManager.Instance.RestartLevel();

        }
        // Combine ground and jumpthrough layers into a binary layermask
        // For detecting if player IsGrounded();
        int groundLayer = 8;
        int jumpthroughLayer = 13;
        ground = (1 << groundLayer) | (1 << jumpthroughLayer);
            
        startLocation = transform.position;
        groundPoint1 = GameObject.Find("groundPoint1");
        groundPoint2 = GameObject.Find("groundPoint2");
        groundPoint3 = GameObject.Find("groundPoint3");

        // HUD
        HUDScope = GameObject.Find("HUDScope").GetComponent<Animator>();
        audio = GetComponents<AudioSource>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        body = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        melee = GameObject.Find("melee").GetComponent<BoxCollider2D>();
        melee.enabled = false;
        grounded = IsGrounded();      
        invincible = false;
        canFire = true;
        facing = 1;
        canMove = true;
        alive = true;
        currentHealth = 3;

        if (debug)
        {
            scopeCooldownTime = 0;
            setLevel(3);
        }
    }
	
    // Update is called once per frame

    void FixedUpdate()
    {
        // Flicker the player sprite when invincible
        if (invincible)
        {
            if (spriteRenderer.color.a == 1)
            {
                spriteRenderer.color = noBrightness;
            }
            else
            {
                spriteRenderer.color = fullBrightness;
            }
        }
        else
        {
            spriteRenderer.color = fullBrightness;
        }
    }

    void Update()
    {
        grounded = IsGrounded();

        // Enable jumpthrough platforms by disabled collissions when player is jumping
        // Layer 10 = player
        // Layer 13 = jumpthrough
        if (body.velocity.y >= 0.05f)
        {
            Physics2D.IgnoreLayerCollision(13, 10, true);
        }
        else
        {
            Physics2D.IgnoreLayerCollision(13, 10, false);
        }


        if (hurt)
        {
            body.velocity = new Vector2((2 * -facing), 1f);
        }
        else if (canMove)
        {
            if (Input.GetKey("d") || Input.GetKey(KeyCode.RightArrow))
            {
                Move(1);
            }
            else if (Input.GetKey("a") || Input.GetKey(KeyCode.LeftArrow))
            {
                Move(-1);
            }
            else
            {
                Move(0);
            }

            // Jump       
            if ((Input.GetKeyDown("w") || Input.GetKeyDown("space") || Input.GetKeyDown(KeyCode.UpArrow)) && grounded)
            {
                audio[1].Play();
                body.velocity = new Vector2(body.velocity.x, jumpForce);
            }
                
            // Weapons
            if (hasPowerglove)
            {
                if (Input.GetKey("j") && canMove && grounded)
                {
                    UseMelee();
                }
            }
            if (hasZapper)
            {
                if (Input.GetKey("k") && canFire)
                {
                    FireZapper();
                }
            }



            if (hasSuperscope)
            {
                if (Input.GetKey("l") && canFire && scopeIsCool)
                {
                    FireSuperScope();
                }
            }
        }
    }

    // Move the player
    private void Move(float h)
    {
        if (Mathf.Abs(h) > 0)
        {
            body.constraints = RigidbodyConstraints2D.FreezeRotation;
            anim.SetBool("run", true);
            Vector3 scale = transform.localScale;
            body.velocity = new Vector2(moveSpeed * Mathf.Sign(h), body.velocity.y);
            if (body.velocity.x > 0)
            {
                scale.x = Mathf.Abs(scale.x);
                transform.localScale = scale;
                facing = 1;
            }
            else if (body.velocity.x < 0)
            {
                scale.x = Mathf.Abs(scale.x) * -1;
                transform.localScale = scale;
                facing = -1;
            }
        }
        else
        {
            anim.SetBool("run", false);
            if (grounded)
            {
                body.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezeRotation;
            }
            body.velocity = new Vector2(0, body.velocity.y);
        }
    }

    // Returns true if character is on the ground
    public bool IsGrounded()
    {
        if ((Physics2D.Raycast(groundPoint1.transform.position, Vector2.down, 0.2f, ground.value)
            || Physics2D.Raycast(groundPoint2.transform.position, Vector2.down, 0.2f, ground.value)
            || Physics2D.Raycast(groundPoint3.transform.position, Vector2.down, 0.2f, ground.value)) && Mathf.Abs(body.velocity.y) < 4f)
        {
            anim.SetBool("grounded", true);
            return true;
        }
        else
        {
            anim.SetBool("grounded", false);
            return false;
        }
    }

    // Take damage, die if health goes to 0
    public void takeDamage()
    {
        if (!invincible)
        {
            audio[2].Play();
            // Interrupt an in-progress melee attack
            cancelAttack = true;
            currentHealth -= 1;
            canMove = false;
            Stop();

            // TODO: Should create a canMoveTimer to prevent this from reducing move time wait
            // e.g. if canMoveTimer < 1, canMoveTimer=1
            if (currentHealth <= 0)
            {
                die();
            }
            else
            {
                Invoke("resetMove", 1);
                knockback();
            }
        }
    }

    // Reduce lives, respawn if you have lives
    private void die()
    {
        Stop();
        body.simulated = false;
        anim.SetTrigger("die");
        canMove = false;
        audio[3].Play();
        audio[4].PlayDelayed(2);
        Invoke("setAliveFalse", 3);
    }

    void setAliveFalse()
    {
        alive = false;
    }

    // Called after iframes expire
    private void resetInvincibility()
    {
        invincible = false;

        // Reset the attack interrupt flag
        cancelAttack = false;
    }

    // Player is knocked backwards upon collidding with an enemy
    private void knockback()
    {
        anim.SetTrigger("knockback");
        hurt = true;
        Invoke("resetHurt", .25f);
        invincible = true;
        Invoke("resetInvincibility", 3);
    }

    private void resetHurt()
    {
        Stop();
        hurt = false;
    }

    // Attack methods
    public void UseMelee()
    {
        Stop();
        body.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezeRotation;
        canMove = false;
        anim.SetTrigger("melee");

    }

    public void FireZapper()
    {
        // Spawn an instance of the bullet prefab
        Instantiate(Resources.Load("fireball"));
        canFire = false;
        anim.SetTrigger("zap");
    }

    public void FireSuperScope()
    {
        HUDScope.SetTrigger("cooldown");
        scopeIsCool = false;
        Invoke("resetScopeCool", scopeCooldownTime);
        Instantiate(Resources.Load("superScopeShot"));
        canFire = false;
        anim.SetTrigger("scope");
    }

    public void Stop()
    {
        body.velocity = Vector2.zero;
    }

    public void resetFire()
    {
        canFire = true;  
    }

    public void resetMove()
    {
        canMove = true;  
    }

    public void startResetFireTimer(float delay)
    {
        Invoke("resetFire", delay);
    }

    // Reset the cooldown for the scope shot
    public void resetScopeCool()
    {
        scopeIsCool = true;
    }


    private void setMelee()
    {
        // Only enable the melee collider if the attack hasn't been interrupted
        if (!cancelAttack)
        {
            audio[0].Play();
            melee.enabled = true;
        }
        else
        {
            cancelAttack = false;
        }
    }

    private void resetMelee()
    {
        resetMove();
        body.constraints = RigidbodyConstraints2D.FreezeRotation;
        melee.enabled = false;
    }

    public void collectItem(float t)
    {
        LevelManager.Instance.playerIsCollectingItem = true;
        body.constraints = RigidbodyConstraints2D.FreezeRotation | RigidbodyConstraints2D.FreezePositionX;
        canMove = false;
        anim.SetBool("item", true);
    }

    public void resetItem()
    {
        LevelManager.Instance.playerIsCollectingItem = false;
        body.constraints = RigidbodyConstraints2D.FreezeRotation;
        anim.SetBool("item", false);
        resetMove();
    }

    // Player should repeatedly take damage from hazards if standing on/in them
    void OnCollisionStay2D(Collision2D col)
    {
        if (col.transform.tag == "Hazard")
        {
            takeDamage();
        }
    }

    // Die from falling in a pit
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.name == "abyss")
        {
            die();
        }
    }

    public void setLevel(int l)
    {
        currentLevel = l;
        if (l == 1)
        {
            hasPowerglove = true;
            anim.runtimeAnimatorController = Resources.Load("PlayerNoZapper") as RuntimeAnimatorController;
        }
        else if (l == 2)
        {
            anim.runtimeAnimatorController = Resources.Load("Player") as RuntimeAnimatorController;
            hasPowerglove = true;
            hasZapper = true;
        }
        else if (l == 3)
        {
            HUDScope = GameObject.Find("HUDScope").GetComponent<Animator>();
            anim.runtimeAnimatorController = Resources.Load("Player") as RuntimeAnimatorController;
            hasPowerglove = true;
            hasZapper = true;
            hasSuperscope = true;
            HUDScope.SetBool("hasScope", true);
        }
    }

    public void Disappear()
    {
        GameObject.Find("ExitDoor").GetComponent<SpriteRenderer>().sortingLayerName = "Infinity";
        Stop();
        body.constraints = RigidbodyConstraints2D.FreezeAll;
        spriteRenderer.sortingLayerName = "Infinity";
        spriteRenderer.sortingOrder = 100;
        Invoke("DestroyTheWorld", 1.5f);

    }

    public void DestroyTheWorld()
    {
        GameObject.Find("Level99").SetActive(false);
        GameObject.Find("LevelNotifications").SetActive(false);
        LevelManager.Instance.Invoke("ShowCredits", 3);
    }

    // At the end of the level, publish any relevant state details to the Level Manager
    public void UpdateState()
    {
        LevelManager.Instance.playerLevel = currentLevel;

    }
}