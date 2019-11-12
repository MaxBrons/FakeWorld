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
    private Vector2 mousePos;
    private Vector2 aimPos;
    private GameObject arm;
    private float speed = 3;

    private void Awake()
    {

        arm = transform.GetChild(0).gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        //Camera rotation towards mouse position
        Vector3 mouseWorldPosition = cam.ScreenToWorldPoint(Input.mousePosition + Vector3.forward * 10f);

        //Sets the rotation off the player to the mouse position
        float angle = AngleBetweenPoints(transform.position, mouseWorldPosition);
        arm.transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, angle + 90));

        //Weapon shooting
        if (Input.GetMouseButton(0))
            StartCoroutine(Shoot());
    }

    //Returns the angle between the player and the cursor
    float AngleBetweenPoints(Vector2 a, Vector2 b)
    {
        return Mathf.Atan2(a.y - b.y, a.x - b.x) * Mathf.Rad2Deg;
    }

    private IEnumerator Shoot()
    {
        while(fireGun.enabled == true)
        {
            fireGun.SetTrigger("Start");
            Instantiate(bullet, barrel.position, barrel.rotation);
            fireGun.ResetTrigger("Start");
            fireGun.enabled = false;
            yield return null;
        }
        yield return null;
    }
}
