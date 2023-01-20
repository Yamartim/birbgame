using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// main player class that defines movement
public class PlayerMovement : MonoBehaviour
{

#region attributes

    // components in this object
    private Rigidbody rb;
    private PlayerStats stats;
    private PlayerAnimation anim;
    private GroundCheck ground;
    private AudioSource flapSound;
    private ParticleSystem dustFx;
    private ParticleSystem.MainModule dustMain;
    private ParticleSystem.ShapeModule dustShape;

    // physics parameters
    [Header("Physics Parameters")]
    [SerializeField] private float moveSpeed = 10.0f;
    [Space]
    [SerializeField] private float rotateSpeed = 10.0f;
    [SerializeField] private float rotationLimit = .6f;
    [Space]
    [SerializeField] private float jumpHeight = 10.0f;
    [SerializeField] private float glideSpeed = -1.0f;
    [Space]
    [SerializeField] private float fallMultiplier = 2.5f;
    [SerializeField] private float lowJumpMultiplier = 2f;
    private Vector3 inputVector, moveDirection;
    private Quaternion camAngle;

    private int midairJumps = 5;

    // inputs and conditionals
    private float xInput, yInput;
    private bool jumpInput, pauseInput, falling, grounded;
    

    public bool isPaused { get; private set;} = false;

    // external references
    [Header("External References")]
    [SerializeField] private GameObject pausemenu;
    [SerializeField] private Transform LevelStart;
    private Camera cam;

#endregion
    

    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
        stats = gameObject.GetComponent<PlayerStats>();
        anim = gameObject.GetComponent<PlayerAnimation>();
        flapSound = gameObject.GetComponent<AudioSource>();
        ground = gameObject.GetComponentInChildren<GroundCheck>();
        cam = Camera.main;
        dustFx = gameObject.GetComponentInChildren<ParticleSystem>();
        dustMain = dustFx.main;
        dustShape = dustFx.shape;

        isPaused = false;
        ReplenishJumps();
    }

    // Update is called once per frame
    void Update()
    {
        //capturing inputs
        xInput = Input.GetAxis("Horizontal");
        yInput = Input.GetAxis("Vertical");
        jumpInput = Input.GetButtonDown("Jump");
        pauseInput = Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.P);
        falling = rb.velocity.y < 0;
        grounded = ground.IsGrounded();

        if(pauseInput)
            TogglePause();
        
        if(!isPaused)
        {
            ProcessMovement();
            
            ProcessJump();

            ProcessGlide();

            AdjustFall();

        }

    }


    // allows the player to jump many times again when the ground is touched
    private void OnCollisionEnter(Collision other) {
        if(grounded)
        {
            ReplenishJumps();
        }
    }

    private void ReplenishJumps()
    {
        midairJumps = stats.coinscollected;
    }

    // return to begining functionality
    public void ResetPlayer()
    {
        rb.velocity = Vector3.zero;
        transform.position = LevelStart.position;
    }

    // pause functionality
    public void TogglePause()
    {
        if (Time.timeScale > 0) 
        {
            Time.timeScale = 0;
            isPaused = true;
            pausemenu.SetActive(true);
        } else if(Time.timeScale == 0) {
            Time.timeScale = 1;
            isPaused = false;
            pausemenu.SetActive(false);
        }
    }

#region movement methods

    // determines player movement with input and perspective of the camera
    private void ProcessMovement()
    {
        //attempt to solve a bug
        //float camEuler = Camera.main.transform.eulerAngles.y;

        //normalized for the speed to be constant, neutralizing the y rotation
        inputVector = new Vector3(xInput, 0f, yInput).normalized;

        //getting camera angle for relative movement
        camAngle = Quaternion.Euler(0f, cam.transform.eulerAngles.y, 0f);

        //final direction vector
        moveDirection = camAngle * inputVector;


        //turning direction and speed into movement without affecting vertical velocity
        rb.velocity = moveDirection*moveSpeed + Vector3.up*rb.velocity.y;
        //alternative:
        //rb.velocity = new Vector3(moveDirection.x*moveSpeed, rb.velocity.y, moveDirection.z*moveSpeed);

        ProcessRotation(moveDirection);
        MovementParticles();
    }

    // rotates player towards direction they're moving to
    private void ProcessRotation(Vector3 dir)
    {
        Quaternion angle, angleVelocity;

        bool isMoving = (dir.x != 0f || dir.z != 0f) && dir != Vector3.zero && (xInput != 0f || yInput != 0);

        dir.y *= rotationLimit;

        if (isMoving)
        {
            angle = Quaternion.LookRotation(dir, Vector3.up);

            angleVelocity = Quaternion.Euler(Vector3.up * rotateSpeed * Time.deltaTime);

            rb.MoveRotation(angle*angleVelocity);
            
        }
    }

    // jumping and double jumping functionality
    private void ProcessJump()
    {
        bool jump = jumpInput && grounded;
        bool midairJump = jumpInput && !grounded && midairJumps > 0;

        if (jump)
        {
            rb.velocity += Vector3.up * jumpHeight;
            anim.JumpAnim();
            JumpParticles();
            flapSound.Play();
        }
        else if (midairJump)
        {
            // normally id put these tree repeated lines in a function but i wanted to add changes to the jump physics and animations eventually so I chose to leave it hear to be easy to change later
            rb.velocity += Vector3.up * jumpHeight;
            anim.JumpAnim();
            JumpParticles();
            flapSound.Play();
            midairJumps--;
        }
    }

    // allows the player to glide by pressing the spacebar
    private void ProcessGlide()
    {
        bool glideInput = Input.GetButton("Jump");

        if (glideInput && falling && !jumpInput) 
        {

            rb.useGravity = false;
            rb.velocity = new Vector3(rb.velocity.x, glideSpeed, rb.velocity.z);
            anim.GlideAnim(true);

        } else {

            rb.useGravity = true;
            anim.GlideAnim(false);

        }

    }

    // physics adjustment so that jumping feels more responsive to player input
    private void AdjustFall()
    {
        bool highJump = rb.velocity.y > 0 && !Input.GetButton("Jump");

        if (falling)
        {

            rb.velocity += Vector3.up * Physics.gravity.y * (fallMultiplier - 1) * Time.deltaTime;

        }
        else if (highJump)
        {

            rb.velocity += Vector3.up * Physics.gravity.y * (lowJumpMultiplier - 1) * Time.deltaTime;

        }
    }

#endregion


#region particle methods

    private void MovementParticles()
    {
        if((xInput != 0 || yInput != 0) && grounded && !dustFx.isPlaying )
        {
            dustFx.Play();

        } else 
        {
            dustFx.Stop();
        }
    }

    private void JumpParticles()
    {
        if (jumpInput && grounded){
            dustMain.startSpeed = 2f;
            dustShape.randomDirectionAmount = 1f;
            //ParticleSystem.EmitParams jumpemit = new ParticleSystem.EmitParams();
            //jumpemit.velocity = Vector3.forward * 5f;
            
            //dustFx.Emit(jumpemit, 20);
            dustFx.Play();
        } else
        {
            dustMain.startSpeed = .4f;
            dustShape.randomDirectionAmount = .1f;
        }
    }
    
#endregion


}
