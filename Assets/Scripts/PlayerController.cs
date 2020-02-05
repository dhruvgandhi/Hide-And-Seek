﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(PlayerMotor))]
public class PlayerController : MonoBehaviour
{
   [SerializeField]
   private float speed = 5f;

   private PlayerMotor motor;
   void Start()
   {
       motor = GetComponent<PlayerMotor>();
   }
   void Update()
   {
       //Calculate Movemnent Velocity as 3D Vector
       float _xMov = Input.GetAxisRaw("Horizontal");
       float _zMov = Input.GetAxisRaw("Vertical");
       Vector3 _movHorizontal = transform.right * _xMov;
       Vector3 _movVertical = transform.forward * _zMov;
       //Final Movemnt vector
       Vector3 _velocity = (_movHorizontal + _movVertical).normalized * speed;
       //ApplyVelocity
       motor.Move(_velocity);

       
   }
}
