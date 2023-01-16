using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    private Animator anim;
    private Rigidbody rb;
    private GroundCheck gc;



    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
        gc = GetComponentInChildren<GroundCheck>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        anim.SetFloat("SPEED", new Vector3(rb.velocity.x, 0f, rb.velocity.z).magnitude);
        anim.SetFloat("JUMP_SPEED", rb.velocity.y);

        anim.SetBool("GROUNDED", gc.IsGrounded());
    }

    public void JumpAnim()
    {
        anim.SetTrigger("JUMP");
    }

    public void GlideAnim(bool state)
    {
        anim.SetBool("GLIDE", state);
    }
}
