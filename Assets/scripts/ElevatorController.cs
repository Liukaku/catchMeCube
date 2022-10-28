using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElevatorController : MonoBehaviour
{
    private float m_travelDistance = 0;
    public float m_maxDistance = 15.0f;

    public float m_speed = 8.0f;

    private Rigidbody m_Rigidbody;

    private Coroutine m_reverseCorutine;


    // Start is called before the first frame update
    private IEnumerator Start()
    {
        m_Rigidbody = GetComponent<Rigidbody>();
        enabled = false;
        yield return new WaitForSeconds(3.0f);
        enabled = true;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(m_travelDistance >= m_maxDistance)
        {
            if(m_reverseCorutine == null)
            {
                m_reverseCorutine = StartCoroutine(nameof(ReverseElevator));
            }
        } else
        {
            float distanceStep = m_speed * Time.fixedDeltaTime;
            m_travelDistance += Mathf.Abs(distanceStep);

            Vector3 elevatorPos = m_Rigidbody.position;
            elevatorPos.y += distanceStep;

            m_Rigidbody.MovePosition(elevatorPos);
        }

    }

    private IEnumerator ReverseElevator()
    {
        yield return new WaitForSeconds(3.0f);
        m_travelDistance = 0;
        m_speed = -m_speed;
        m_reverseCorutine = null;
    }

}
