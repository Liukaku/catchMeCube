using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerController : MonoBehaviour
{
    private Rigidbody m_rb;
    public float speed;

    private void Awake()
    {
        m_rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(horizontalInput, 0, verticalInput);

        if(horizontalInput != 0f && verticalInput != 0f)
        {
            movement = Vector3.ClampMagnitude(movement, 1);
        }


        m_rb.MovePosition(m_rb.position + movement * speed * Time.deltaTime);
    }
}
