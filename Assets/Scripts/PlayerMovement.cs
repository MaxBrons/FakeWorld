using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed = 3;
    private SpriteRenderer spr;
    private Vector3 rightPos, upPos;

    private void Awake()
    {
        spr = GetComponent<SpriteRenderer>();
        rightPos = new Vector3(.1f, 0, 0);
        upPos = new Vector3(0, .1f, 0);
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.A))
        {
            transform.position += -rightPos * speed * Time.deltaTime;
            spr.flipX = true;
        }
        if (Input.GetKey(KeyCode.S))
            transform.position += -upPos * speed * Time.deltaTime;
        if (Input.GetKey(KeyCode.D))
        {
            transform.position += rightPos * speed * Time.deltaTime;
            spr.flipX = false;
        }
        if (Input.GetKey(KeyCode.W))
            transform.position += upPos * speed * Time.deltaTime;
    }
}
