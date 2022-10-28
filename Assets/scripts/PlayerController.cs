using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerController : MonoBehaviour
{
    public Quaternion PlayerRotation;
    public Camera followCamera;
    private Vector3 m_CameraPosition;

    private Rigidbody m_rb;
    public float speed;

    private GameObject m_Elevator;
    private float m_ElevatorOffsetY;

    float mDesiredRotation = 0f;
    private void Awake()
    {
        m_CameraPosition = followCamera.transform.position - transform.position;
        m_rb = GetComponent<Rigidbody>();

    }

    void FixedUpdate()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(horizontalInput, 0, verticalInput);
        Vector3 playerPos = m_rb.position;


        if(m_Elevator != null)
        {
            playerPos.y = m_Elevator.transform.position.y + m_ElevatorOffsetY;
        }

        if(horizontalInput != 0f && verticalInput != 0f)
        {
            movement = Vector3.ClampMagnitude(movement, 1);
        }


        Vector3 rotatedMovement = Quaternion.Euler(0, followCamera.transform.rotation.eulerAngles.y, 0) * movement;

        mDesiredRotation = Mathf.Atan2(rotatedMovement.x, rotatedMovement.z) * Mathf.Rad2Deg;

        Quaternion currentRotation = transform.rotation;
        Quaternion targetRotation = Quaternion.Euler(0, mDesiredRotation, 0);

      

        m_rb.MovePosition(playerPos + movement * speed * Time.fixedDeltaTime);
        if (horizontalInput + verticalInput != 0)
        {
            m_rb.MoveRotation(Quaternion.Lerp(currentRotation, targetRotation, 10 * Time.fixedDeltaTime));
        }
        //transform.rotation = Quaternion.Lerp(currentRotation, targetRotation, 10 * Time.fixedDeltaTime);
    }

    private void LateUpdate()
    {
        followCamera.transform.position = m_rb.position + m_CameraPosition;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Elevator"))
        {
            m_Elevator = other.gameObject;
            m_ElevatorOffsetY = transform.position.y - m_Elevator.transform.position.y;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Elevator"))
        {
            m_Elevator = null;
            Debug.Log("un-mounted the object");
        }
    }
}
