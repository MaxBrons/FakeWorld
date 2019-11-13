using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] ContactFilter2D contactFilter;
    [SerializeField] private float radius;
    [SerializeField] float time;
    [SerializeField] private LayerMask layer;
    private Transform player;
    private Animator walkAnim;

    private void Awake()
    {
        walkAnim = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }
    void Update()
    {

        if (Physics2D.OverlapCircle(transform.position, radius, layer))
        {
            walkAnim.SetFloat("Speed", 1);;
            if((player.transform.position - transform.position).sqrMagnitude > .2f)
                transform.position = Vector2.MoveTowards(transform.position, player.position, time * Time.deltaTime);
        }
        else
            walkAnim.SetFloat("Speed", 0);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Bullet")
            Destroy(gameObject);
    }
}
