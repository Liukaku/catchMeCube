using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpManager : MonoBehaviour
{
    // Update is called once per frame
    void FixedUpdate()
    {
        transform.Rotate(Vector3.right * 60 * Time.deltaTime);
        transform.Rotate(Vector3.up * 30 * Time.deltaTime);
    }
}
