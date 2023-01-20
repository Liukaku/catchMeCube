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
        speed = Random.Range(10, 30);
        m_player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 moveTowards = (m_player.transform.position - transform.position).normalized;
        moveTowards.y = 0;
        m_rb.AddForce(moveTowards * speed);
        if (transform.position.y <= -3)
        {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player") && m_player.GetComponent<PlayerController>().speedModifier == 1)
        {
            Rigidbody playerRb = collision.gameObject.GetComponent<Rigidbody>();
            Vector3 awayFromEnemy = collision.transform.position - transform.position;
            playerRb.AddForce(awayFromEnemy * 5.0f, ForceMode.Impulse);
        }
    }
}
