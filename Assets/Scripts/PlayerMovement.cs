using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private float speed = 3;

    void Update()
    {
        if (Input.GetKey(KeyCode.A))
            transform.position += transform.right * speed * Time.deltaTime;
        else if (Input.GetKey(KeyCode.S))
            transform.position += -transform.up * speed * Time.deltaTime;
        else if (Input.GetKey(KeyCode.D))
            transform.position += -transform.right * speed * Time.deltaTime;
        else if (Input.GetKey(KeyCode.W))
            transform.position += transform.up * speed * Time.deltaTime;
    }
}
