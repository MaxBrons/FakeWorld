using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Chests : MonoBehaviour
{
    [SerializeField] private float radius;
    //[SerializeField] private Image eButton;
    [SerializeField] Sprite[] images;
    [SerializeField] int[] answers;
    [SerializeField] private LayerMask layer;
    [SerializeField] private GameObject zombie;
    private Image questionImage;
    private Text answerText;

    public static int score = 0;

    private bool chestOpened = false;
    
    private void Awake()
    {
        questionImage = GameObject.FindGameObjectWithTag("UI").transform.GetChild(0).GetComponent<Image>();
        answerText = GameObject.FindGameObjectWithTag("UI").transform.GetChild(1).GetComponent<Text>();
        questionImage.sprite = GetRandomQuestion();
        //eButton.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Physics2D.OverlapCircle(transform.position, radius, layer))
        {
            Debug.Log(1);
            //eButton.enabled = true;
            if (Input.GetKey(KeyCode.E))
                StartCoroutine(OpenChest());

            if (chestOpened && Input.GetKey(KeyCode.Escape))
                CloseChest();
        }
        else
        {
            //eButton.enabled = false;
        }
    }

    private Sprite GetRandomQuestion()
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

        answerText.enabled = true;
        if (Input.GetKey(KeyCode.Y))
            GetAwnser(0);
        else if (Input.GetKey(KeyCode.N))
            GetAwnser(1);
    }

    private void CloseChest()
    {
        chestOpened = false;
        questionImage.enabled = false;
        answerText.enabled = false;
        //close UI
    }

    private void GetAwnser(int index)
    {
        if(index == answers[index])
            score++;
        else
        Instantiate(zombie, transform.position, Quaternion.identity);
        questionImage.enabled = false;
        answerText.enabled = false;
    }
}
