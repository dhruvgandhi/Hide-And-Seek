using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Rigidbody))]
public class PlayerMotor : MonoBehaviour
{
    private Vector3 velocity = Vector3.zero;
    private Rigidbody rb;
    //For Jumping
    bool isGrounded=false;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }
    public void Move(Vector3 _velocity)
    {
        velocity = _velocity;
    }
    //For Jumping
    void OnCollisionEnter(Collision col)
    {
        if((col.gameObject.tag == "Ground") && !isGrounded)
            isGrounded = true;
    }
    void Update()
    {
        PerformMovement();
        //Jump;
       if(Input.GetButtonDown("Jump") && isGrounded)
       {
           rb.AddForce(new Vector3(0f,10f,0f),ForceMode.Impulse);
           isGrounded = false;
       }
    }
    //Perform Movement based on velocity variable
    void PerformMovement()
    {
        if(velocity != Vector3.zero)
        {
            rb.MovePosition(rb.position + velocity * Time.fixedDeltaTime);
        }

    }

}
