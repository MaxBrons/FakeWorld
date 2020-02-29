using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Chests : MonoBehaviour
{
    [SerializeField] private float radius;
    [SerializeField] private Sprite[] images;
    [SerializeField] int[] answers;
    [SerializeField] private LayerMask layer;
    [SerializeField] private GameObject zombie;

    private Image eButton;
    private Image questionImage;
    private Text winText;
    private Sprite questionImageSprite;
    private Text answerText;
    private int randomInt;

    private static int score = 0;
    private static int totalScore = 0;

    private bool chestOpened = false;
    
    private void Awake()
    {
        eButton = transform.GetChild(0).GetChild(0).GetComponent<Image>();
        eButton.enabled = false;
        questionImageSprite = GetRandomQuestion();
        questionImage = GameObject.FindGameObjectWithTag("UI").transform.GetChild(0).GetComponent<Image>();
        answerText = GameObject.FindGameObjectWithTag("UI").transform.GetChild(1).GetComponent<Text>();
        winText = GameObject.FindGameObjectWithTag("UI").transform.GetChild(2).GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Physics2D.OverlapCircle(transform.position, radius, layer))
        {
            if(!chestOpened)
                eButton.enabled = true;

            if (!chestOpened && Input.GetKey(KeyCode.E))
                StartCoroutine(OpenChest());

            if (chestOpened && Input.GetKey(KeyCode.Escape))
                CloseChest();
        }
        else
        {
            eButton.enabled = false;
        }
    }

    private Sprite GetRandomQuestion()
    {
        randomInt = Random.Range(0, images.Length - 1);
        return images[randomInt];
    }

    private IEnumerator OpenChest()
    {
        chestOpened = true;
        eButton.enabled = false;
        questionImage.sprite = questionImageSprite;
        questionImage.SetNativeSize();
        questionImage.enabled = true;
        answerText.enabled = true;

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
        eButton.enabled = true;
        questionImage.enabled = false;
        answerText.enabled = false;
    }

    private void GetAwnser(int index)
    {
        totalScore += 1;
        questionImage.enabled = false;
        answerText.enabled = false;

        if (answers[randomInt] == index)
            score += 1;
        else
            Instantiate(zombie, transform.position, Quaternion.identity);
            
        if (totalScore == GameObject.FindGameObjectsWithTag("Chests").Length)
        {
            winText.enabled = true;
            Invoke("ResetGame", 3);
        }
    }

    private void ResetGame()
    {
        SceneManager.LoadScene(0);
    }
}
