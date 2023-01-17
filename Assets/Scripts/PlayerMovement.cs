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
    private float movespeed = 10.0f;

    [SerializeField]
    private float rotatespeed = 10.0f;
    [SerializeField]
    private float rotationlimit = .6f;
    [SerializeField]
    private float jumpheight = 10.0f;
    [SerializeField]
    private float glidespeed = -1.0f;

    [SerializeField] 
    private float fallMultiplier = 2.5f;
    [SerializeField] 
    private float lowJumpMultiplier = 2f;

    private Vector3 movedirection, camerarelativeangle;
    private int midairjumps = 5;

    private float xinput, yinput;
    private bool ismoving, falling, jumpinput, jump, highjump, midairjump, glideinput, pauseinput;

    public bool ispaused { get; private set;} = false;
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
        xinput = Input.GetAxis("Horizontal");
        yinput = Input.GetAxis("Vertical");
        jumpinput = Input.GetButtonDown("Jump");
        pauseinput = Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.P);
        falling = rb.velocity.y < 0;

        if(pauseinput)
            TogglePause();
        
        if(!ispaused)
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
        camerarelativeangle = xinput * cam.transform.right + yinput * cam.transform.forward;

        movedirection = new Vector3(camerarelativeangle.x, 0f, camerarelativeangle.z).normalized * movespeed;

        rb.velocity = movedirection + Vector3.up * rb.velocity.y;

        ProcessRotation(movedirection);
        
    }

    private void ProcessRotation(Vector3 dir)
    {
        ismoving = (dir.x != 0f || dir.z != 0f) && dir != Vector3.zero;

        dir.y *= rotationlimit;

        if (ismoving){
            Quaternion angle = Quaternion.LookRotation(dir, Vector3.up);

            Quaternion angvelocity = Quaternion.Euler(Vector3.up*rotatespeed*Time.deltaTime);

            rb.MoveRotation(angle*angvelocity);
            
        }
    }

    private void ProcessJump()
    {
        jump = jumpinput && ground.IsGrounded();
        midairjump = jumpinput && !ground.IsGrounded() && midairjumps > 0;

        if (jump)
        {
            rb.velocity += Vector3.up * jumpheight;
            anim.JumpAnim();
        }
        else if (midairjump)
        {
            rb.velocity += Vector3.up * jumpheight;
            anim.JumpAnim();
            midairjumps--;
        }
    }

    private void ProcessGlide()
    {
        glideinput = Input.GetButton("Jump");

        if (glideinput && falling) {

            rb.mass = 0f;
            rb.velocity = new Vector3(rb.velocity.x, glidespeed, rb.velocity.z);
            anim.GlideAnim(true);

        } else {

            rb.mass = 1f;
            anim.GlideAnim(false);

        }

    }

    private void AdjustFall()
    {
        highjump = rb.velocity.y > 0 && !Input.GetButton("Jump");

        if (falling)
        {

            rb.velocity += Vector3.up * Physics.gravity.y * (fallMultiplier - 1) * Time.deltaTime;

        }
        else if (highjump)
        {

            rb.velocity += Vector3.up * Physics.gravity.y * (lowJumpMultiplier - 1) * Time.deltaTime;

        }
    }

    private void ReplenishJumps()
    {
        midairjumps = stats.coinscollected;
    }

    public void ResetPlayer()
    {
        transform.position = LevelStart.position;
    }

    public void TogglePause()
    {
        if (Time.timeScale > 0) {
            Time.timeScale = 0;
            ispaused = true;
            pausemenu.SetActive(false);
        } else if(Time.timeScale == 0) {
            Time.timeScale = 1;
            ispaused = false;
            pausemenu.SetActive(true);
        }
    }


}
