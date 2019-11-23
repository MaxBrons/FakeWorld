using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject chest;
    private PlayerMovement pm;
    private Shooting sh;

    private void Awake()
    {
        pm = GameObject.Find("Player").GetComponent<PlayerMovement>();
        sh = GameObject.Find("Player").GetComponent<Shooting>();
    }

    public void ResetGame()
    {
        pm.ResetStats();
        sh.ResetStats();

        GameObject[] goa = GameObject.FindGameObjectsWithTag("Chests");
        for (int i = 0; i < goa.Length; i++)
        {
            Instantiate(chest, goa[i].transform.position, Quaternion.identity, goa[0].transform.parent.transform);
            Destroy(goa[i].gameObject);
        }
    }
}
