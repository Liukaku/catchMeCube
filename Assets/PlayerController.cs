using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerController : MonoBehaviour
{
    public float speed;

    void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(horizontalInput, 0, verticalInput);

        if(horizontalInput != 0f && verticalInput != 0f)
        {
            movement = Vector3.ClampMagnitude(movement, 1);
            Debug.Log(movement);
        }

        transform.Translate(movement * speed * Time.deltaTime);
    }
}
