using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CartController : MonoBehaviour
{
    public float initialVelocity;
    public float finalVelocity;

    public static bool collisionFlag = false;

    private GameObject cart;
    private Rigidbody rbCart;

    private void Start()
    {
        cart = this.gameObject;
        rbCart = cart.GetComponent<Rigidbody>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        finalVelocity = rbCart.velocity.x;

        collisionFlag = true;
    }

    private void FixedUpdate()
    {
        if (collisionFlag == false)
            initialVelocity = rbCart.velocity.x;
    }
}
