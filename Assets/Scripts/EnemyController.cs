using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] ContactFilter2D contactFilter;
    [SerializeField] private float radius;
    [SerializeField] float time;
    [SerializeField] private LayerMask layer;
    private SpriteRenderer spr;
    private GameObject arm;
    private Transform player;
    private Animator walkAnim;
    private bool isTakinDamage = false;
    private float timer = 2;

    private void Awake()
    {
        spr = GetComponent<SpriteRenderer>();
        arm = transform.GetChild(0).gameObject;
        walkAnim = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }
    void Update()
    {
        if (timer > 0) 
            timer -= Time.deltaTime;

        if (Physics2D.OverlapCircle(transform.position, radius, layer))
        {
            walkAnim.SetTrigger("Move"); ;
            if ((player.transform.position - transform.position).sqrMagnitude > .2f)
            {
                transform.position = Vector2.MoveTowards(transform.position, player.position, time * Time.deltaTime);

                //Face the players direction
                Vector3 dir = player.transform.position - arm.transform.position;
                dir.Normalize();
                float zAngle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg - 90;
                arm.transform.rotation = Quaternion.Euler(0, 0, zAngle);

                float angle = AngleBetweenPoints(arm.transform.position, player.position);
                if (angle >= -90 && angle <= 90)
                    spr.flipX = true;
                else
                    spr.flipX = false;
            }
            else if (timer <= 0)
            {
                player.GetComponent<PlayerMovement>().Damage();
                timer = 2;
            }
                
        }
    }

    //Returns the angle between the player and the cursor
    float AngleBetweenPoints(Vector2 a, Vector2 b)
    {
        return Mathf.Atan2(a.y - b.y, a.x - b.x) * Mathf.Rad2Deg;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Bullet")
            Destroy(gameObject);
    }
}
