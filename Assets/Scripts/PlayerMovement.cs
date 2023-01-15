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
    private float rotatespeed = 100.0f;
    [SerializeField]
    private float jumpheight = 10.0f;

    [SerializeField] 
    private float fallMultiplier = 2.5f;
    [SerializeField] 
    private float lowJumpMultiplier = 2f;

    private Vector3 movedirection, camerarelative;
    private int midairjumps = 5;

    private bool canjump;
    private bool canmidairjump;

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

        rb.velocity = ProcessMovement();

        if(rb.velocity.y < 0)
        {
            rb.velocity += Vector3.up * Physics.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
        }else if(rb.velocity.y > 0 && !Input.GetButton("Jump"))
        {
            rb.velocity += Vector3.up * Physics.gravity.y * (lowJumpMultiplier - 1) * Time.deltaTime;
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
        canjump = Input.GetButtonDown("Jump") && ground.IsGrounded();
        canmidairjump = Input.GetButtonDown("Jump") && !ground.IsGrounded() && midairjumps > 0;
        
        //movedirection = new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical")).normalized * movespeed + Vector3.up * rb.velocity.y;
        camerarelative = Input.GetAxis("Horizontal") * cam.transform.right + Input.GetAxis("Vertical") * cam.transform.forward;
        movedirection =  new Vector3(camerarelative.x, 0f, camerarelative.z).normalized * movespeed + Vector3.up * rb.velocity.y;
    
        ProcessRotation(movedirection);

        if (canjump)
        {
            movedirection.y += jumpheight;

        }else if (canmidairjump)
        {
            movedirection.y += jumpheight;
            midairjumps --;
        }


        return movedirection;
    }

    private void ProcessRotation(Vector3 dir)
    {
        if (dir != Vector3.zero)
        {
            Quaternion angle = Quaternion.LookRotation(dir, Vector3.up);
            rb.MoveRotation(angle);
            
        }
    }

    private void ReplenishJumps()
    {
        midairjumps = stats.GetCoinTotal();
    }

    public void ResetPlayer()
    {
        GameManager.Instance.DeathTransition();

        new WaitForSeconds(1f);

        transform.position = LevelStart.position;

    }


}
