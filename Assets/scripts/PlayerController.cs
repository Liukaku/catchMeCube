using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Unity.VisualScripting;
using Unity;


public static class GameObjectEfx
{
    public static void DrawCircle(this GameObject container, float lineWidth, Material matRef)
    {
        int segments = 360;
        LineRenderer Line = container.AddComponent<LineRenderer>();
        Line.material = matRef;
        Line.useWorldSpace = false;
        Line.startWidth = lineWidth;
        Line.endWidth = lineWidth;
        Line.positionCount = segments + 1;

        var points = new Vector3[Line.positionCount];

        for (int i = 0; i < points.Length; i++)
        {
            var rad = Mathf.Deg2Rad * i;
            points[i] = new Vector3(Mathf.Cos(rad), 0, Mathf.Sin(rad));
        }

        Line.SetPositions(points);
    }

    public static void ChangeCircleMat(this GameObject container, Material matRef)
    {
        var Line = container.GetComponent<LineRenderer>();
        Line.material = matRef;
    }
}
public class PlayerController : MonoBehaviour
{
    public Quaternion PlayerRotation;
    public Camera followCamera;
    private Vector3 m_CameraPosition;

    public Material materialRefCol;
    public Material materialRefTrans;
    private Rigidbody m_rb;
    public float speed;
    public float speedModifier = 1;

    private GameObject m_Elevator;
    private float m_ElevatorOffsetY;

    float mDesiredRotation = 0f;

    private GameObject CircleWarn;
    private void Awake()
    {

        CircleWarn = new GameObject
        {
            name = "Circle"
        };

        CircleWarn.transform.parent = transform;

        Vector3 CirclePos = Vector3.zero;

        CircleWarn.transform.localPosition = CirclePos;
        CircleWarn.DrawCircle(0.5f, materialRefTrans);

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

      

        m_rb.MovePosition(playerPos + movement * speed * speedModifier * Time.fixedDeltaTime);
        if (horizontalInput + verticalInput != 0)
        {
            m_rb.MoveRotation(Quaternion.Lerp(currentRotation, targetRotation, 10 * Time.fixedDeltaTime));
        }

    }

    private void LateUpdate()
    {
        followCamera.transform.position = m_rb.position + m_CameraPosition;
    }

    private void OnCollisionEnter(Collision collision)
    {

        if (collision.gameObject.CompareTag("PowerUp"))
        {
            Debug.Log("power up!");
            Destroy(collision.gameObject);
            speedModifier = 2;
            CircleWarn.ChangeCircleMat(materialRefCol);
            StartCoroutine(SpeedCountDown());
        }

        if(collision.gameObject.CompareTag("EnemyTag") && speedModifier > 1)
        {
            Rigidbody enemyRb = collision.gameObject.GetComponent<Rigidbody>();
            Vector3 awayFromPlayer = collision.transform.position - transform.position;
            enemyRb.AddForce(awayFromPlayer * 30.0f, ForceMode.Impulse);
        }
    }

    private IEnumerator SpeedCountDown()
    {
        yield return new WaitForSeconds(4);
        speedModifier = 1;
        CircleWarn.ChangeCircleMat(materialRefTrans);
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
