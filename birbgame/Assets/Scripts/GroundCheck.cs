using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// object with this should be positioned at the player's feet
public class GroundCheck : MonoBehaviour
{
    [SerializeField] float checkRadius = .1f;
    [SerializeField] LayerMask groundLayer;

    // returns true if the object intersects with any other object in the "ground" layer
    public bool IsGrounded()
    {
        return Physics.CheckSphere(transform.position, checkRadius, groundLayer);
    }
}
