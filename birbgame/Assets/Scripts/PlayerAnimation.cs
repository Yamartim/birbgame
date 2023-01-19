using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// class that takes care of animation stuff for the player
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

#region set animator parameters

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

#endregion

}
