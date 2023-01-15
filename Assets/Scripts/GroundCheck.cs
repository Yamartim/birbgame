using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCheck : MonoBehaviour
{
    [SerializeField] LayerMask groundlayer;
    public bool IsGrounded()
    {
        return Physics.CheckSphere(transform.position, .2f, groundlayer);
    }
}
