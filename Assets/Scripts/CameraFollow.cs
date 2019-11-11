using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private float smoothSpeed = 3;
    private Transform player;

    private void Awake()
    {
        player = GameObject.Find("Player").transform;
    }

    void LateUpdate()
    {
        Vector3 camPos = player.position + new Vector3(0, 0, -3);
        Vector3 lerpPos = Vector3.Lerp(transform.position, camPos, smoothSpeed * Time.deltaTime);
        transform.position = lerpPos;
    }
}
