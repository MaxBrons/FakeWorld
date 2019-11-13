using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    [SerializeField] private Camera cam;
    [SerializeField] private Transform barrel;
    [SerializeField] private Animator fireGun;
    [SerializeField] private GameObject bullet;

    public SpriteRenderer gun;

    private float angle;
    private SpriteRenderer spr;
    private GameObject arm;

    private void Awake()
    {
        spr = GetComponent<SpriteRenderer>();
        arm = transform.GetChild(0).gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        //Camera rotation towards mouse position
        Vector3 mouseWorldPosition = cam.ScreenToWorldPoint(Input.mousePosition + Vector3.forward * 10f);

        //Sets the rotation off the player to the mouse position
        angle = AngleBetweenPoints(transform.position, mouseWorldPosition);
        arm.transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, angle + 180));

        //Sprite flip
        if (angle >= -90 && angle <= 90){
            spr.flipX = true;
            gun.flipY = true;
        }
        else{
            spr.flipX = false; gun.flipY = false;
        }

        //Weapon shooting
        if (Input.GetMouseButtonDown(0))
            StartCoroutine(Shoot());

        Debug.DrawRay(barrel.position, barrel.forward ,Color.red);
    }

    //Returns the angle between the player and the cursor
    float AngleBetweenPoints(Vector2 a, Vector2 b)
    {
        return Mathf.Atan2(a.y - b.y, a.x - b.x) * Mathf.Rad2Deg;
    }

    private IEnumerator Shoot()
    {
        if (!fireGun.GetCurrentAnimatorStateInfo(0).IsName("GunSmoke"))
        {
            fireGun.SetTrigger("Shoot");
            while (!fireGun.GetCurrentAnimatorStateInfo(0).IsName("Checkpoint"))
                yield return null;

            fireGun.SetTrigger("CheckpointCleared");
            RaycastHit2D hit = Physics2D.Raycast(barrel.position, barrel.forward, 15);
            if (hit.collider != null && hit.collider.tag == "Zombie")
            {
                Debug.Log(hit.transform.position - barrel.position);
                Destroy(hit.collider.gameObject);
            }
            yield return null;
        }
    }
}
