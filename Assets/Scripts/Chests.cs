using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Chests : MonoBehaviour
{
    [SerializeField] ContactFilter2D contactFilter;
    [SerializeField] private float radius;
    [SerializeField] private Image eButton;
    [SerializeField] Image[] images;
    [SerializeField] int[] answers;
    [SerializeField] Image questionImage;
    [SerializeField] private GameObject zombie;

    public static int score = 0;

    private bool chestOpened = false;
    private Collider2D[] collidersInRange;
    
    private void Awake()
    {
        questionImage = GetRandomQuestion();
        eButton.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        Physics2D.OverlapCircle(transform.position, radius, contactFilter, collidersInRange);
        if (collidersInRange.Length > 0)
        {
            for (int i = 0; i < collidersInRange.Length; i++)
            {
                if(collidersInRange[i].tag == "Player")
                {
                    eButton.enabled = true;
                    if (Input.GetKey(KeyCode.E))
                        OpenChest();
                        
                    if (chestOpened && Input.GetKey(KeyCode.Escape))
                        CloseChest();
                }
            }
        }
    }

    private Image GetRandomQuestion()
    {
        int i = Random.Range(0, images.Length);
        return images[i];
    }

    private IEnumerator OpenChest()
    {
        chestOpened = true;
        questionImage.enabled = true;

        while (!Input.GetKey(KeyCode.Y) && !Input.GetKey(KeyCode.N))
            yield return null;

        if (Input.GetKey(KeyCode.Y))
            GetAwnser(0);
        else if (Input.GetKey(KeyCode.N))
            GetAwnser(1);
    }

    private void CloseChest()
    {
        chestOpened = false;
        questionImage.enabled = false;
        //close UI
    }

    private void GetAwnser(int index)
    {
        if(index == answers[index])
            score++;
        else
            for (int i = 0; i < 10; i++)
               Instantiate(zombie, transform.position, Quaternion.identity);
    }
}
