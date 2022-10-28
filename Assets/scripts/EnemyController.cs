using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float speed;

    private Rigidbody m_rb;
    private GameObject m_player;

    private void Awake()
    {
        m_rb = GetComponent<Rigidbody>();   
    }
    // Start is called before the first frame update
    void Start()
    {
        m_player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 moveTowards = (m_player.transform.position - transform.position).normalized;
        m_rb.AddForce(moveTowards * speed);
    }
}
