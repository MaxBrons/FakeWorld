using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    [SerializeField] private Camera cam;
    [SerializeField] private GameObject weapon;
    [SerializeField] private GameObject bullet;
    [SerializeField] private Transform barrel;
    [SerializeField] private Animator fireGun;

    public SpriteRenderer gun;

    private SpriteRenderer spr;
    private Vector2 mousePos;
    private Vector2 aimPos;
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
        float angle = AngleBetweenPoints(transform.position, mouseWorldPosition);
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
        if (Input.GetMouseButton(0))
            Shoot();
    }

    //Returns the angle between the player and the cursor
    float AngleBetweenPoints(Vector2 a, Vector2 b)
    {
        return Mathf.Atan2(a.y - b.y, a.x - b.x) * Mathf.Rad2Deg;
    }

    private void Shoot()
    {
        if (fireGun.GetComponent<Animation>().isPlaying)
        {
            fireGun.SetTrigger("Shoot");
            Transform barrel = gun.transform.Find("Barrel").transform;
            RaycastHit hit;
            if (Physics.Raycast(barrel.position, barrel.forward, out hit, 30) && hit.transform.tag == "Zombie")
                Destroy(hit.transform.gameObject);
        }
    }
}
