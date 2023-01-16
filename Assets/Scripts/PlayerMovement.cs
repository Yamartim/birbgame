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
    private float fallMultiplier = 2.5f;
    [SerializeField] 
    private float lowJumpMultiplier = 2f;

    private Vector3 movedirection, camerarelative;
    private int midairjumps = 5;

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
        if(Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.P))
            TogglePause();
        
        if(!ispaused)
        {
            rb.velocity = ProcessMovement();

            if(rb.velocity.y < 0){

                rb.velocity += Vector3.up * Physics.gravity.y * (fallMultiplier - 1) * Time.deltaTime;

            } else if(rb.velocity.y > 0 && !Input.GetButton("Jump")){

                rb.velocity += Vector3.up * Physics.gravity.y * (lowJumpMultiplier - 1) * Time.deltaTime;

            }
        }

    }


    private void OnCollisionEnter(Collision other) {
        if(ground.IsGrounded())
        {
            ReplenishJumps();
        }
    }


    private Vector3 ProcessMovement()
    {
        bool willjump = Input.GetButtonDown("Jump") && ground.IsGrounded();
        bool willmidairjump = Input.GetButtonDown("Jump") && !ground.IsGrounded() && midairjumps > 0;
        
        //movedirection = new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical")).normalized * movespeed + Vector3.up * rb.velocity.y;
        camerarelative = Input.GetAxis("Horizontal") * cam.transform.right + Input.GetAxis("Vertical") * cam.transform.forward;
        movedirection =  new Vector3(camerarelative.x, 0f, camerarelative.z).normalized * movespeed + Vector3.up * rb.velocity.y;
    
        ProcessRotation(movedirection);

        if (willjump){
            movedirection.y += jumpheight;
            anim.JumpAnim();

        } else if (willmidairjump){
            movedirection.y += jumpheight;
            midairjumps --;
            anim.JumpAnim();
        }

        return movedirection;
    }

    private void ProcessRotation(Vector3 dir)
    {
        dir.y *= rotationlimit;

        if ((dir.x != 0f || dir.z != 0f) && dir != Vector3.zero){
            Quaternion angle = Quaternion.LookRotation(dir, Vector3.up);

            Quaternion angvelocity = Quaternion.Euler(Vector3.up*rotatespeed*Time.deltaTime);

            rb.MoveRotation(angle*angvelocity);
            
        }
    }

    private void ReplenishJumps()
    {
        midairjumps = stats.coinscollected;
    }

    public void ResetPlayer()
    {
        GameManager.Instance.DeathTransition();

        new WaitForSeconds(1f);

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
