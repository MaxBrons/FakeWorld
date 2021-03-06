﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shooting : MonoBehaviour
{
    [SerializeField] private Camera cam;
    [SerializeField] private Transform barrel;
    [SerializeField] private Animator fireGun;
    [SerializeField] private Text bullets;

    public SpriteRenderer gun;

    private int amountOfBullets = 10;
    private float angle;
    private SpriteRenderer spr;
    private GameObject arm;

    private void Awake()
    {
        bullets.text = "x" + amountOfBullets;
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
            spr.flipX = false;
            gun.flipY = false;
        }

        //Weapon shooting
        if (Input.GetMouseButtonDown(0) && amountOfBullets > 0)
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
        if (!fireGun.GetCurrentAnimatorStateInfo(0).IsName("GunSmoke") && bullets.text != "Reloading...")
        {
            if (amountOfBullets > 1)
            {
                amountOfBullets--;
                bullets.text = "x" + amountOfBullets;

                //shoots a ray to check if it hit a enemy
                fireGun.SetTrigger("Shoot");
                while (!fireGun.GetCurrentAnimatorStateInfo(0).IsName("Checkpoint"))
                    yield return null;

                fireGun.SetTrigger("CheckpointCleared");
                RaycastHit2D hit = Physics2D.Raycast(barrel.position, barrel.forward, 15);
                if (hit.collider != null && hit.collider.tag == "Zombie")
                {
                    Destroy(hit.collider.gameObject);
                }
            }
            else
            {
                bullets.text = "Reloading...";
                yield return new WaitForSeconds(2f);
                ResetStats();
            }
        }
    }

    public void ResetStats()
    {
        amountOfBullets = 10;
        bullets.text = "x" + amountOfBullets;
    }
}
