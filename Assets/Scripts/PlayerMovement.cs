using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody rb;
    private PlayerStats stats;
    private PlayerAnimation anim;
    private GroundCheck ground;

    [SerializeField]
    private float moveSpeed = 10.0f;

    [SerializeField]
    private float rotateSpeed = 10.0f;
    [SerializeField]
    private float rotationLimit = .6f;
    [SerializeField]
    private float jumpHeight = 10.0f;
    [SerializeField]
    private float glideSpeed = -1.0f;

    [SerializeField] 
    private float fallMultiplier = 2.5f;
    [SerializeField] 
    private float lowJumpMultiplier = 2f;

    private Vector3 moveDirection, cameraRelativeAngle;
    private int midairJumps = 5;

    private float xInput, yInput;
    private bool isMoving, falling, jumpInput, jump, highJump, midairJump, glideInput, pauseInput;

    public bool isPaused { get; private set;} = false;
    [SerializeField] 
    private GameObject pausemenu;


    [SerializeField] private Transform LevelStart;
    [SerializeField] private Camera cam;

    

    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
        stats = gameObject.GetComponent<PlayerStats>();
        anim = gameObject.GetComponent<PlayerAnimation>();
        ground = gameObject.GetComponentInChildren<GroundCheck>();

        ReplenishJumps();
    }

    // Update is called once per frame
    void Update()
    {
        xInput = Input.GetAxis("Horizontal");
        yInput = Input.GetAxis("Vertical");
        jumpInput = Input.GetButtonDown("Jump");
        pauseInput = Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.P);
        falling = rb.velocity.y < 0;

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

    private void OnCollisionEnter(Collision other) {
        if(ground.IsGrounded())
        {
            ReplenishJumps();
        }
    }


    private void ProcessMovement()
    {
        cameraRelativeAngle = xInput * cam.transform.right + yInput * cam.transform.forward;

        moveDirection = new Vector3(cameraRelativeAngle.x, 0f, cameraRelativeAngle.z).normalized * moveSpeed;

        rb.velocity = moveDirection + Vector3.up * rb.velocity.y;

        ProcessRotation(moveDirection);
        
    }

    private void ProcessRotation(Vector3 dir)
    {
        isMoving = (dir.x != 0f || dir.z != 0f) && dir != Vector3.zero;

        dir.y *= rotationLimit;

        if (isMoving){
            Quaternion angle = Quaternion.LookRotation(dir, Vector3.up);

            Quaternion angvelocity = Quaternion.Euler(Vector3.up*rotateSpeed*Time.deltaTime);

            rb.MoveRotation(angle*angvelocity);
            
        }
    }

    private void ProcessJump()
    {
        jump = jumpInput && ground.IsGrounded();
        midairJump = jumpInput && !ground.IsGrounded() && midairJumps > 0;

        if (jump)
        {
            rb.velocity += Vector3.up * jumpHeight;
            anim.JumpAnim();
        }
        else if (midairJump)
        {
            rb.velocity += Vector3.up * jumpHeight;
            anim.JumpAnim();
            midairJumps--;
        }
    }

    private void ProcessGlide()
    {
        glideInput = Input.GetButton("Jump");

        if (glideInput && falling) {

            rb.mass = 0f;
            rb.velocity = new Vector3(rb.velocity.x, glideSpeed, rb.velocity.z);
            anim.GlideAnim(true);

        } else {

            rb.mass = 1f;
            anim.GlideAnim(false);

        }

    }

    private void AdjustFall()
    {
        highJump = rb.velocity.y > 0 && !Input.GetButton("Jump");

        if (falling)
        {

            rb.velocity += Vector3.up * Physics.gravity.y * (fallMultiplier - 1) * Time.deltaTime;

        }
        else if (highJump)
        {

            rb.velocity += Vector3.up * Physics.gravity.y * (lowJumpMultiplier - 1) * Time.deltaTime;

        }
    }

    private void ReplenishJumps()
    {
        midairJumps = stats.coinscollected;
    }

    public void ResetPlayer()
    {
        transform.position = LevelStart.position;
    }

    public void TogglePause()
    {
        if (Time.timeScale > 0) {
            Time.timeScale = 0;
            isPaused = true;
            pausemenu.SetActive(false);
        } else if(Time.timeScale == 0) {
            Time.timeScale = 1;
            isPaused = false;
            pausemenu.SetActive(true);
        }
    }


}
