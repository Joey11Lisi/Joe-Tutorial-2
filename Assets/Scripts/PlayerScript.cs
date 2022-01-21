using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerScript : MonoBehaviour
{
    private Rigidbody2D rd2d;

    public float speed;

    public Text score;
    public Text healthText;

    private int scoreValue = 0;

    public int health = 3;
    public bool onGround = true;

    // Start is called before the first frame update
    void Start()
    {
        rd2d = GetComponent<Rigidbody2D>();
        score.text = "Score: " + scoreValue.ToString();
    }

    public void TakeDamage()
    {
        health--;
        healthText.text = "Health: " + health.ToString();

        if (health == 0)
        {
            FindObjectOfType<LevelManager>().LoseLevel();
            Destroy(gameObject);

        }

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float hozMovement = Input.GetAxis("Horizontal");
        float vertMovement = Input.GetAxis("Vertical");
        rd2d.AddForce(new Vector2(hozMovement * speed, vertMovement * speed));

        if (onGround)
        {
            if (hozMovement != 0)
            {
                GetComponent<Animator>().SetTrigger("Walk");
                GetComponent<Animator>().ResetTrigger("Idle");
                GetComponent<Animator>().ResetTrigger("Fall");

            }
            else
            {

                print("on ground in fixed update is triggering");
                GetComponent<Animator>().SetTrigger("Idle");
                GetComponent<Animator>().ResetTrigger("Walk");
                GetComponent<Animator>().ResetTrigger("Fall");

            }

        }

    

        if (hozMovement < 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
        else
        {
            transform.localScale = new Vector3(1, 1, 1);

        }
        if (Input.GetKey("escape"))
        {
            Application.Quit();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Coin")
        {
            scoreValue += 1;
            score.text = "Score: " + scoreValue.ToString();
            Destroy(collision.gameObject.gameObject);

            if (scoreValue == 4 * (FindObjectOfType<LevelManager>().currentLevel + 1))
            {
                FindObjectOfType<LevelManager>().NextLevel();
            }
        }

    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.collider.tag == "Ground")
        {
            if (Input.GetKey(KeyCode.W))
            {
                onGround = false;
                GetComponent<Animator>().SetTrigger("Fall");
                GetComponent<Animator>().ResetTrigger("Idle");
                GetComponent<Animator>().ResetTrigger("Walk");

                rd2d.AddForce(new Vector2(0, 3.5f), ForceMode2D.Impulse); //the 3 in this line of code is the player's "jumpforce," and you change that number to get different jump behaviors.  You can also create a public variable for it and then edit it in the inspector.
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Ground")
        {
            print("oncollision enter idle is triggering");
            GetComponent<Animator>().SetTrigger("Idle");
            GetComponent<Animator>().ResetTrigger("Fall");
            GetComponent<Animator>().ResetTrigger("Walk");

            onGround = true;
        }

    }
}