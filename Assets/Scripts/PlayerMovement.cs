using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed = 3;
    [SerializeField] private Text amountOfLives;
    [SerializeField] private GameManager gameManager;

    private int health = 5;
    private Animator walkAnim;
    private Vector3 rightPos, upPos;

    private void Awake()
    {
        amountOfLives.text = "x" + health.ToString();
        walkAnim = GetComponent<Animator>();
        rightPos = new Vector3(.1f, 0, 0);
        upPos = new Vector3(0, .1f, 0);
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D))
            walkAnim.SetFloat("Speed", Mathf.Abs(Input.GetAxisRaw("Horizontal") * speed));
        else if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.W))
            walkAnim.SetFloat("Speed", Mathf.Abs(Input.GetAxisRaw("Vertical") * speed));

        if (Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.S) || Input.GetKeyUp(KeyCode.D) || Input.GetKeyUp(KeyCode.W))
            walkAnim.SetFloat("Speed", 0);

        if (Input.GetKey(KeyCode.A)) { transform.position += -rightPos * speed * Time.deltaTime; }
        if (Input.GetKey(KeyCode.S)) { transform.position += -upPos * speed * Time.deltaTime; }
        if (Input.GetKey(KeyCode.D)) {transform.position += rightPos * speed * Time.deltaTime; }
        if (Input.GetKey(KeyCode.W)) { transform.position += upPos * speed * Time.deltaTime; }
    }

    public void Damage()
    {
        if (health >= 1)
        {
            health--;
            amountOfLives.text = "x" + health.ToString();
        }
        else
            gameManager.ResetGame();
    }

    public void ResetStats()
    {
        transform.position = new Vector3(0, -1, 0);
        health = 5;
        amountOfLives.text = "x" + health.ToString();
    }
}
